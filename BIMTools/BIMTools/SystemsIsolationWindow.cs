using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Electrical;
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
        public PipeInsulationType[] insulationTypes { get; set; }

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

            var pipingSystems = new FilteredElementCollector(document).OfCategory(BuiltInCategory.OST_PipingSystem).WhereElementIsNotElementType().Cast<PipingSystem>().ToArray();
            var pipingSystemTypes = pipingSystems.Select(x => document.GetElement(x.GetTypeId())).ToArray().Distinct();
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
            /////
            //var pipesGroupedBySystemTypeNames = new FilteredElementCollector(document).OfCategory(BuiltInCategory.OST_PipeCurves).WhereElementIsNotElementType().Cast<Pipe>().GroupBy(p => p.get_Parameter(BuiltInParameter.RBS_PIPING_SYSTEM_TYPE_PARAM).AsValueString()).OrderBy(g => g.Key);
            //foreach ( var pipesGroupedBySystemTypeName in pipesGroupedBySystemTypeNames)
            //{
            //    systemNode.Nodes["VK"].Nodes.Add(pipesGroupedBySystemTypeName.Key, pipesGroupedBySystemTypeName.Key);
            //    var pipesGroupedBySystemNames = pipesGroupedBySystemTypeName.GroupBy(p => p.MEPSystem.Name).OrderBy(g => g.Key);
            //    foreach (var pipesGroupedBySystemName in pipesGroupedBySystemNames)
            //    {
            //        systemNode.Nodes["VK"].Nodes[pipesGroupedBySystemTypeName.Key].Nodes.Add(pipesGroupedBySystemName.Key, pipesGroupedBySystemName.Key);
            //        var pipesGroupedByDiameters = pipesGroupedBySystemName.GroupBy(p => p.get_Parameter(BuiltInParameter.RBS_CALCULATED_SIZE).AsString()).OrderBy(g => g.Key);
            //        foreach (var pipesGroupedByDiameter in pipesGroupedByDiameters)
            //        {
            //            systemNode.Nodes["VK"].Nodes[pipesGroupedBySystemTypeName.Key].Nodes[pipesGroupedBySystemName.Key].Nodes.Add(pipesGroupedByDiameter.Key, pipesGroupedByDiameter.Key);
            //            //создаю словарь - имя системы: размер: трубы
            //            if (pipesDictionary.ContainsKey(pipesGroupedBySystemName.Key))
            //            {
            //                pipesDictionary[pipesGroupedBySystemName.Key].Add(pipesGroupedByDiameter.Key, pipesGroupedByDiameter.ToArray());
            //            }
            //            else
            //            {
            //                pipesDictionary.Add(pipesGroupedBySystemName.Key, new Dictionary<string, Pipe[]> { { pipesGroupedByDiameter.Key, pipesGroupedByDiameter.ToArray() } });
            //            }
            //        }

            //    }
            //}
            /////
            systemNode.ExpandAll();

            insulationTypes = new FilteredElementCollector(document).OfClass(typeof(PipeInsulationType)).Cast<PipeInsulationType>().ToArray();
            foreach (var insulationType in insulationTypes)
            {
                insulationTypesComboBox.Items.Add(insulationType.Name);
            }
            insulationTypesComboBox.SelectedItem = insulationTypesComboBox.Items[0];

        }



        private void PlaceInsulation_Click(object sender, EventArgs e)
        {
            var insulationType = insulationTypes.First(ins => ins.Name == (string)insulationTypesComboBox.SelectedItem);
            int insulationWidth = int.Parse(insulationWidthTextBox.Text);
            //если выбрали всю системы
            if (systemsDiametersDictionary.ContainsKey(systemsTreeView.SelectedNode.Text))
            {
                string systemName = systemsTreeView.SelectedNode.Text;
                var elementsByDiameters = systemsDiametersDictionary[systemName];
                using (Transaction transaction = new Transaction(document))
                {
                    transaction.Start("Изоляция");
                    foreach ( var elementArray in elementsByDiameters.Values)
                    {
                        foreach (var element in elementArray)
                        {

                            PipeInsulation.Create(document,
                                      element.Id,
                                      insulationType.Id,
                                      ConvertUnit(document, insulationWidth));
                        }
                    }
                    transaction.Commit();
                }
            }
            //если выбрали трубы/элементы
            else
            {
                var diameter = int.Parse(string.Join("", systemsTreeView.SelectedNode.Text.Where(c => char.IsDigit(c))));
                var diametertext = systemsTreeView.SelectedNode.Text;
                string systemName = systemsTreeView.SelectedNode.Parent.Text;
                var elements = systemsDiametersDictionary[systemName][diametertext];
                using (Transaction transaction = new Transaction(document))
                {
                    transaction.Start("Изоляция");
                    foreach (var element in elements)
                    {
                        PipeInsulation.Create(document,
                                  element.Id,
                                  insulationType.Id,
                                  ConvertUnit(document, insulationWidth));
                    }
                    transaction.Commit();
                }
            }

        }

        private void DeleteInsulation_Click(object sender, EventArgs e)
        {
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
                        ElementClassFilter filter = new ElementClassFilter(typeof(PipeInsulation));
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
                    ElementClassFilter filter = new ElementClassFilter(typeof(PipeInsulation));
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


    }
}
