using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Electrical;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static BIMTools.MyRevitUtils;

namespace BIMTools
{
    public partial class SystemsIsolationWindow : System.Windows.Forms.Form
    {
        public Document document {  get; set; }
        public UIDocument uidocument { get; set; }
        public Element[] pipeInsulationTypes { get; set; }
        public Element[] ductInsulationTypes { get; set; }

        public Dictionary<string, Dictionary<string, Element[]>> systemsDiametersDictionary = new Dictionary<string, Dictionary<string, Element[]>>();
        public Dictionary<string, Element[]> systemsElementsDictionary = new Dictionary<string, Element[]>();
        public SystemsIsolationWindow()
        {
            InitializeComponent();
        }

        private void SystemsIsolationWindow_Load(object sender, EventArgs e)
        {
            TreeNode systemNode = new TreeNode();
            systemNode.Name = "System";
            systemNode.Text = "Система";
            systemsTreeView.Nodes.Add(systemNode);
            //systemNode.Nodes["System"].ForeColor = System.Drawing.Color.Gray;

            systemNode.Nodes.Add("OV", "ОВ");
            systemNode.Nodes["OV"].ForeColor = System.Drawing.Color.Gray;
            systemNode.Nodes.Add("VK", "ВК");
            systemNode.Nodes["VK"].ForeColor = System.Drawing.Color.Gray;

            //водоснабжение
            var pipingSystems = new FilteredElementCollector(document).OfCategory(BuiltInCategory.OST_PipingSystem).WhereElementIsNotElementType().Cast<PipingSystem>().ToArray();
            var pipingSystemTypes = pipingSystems.Select(x => document.GetElement(x.GetTypeId())).OrderBy(x => x.Name).ToArray().Distinct();
            foreach (var pipingSystemType in pipingSystemTypes)
            {
                systemNode.Nodes["VK"].Nodes.Add(pipingSystemType.Name, pipingSystemType.Name);
                systemNode.Nodes["VK"].Nodes[pipingSystemType.Name].ForeColor = System.Drawing.Color.Gray;
            }
            foreach (var pipingSystem in pipingSystems)
            {
                //все элементы из системы
                var pipeElements = pipingSystem.PipingNetwork.OfType<Pipe>();
                var familyElements = pipingSystem.PipingNetwork.OfType<FamilyInstance>();


                //
                var pipingSystemType = document.GetElement(pipingSystem.GetTypeId());
                systemNode.Nodes["VK"].Nodes[pipingSystemType.Name].Nodes.Add(pipingSystem.Name, pipingSystem.Name);
                var pipesGroupedByDiameters = pipingSystem.PipingNetwork.OfType<Pipe>().GroupBy(p => p.get_Parameter(BuiltInParameter.RBS_CALCULATED_SIZE).AsString()).OrderBy(g => g.Key);
                //трубы системы в дереве
                foreach (var pipesGroupedByDiameter in pipesGroupedByDiameters)
                {
                    systemNode.Nodes["VK"].Nodes[pipingSystemType.Name].Nodes[pipingSystem.Name].Nodes.Add(pipesGroupedByDiameter.Key, "Трубы: " + pipesGroupedByDiameter.Key);
                    //создаю словарь - имя системы: размер: трубы
                    if (systemsDiametersDictionary.ContainsKey(pipingSystem.Name))
                    {
                        systemsDiametersDictionary[pipingSystem.Name].Add("Трубы: " + pipesGroupedByDiameter.Key, pipesGroupedByDiameter.ToArray());
                    }
                    else
                    {
                        systemsDiametersDictionary.Add(pipingSystem.Name, new Dictionary<string, Element[]> { { "Трубы: " + pipesGroupedByDiameter.Key, pipesGroupedByDiameter.ToArray() } });
                    }
                }
                // прочие элементы в дереве
                var fittingsGroupedByDiameters = pipingSystem.PipingNetwork.OfType<FamilyInstance>().GroupBy(p => p.get_Parameter(BuiltInParameter.RBS_CALCULATED_SIZE).AsString()).OrderBy(g => g.Key);
                foreach (var fittingsGroupedByDiameter in fittingsGroupedByDiameters)
                {
                    systemNode.Nodes["VK"].Nodes[pipingSystemType.Name].Nodes[pipingSystem.Name].Nodes.Add(fittingsGroupedByDiameter.Key, "Прочие элементы: " + fittingsGroupedByDiameter.Key);
                    //создаю словарь - имя системы: размер: трубы
                    if (systemsDiametersDictionary.ContainsKey(pipingSystem.Name))
                    {
                        systemsDiametersDictionary[pipingSystem.Name].Add("Прочие элементы: " + fittingsGroupedByDiameter.Key, fittingsGroupedByDiameter.ToArray());
                    }
                    else
                    {
                        systemsDiametersDictionary.Add(pipingSystem.Name, new Dictionary<string, Element[]> { { "Прочие элементы: " + fittingsGroupedByDiameter.Key, fittingsGroupedByDiameter.ToArray() } });
                    }
                }

            }

            //вентиляция
            var ductSystems = new FilteredElementCollector(document).OfCategory(BuiltInCategory.OST_DuctSystem).WhereElementIsNotElementType().Cast<MechanicalSystem>().ToArray();
            var ductSystemTypes = ductSystems.Select(x => document.GetElement(x.GetTypeId())).OrderBy(x => x.Name).ToArray().Distinct();
            foreach (var ductSystemType in ductSystemTypes)
            {
                systemNode.Nodes["OV"].Nodes.Add(ductSystemType.Name, ductSystemType.Name);
                systemNode.Nodes["OV"].Nodes[ductSystemType.Name].ForeColor = System.Drawing.Color.Gray;
            }
            foreach (MechanicalSystem ductSystem in ductSystems)
            {
                //все элементы из системы
                var ductElements = ductSystem.DuctNetwork.OfType<Duct>();
                var familyElements = ductSystem.DuctNetwork.OfType<FamilyInstance>();

                var ductSystemType = document.GetElement(ductSystem.GetTypeId());
                systemNode.Nodes["OV"].Nodes[ductSystemType.Name].Nodes.Add(ductSystem.Name, ductSystem.Name);
                var ductsGroupedByRazmers = ductElements.GroupBy(p => p.get_Parameter(BuiltInParameter.RBS_CALCULATED_SIZE).AsString()).OrderBy(g => g.Key);
                //трубы системы в дереве
                foreach (var ductsGroupedByRazmer in ductsGroupedByRazmers)
                {
                    systemNode.Nodes["OV"].Nodes[ductSystemType.Name].Nodes[ductSystem.Name].Nodes.Add(ductsGroupedByRazmer.Key, "Воздуховоды: " + ductsGroupedByRazmer.Key);
                    //создаю словарь - имя системы: размер: трубы
                    if (systemsDiametersDictionary.ContainsKey(ductSystem.Name))
                    {
                        systemsDiametersDictionary[ductSystem.Name].Add("Воздуховоды: " + ductsGroupedByRazmer.Key, ductsGroupedByRazmer.ToArray());
                    }
                    else
                    {
                        systemsDiametersDictionary.Add(ductSystem.Name, new Dictionary<string, Element[]> { { "Воздуховоды: " + ductsGroupedByRazmer.Key, ductsGroupedByRazmer.ToArray() } });
                    }
                }

                //
                // прочие элементы в дереве
                var fittingsGroupedByDiameters = familyElements.GroupBy(p => p.get_Parameter(BuiltInParameter.RBS_CALCULATED_SIZE).AsString()).OrderBy(g => g.Key);
                foreach (var fittingsGroupedByDiameter in fittingsGroupedByDiameters)
                {
                    systemNode.Nodes["OV"].Nodes[ductSystemType.Name].Nodes[ductSystem.Name].Nodes.Add(fittingsGroupedByDiameter.Key, "Прочие элементы: " + fittingsGroupedByDiameter.Key);
                    //создаю словарь - имя системы: размер: воздуховода
                    if (systemsDiametersDictionary.ContainsKey(ductSystem.Name))
                    {
                        systemsDiametersDictionary[ductSystem.Name].Add("Прочие элементы: " + fittingsGroupedByDiameter.Key, fittingsGroupedByDiameter.ToArray());
                    }
                    else
                    {
                        systemsDiametersDictionary.Add(ductSystem.Name, new Dictionary<string, Element[]> { { "Прочие элементы: " + fittingsGroupedByDiameter.Key, fittingsGroupedByDiameter.ToArray() } });
                    }
                }

            }


            insulationTypesComboBox.Items.Add("Изоляция воздуховодов: ");
            ductInsulationTypes = new FilteredElementCollector(document).OfClass(typeof(DuctInsulationType)).Cast<DuctInsulationType>().ToArray();
            foreach (var insulationType in ductInsulationTypes)
            {
                insulationTypesComboBox.Items.Add(insulationType.Name);
            }

            insulationTypesComboBox.Items.Add("\n");
            insulationTypesComboBox.Items.Add("Изоляция трубопроводов: ");
            pipeInsulationTypes = new FilteredElementCollector(document).OfClass(typeof(PipeInsulationType)).Cast<PipeInsulationType>().ToArray();
            foreach (var insulationType in pipeInsulationTypes)
            {
                insulationTypesComboBox.Items.Add(insulationType.Name);
            }
            insulationTypesComboBox.SelectedItem = insulationTypesComboBox.Items[0];
        }



