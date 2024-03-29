﻿using System;
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
            foreach (var link in links)
            {
                if (link != null)
                {
                    secondDocumentsComboBox.Items.Add(link.Title);
                }
            }

            var categoryNames = namaCategoryDict.Keys.ToArray();
            foreach (var name in categoryNames.OrderBy(n => n))
            {
                firstCategoriesCheckedListBox.Items.Add(name);
                secondCategoriesCheckedListBox.Items.Add(name);
            }


            }

        private void startSearchButton_Click(object sender, EventArgs e)
        {
            var firstSelectedCategoriesNames = firstCategoriesCheckedListBox.CheckedItems.Cast<string>().ToArray();
            var firstSelectedCategories = firstSelectedCategoriesNames.Select(n => namaCategoryDict[n]).ToList();
            var firstDocumentElementMulticategoryFilter = new ElementMulticategoryFilter(firstSelectedCategories);//категории из первого документа

            var secondSelectedCategoriesNames = secondCategoriesCheckedListBox.CheckedItems.Cast<string>().ToArray();
            var secondSelectedCategories = secondSelectedCategoriesNames.Select(n => namaCategoryDict[n]).ToList();
            var secondDocumentName = secondDocumentsComboBox.SelectedItem;
            Document seconddocument;
            ElementMulticategoryFilter secondDocumentElementMulticategoryFilter;
            if ((string)secondDocumentName != currentdocument.Title)
            {
                seconddocument = new FilteredElementCollector(currentdocument)
                                                .OfClass(typeof(RevitLinkInstance))
                                                .Cast<RevitLinkInstance>()
                                                .Where(inst => inst.GetLinkDocument().Title == (string)secondDocumentName).First().GetLinkDocument();
                secondDocumentElementMulticategoryFilter = new ElementMulticategoryFilter(secondSelectedCategories);//категории из второго документа
            }
            else
            {
                seconddocument = currentdocument;
                secondDocumentElementMulticategoryFilter = firstDocumentElementMulticategoryFilter;
            }


            var categoryFEC = new FilteredElementCollector(currentdocument, view.Id).WherePasses(firstDocumentElementMulticategoryFilter);//элементы первого документа
            var elems = categoryFEC;

            Options options = new Options();
            var counter = 0;
            using (Transaction transaction = new Transaction(currentdocument))
            {
                transaction.Start("FindIntersections");
                foreach (Element elem in elems)
                {
                    ElementIntersectsElementFilter elementFilter = new ElementIntersectsElementFilter(elem);
                    LogicalAndFilter lAF = new LogicalAndFilter(secondDocumentElementMulticategoryFilter, elementFilter);
                    //элементы из второго документа пересекающиеся с первым
                    FilteredElementCollector FEC = new FilteredElementCollector(seconddocument).WherePasses(lAF);
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
                        counter++;
                    }
                    else
                    {
                        elem.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS).Set(" ");
                    }
                }
                transaction.Commit();
            }
            labelCounter.Text = counter.ToString();
            var labelColor = counter == 0 ? labelCounter.ForeColor = System.Drawing.Color.Green : labelCounter.ForeColor = System.Drawing.Color.Red;

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



        private void selectAllCategoriesButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < firstCategoriesCheckedListBox.Items.Count; i++)
            {
                firstCategoriesCheckedListBox.SetItemCheckState(i, CheckState.Checked);
            }
        }

        private void SecondSelectAllCategoriesButton(object sender, EventArgs e)
        {
            for (int i = 0; i < secondCategoriesCheckedListBox.Items.Count; i++)
            {
                secondCategoriesCheckedListBox.SetItemCheckState(i, CheckState.Checked);
            }
        }
    }
}
