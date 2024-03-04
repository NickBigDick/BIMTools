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
        public Autodesk.Revit.DB.View view { get; set; }
        public FindIntersectionsWindow()
        {
            InitializeComponent();
        }

        public List<ElementId> categories = new List<ElementId>()
            {
                new ElementId(BuiltInCategory.OST_DuctAccessory),
                new ElementId(BuiltInCategory.OST_DuctCurves),
                new ElementId(BuiltInCategory.OST_DuctFitting),
                new ElementId(BuiltInCategory.OST_DuctTerminal),
                new ElementId(BuiltInCategory.OST_DuctInsulations),
                new ElementId(BuiltInCategory.OST_PipeAccessory),
                new ElementId(BuiltInCategory.OST_PipeCurves),
                new ElementId(BuiltInCategory.OST_PipeFitting),
                new ElementId(BuiltInCategory.OST_PipeInsulations),
                new ElementId(BuiltInCategory.OST_MechanicalEquipment),
                new ElementId(BuiltInCategory.OST_PlumbingFixtures),
                new ElementId(BuiltInCategory.OST_FlexPipeCurves),
            };
        public Dictionary<string, ElementId> namaCategoryDict = new Dictionary<string, ElementId>()
                {
                    { "Арматура воздуховодов", new ElementId(BuiltInCategory.OST_DuctAccessory)},
                    { "Воздуховоды", new ElementId(BuiltInCategory.OST_DuctCurves)},
                    { "Соединительные детали воздуховодов", new ElementId(BuiltInCategory.OST_DuctFitting)},
                    { "Воздухораспределители", new ElementId(BuiltInCategory.OST_DuctTerminal)},
                    { "Материалы изоляции воздуховодов", new ElementId(BuiltInCategory.OST_DuctInsulations)},
                    { "Арматура трубопроводов", new ElementId(BuiltInCategory.OST_PipeAccessory)},
                    { "Трубы", new ElementId(BuiltInCategory.OST_PipeCurves)},
                    { "Соединительные детали трубопроводов", new ElementId(BuiltInCategory.OST_PipeFitting)},
                    { "Материалы изоляции труб", new ElementId(BuiltInCategory.OST_PipeInsulations)},
                    { "Оборудование", new ElementId(BuiltInCategory.OST_MechanicalEquipment)},
                    { "Сантехнические приборы", new ElementId(BuiltInCategory.OST_PlumbingFixtures)},
                    { "Гибкие трубы", new ElementId(BuiltInCategory.OST_FlexPipeCurves)},

                };
        private void FindIntersectionsWindow_Load(object sender, EventArgs e)
            {

            //create categories
            firstDocumentsComboBox.Items.Clear();
            firstDocumentsComboBox.Items.Add(currentdocument.Title);
            firstDocumentsComboBox.SelectedItem = firstDocumentsComboBox.Items[0];
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
            var categoryNames = namaCategoryDict.Keys.ToArray();
            foreach (var name in categoryNames)
            {
                firstCategoriesCheckedListBox.Items.Add(name);
                secondCategoriesCheckedListBox.Items.Add(name);
            }

            }

        private void startSearchButton_Click(object sender, EventArgs e)
        {
            var firstSelectedCategoriesNames = firstCategoriesCheckedListBox.SelectedItems.Cast<string>().ToArray();
            var firstSelectedCategories = firstSelectedCategoriesNames.Select(n => namaCategoryDict[n]).ToList();


            var elementMulticategoryFilter = new ElementMulticategoryFilter(firstSelectedCategories);

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