        private void PlaceInsulation_Click(object sender, EventArgs e)
        {
            var insulationType = pipeInsulationTypes.FirstOrDefault(ins => ins.Name == (string)insulationTypesComboBox.SelectedItem);
            int insulationWidth = int.Parse(insulationWidthTextBox.Text);
            //если трубы
            if (insulationType != null)
            {
                //если выбрали всю системы
                if (systemsDiametersDictionary.ContainsKey(systemsTreeView.SelectedNode.Text))
                {
                    string systemName = systemsTreeView.SelectedNode.Text;
                    var elementsByDiameters = systemsDiametersDictionary[systemName];
                    var elementArray = elementsByDiameters.Values.SelectMany(elar => elar.Select(el => el).ToArray()).ToArray();
                    CreateInsulation(document, elementArray, insulationType.Id, insulationWidth, false, true);
                }
                //если выбрали трубы/элементы
                else
                {
                    var diameter = int.Parse(string.Join("", systemsTreeView.SelectedNode.Text.Where(c => char.IsDigit(c))));
                    var diametertext = systemsTreeView.SelectedNode.Text;
                    string systemName = systemsTreeView.SelectedNode.Parent.Text;
                    var elements = systemsDiametersDictionary[systemName][diametertext];
                    CreateInsulation(document, elements, insulationType.Id, insulationWidth, false, true);
                }
            }
            //если воздуховоды
            else
            {
                insulationType = ductInsulationTypes.FirstOrDefault(ins => ins.Name == (string)insulationTypesComboBox.SelectedItem);
                //если выбрали всю системы
                if (systemsDiametersDictionary.ContainsKey(systemsTreeView.SelectedNode.Text))
                {
                    string systemName = systemsTreeView.SelectedNode.Text;
                    var elementsByDiameters = systemsDiametersDictionary[systemName];
                    var elementArray = elementsByDiameters.Values.SelectMany(elar => elar.Select(el => el).ToArray()).ToArray();
                    CreateInsulation(document, elementArray, insulationType.Id, insulationWidth, true, false);
                }
                //если выбрали воздуховоды/элементы
                else
                {
                    var diameter = int.Parse(string.Join("", systemsTreeView.SelectedNode.Text.Where(c => char.IsDigit(c))));
                    var diametertext = systemsTreeView.SelectedNode.Text;
                    string systemName = systemsTreeView.SelectedNode.Parent.Text;
                    var elements = systemsDiametersDictionary[systemName][diametertext];
                    CreateInsulation(document, elements, insulationType.Id, insulationWidth, true, false);
                }
            }

        }
        private void CreateInsulation(Document document, Element[] elementss, ElementId insulationTypeId, double insulationWidth, bool duct = false, bool pipe = false)
        {
            var idsToExclude = new[] { (int)BuiltInCategory.OST_MechanicalEquipment, (int)BuiltInCategory.OST_DuctTerminal};
                
            var elements = elementss.Where(e => !idsToExclude.Contains(e.Category.Id.IntegerValue));
            using (Transaction transaction = new Transaction(document))
            {
                transaction.Start("Изоляция");
                if (duct)
                {
                    foreach (var element in elements)
                    {
                        DuctInsulation.Create(document, element.Id, insulationTypeId,ConvertUnit(document, insulationWidth));
                    }
                }
                else if (pipe)
                {
                    foreach (var element in elements)
                    {
                        PipeInsulation.Create(document, element.Id, insulationTypeId, ConvertUnit(document, insulationWidth));
                    }
                }
                transaction.Commit();
            }
        }

