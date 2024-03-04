using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;
using System.Collections.Generic;


namespace BIMTools
{
    public partial class FindIntersectionsWindow : System.Windows.Forms.Form
    {
        public Document currentdocument {  get; set; }
        public List<ElementId> categories { get; set; }
        public Autodesk.Revit.DB.View view { get; set; }
        public FindIntersectionsWindow()
        {
            InitializeComponent();
        }

        private void FindIntersectionsWindow_Load(object sender, EventArgs e)
        {
            secondDocumentsComboBox.Items.Clear();
            secondDocumentsComboBox.Items.Add(currentdocument.Title);
            secondDocumentsComboBox.SelectedItem = secondDocumentsComboBox.Items[0];
            var links = new FilteredElementCollector(currentdocument).OfClass(typeof(RevitLinkInstance)).Cast<RevitLinkInstance>().Select(inst => inst.GetLinkDocument());
            foreach ( var link in links )
            {
                if (link != null)
                {
                    secondDocumentsComboBox.Items.Add(link.Title);
                }
            }

            firstCategoriesCheckedListBox.Items.Clear();
            secondCategoriesCheckedListBox.Items.Clear();
            categories.ForEach(c => secondCategoriesCheckedListBox.Items.Add(Category.GetCategory(currentdocument,c).Name));
            categories.ForEach(c => firstCategoriesCheckedListBox.Items.Add(Category.GetCategory(currentdocument,c).Name));

        }

        private void startSearchButton_Click(object sender, EventArgs e)
        {
            var firstSelectedCategoriesNames = firstCategoriesCheckedListBox.SelectedItems;
            categories = new FilteredElementCollector(currentdocument).OfClass(typeof(BuiltInCategory)).Where(c => firstSelectedCategoriesNames.Contains(c)).Select(c => c.Id).ToList();

            var elementMulticategoryFilter = new ElementMulticategoryFilter(categories);

            var categoryFEC = new FilteredElementCollector(currentdocument, view.Id).WherePasses(elementMulticategoryFilter);
            var elems = categoryFEC;

            Options options = new Options();
            using (Transaction transaction = new Transaction(currentdocument))
            {
                transaction.Start("FindIntersections");
                foreach (Element elem in elems)
                {
                    ElementIntersectsElementFilter elementFilter = new ElementIntersectsElementFilter(elem);
                    LogicalAndFilter lAF = new LogicalAndFilter(elementMulticategoryFilter, elementFilter);
                    FilteredElementCollector FEC = new FilteredElementCollector(currentdocument, view.Id).WherePasses(lAF);
                    if (elem.GetType() == typeof(FamilyInstance))
                    {
                        var inst = (FamilyInstance)elem;
                        var subComponentIds = inst.GetSubComponentIds();
                        if (subComponentIds.Any())
                        {
                            FEC = FEC.Excluding(subComponentIds);
                        }
                    }
                    if (FEC.Any())
                    {
                        elem.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS).Set("Пересекается");
                    }
                    else
                    {
                        elem.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS).Set(" ");
                    }
                }
                transaction.Commit();
            }

            var parameterFilter = new FilteredElementCollector(currentdocument).OfClass(typeof(ParameterFilterElement)).Cast<ParameterFilterElement>().Where(f => f.Name == "Фильтр пересечений").FirstOrDefault();

            //create rules
            ElementId parameterId = new ElementId(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS);
            FilterRule rule = ParameterFilterRuleFactory.CreateContainsRule(parameterId, "Пересекается", false);

            //create logicalandfilter
            ElementParameterFilter filter = new ElementParameterFilter(rule, true);
            LogicalAndFilter logicalAndFilter = new LogicalAndFilter(new[] { filter });

            using (Transaction transaction = new Transaction(currentdocument))
            {
                transaction.Start("Create and apply filter");
                //createparameterfilter
                if (parameterFilter == default(ParameterFilterElement))
                {
                    parameterFilter = ParameterFilterElement.Create(currentdocument, "Фильтр пересечений", categories);
                    parameterFilter.SetElementFilter(logicalAndFilter);
                }
                //setgrafic override
                OverrideGraphicSettings overrides = new OverrideGraphicSettings();
                overrides.SetCutLineColor(new Color(255, 0, 0));
                overrides.SetHalftone(true);
                //apply filter to view
                if (!view.GetFilters().Contains(parameterFilter.Id))
                {
                    view.AddFilter(parameterFilter.Id);
                }
                //view.SetFilterVisibility(parameterFilter.Id, false);
                view.SetFilterOverrides(parameterFilter.Id, overrides);
                transaction.Commit();
            }
        }
    }
}