        private void DeleteInsulation_Click(object sender, EventArgs e)
        {
            ElementMulticlassFilter filter = new ElementMulticlassFilter(new[] { typeof(PipeInsulation), typeof(DuctInsulation) });
            //если выбрали всю системы
            if (systemsDiametersDictionary.ContainsKey(systemsTreeView.SelectedNode.Text))
            {
                string systemName = systemsTreeView.SelectedNode.Text;
                var elementsByDiameters = systemsDiametersDictionary[systemName];
                using (Transaction transaction = new Transaction(document))
                {
                    transaction.Start("Изоляция");
                    foreach (var elementArray in elementsByDiameters.Values)
                    {

                        foreach (var element in elementArray)
                        {
                            var insulationId = element.GetDependentElements(filter).FirstOrDefault();
                            if (insulationId != null)
                            {
                                document.Delete(insulationId);
                            }
                        }
                    }
                    transaction.Commit();
                }
            }
            else
            {
                var diametertext = systemsTreeView.SelectedNode.Text;
                string systemName = systemsTreeView.SelectedNode.Parent.Text;
                var elements = systemsDiametersDictionary[systemName][diametertext];
                using (Transaction transaction = new Transaction(document))
                {
                    transaction.Start("Удалить изоляцию");
                    foreach (var element in elements)
                    {
                        var insulationId = element.GetDependentElements(filter).FirstOrDefault();
                        if (insulationId != null)
                        {
                            document.Delete(insulationId);
                        }
                    }
                    transaction.Commit();
                }
            }
        }

        private void showSystemButton_Click(object sender, EventArgs e)
        {
            if (systemsDiametersDictionary.ContainsKey(systemsTreeView.SelectedNode.Text))
            {
                string systemName = systemsTreeView.SelectedNode.Text;
                var elementsByDiameters = systemsDiametersDictionary[systemName];
                uidocument.ShowElements(elementsByDiameters.Values.SelectMany(elar => elar.Select(el => el.Id).ToArray()).ToArray());
                uidocument.Selection.SetElementIds(elementsByDiameters.Values.SelectMany(elar => elar.Select(el => el.Id).ToArray()).ToArray());
            }
        }
    }
}
