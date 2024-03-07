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
using static BIMTools.MyRevitUtils;

namespace BIMTools
{
    public partial class SystemsIsolationWindow : System.Windows.Forms.Form
    {
        public Document document {  get; set; }
        public PipeInsulationType[] insulationTypes { get; set; }

        public Dictionary<string, Dictionary<string, Pipe[]>> pipesDictionary = new Dictionary<string, Dictionary<string, Pipe[]>>();
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

            systemNode.Nodes.Add("OV", "ОВ");
            systemNode.Nodes.Add("VK", "ВК");
            var pipingSystemTypes = new FilteredElementCollector(document).OfCategory(BuiltInCategory.OST_PipingSystem).WhereElementIsElementType().Cast<PipingSystemType>().ToArray();
            var pipesGroupedBySystemTypeNames = new FilteredElementCollector(document).OfCategory(BuiltInCategory.OST_PipeCurves).WhereElementIsNotElementType().Cast<Pipe>().GroupBy(p => p.get_Parameter(BuiltInParameter.RBS_PIPING_SYSTEM_TYPE_PARAM).AsValueString()).OrderBy(g => g.Key);
            foreach ( var pipesGroupedBySystemTypeName in pipesGroupedBySystemTypeNames)
            {
                systemNode.Nodes["VK"].Nodes.Add(pipesGroupedBySystemTypeName.Key, pipesGroupedBySystemTypeName.Key);
                var pipesGroupedBySystemNames = pipesGroupedBySystemTypeName.GroupBy(p => p.MEPSystem.Name).OrderBy(g => g.Key);
                foreach (var pipesGroupedBySystemName in pipesGroupedBySystemNames)
                {
                    systemNode.Nodes["VK"].Nodes[pipesGroupedBySystemTypeName.Key].Nodes.Add(pipesGroupedBySystemName.Key, pipesGroupedBySystemName.Key);
                    var pipesGroupedByDiameters = pipesGroupedBySystemName.GroupBy(p => p.get_Parameter(BuiltInParameter.RBS_CALCULATED_SIZE).AsString()).OrderBy(g => g.Key);
                    foreach (var pipesGroupedByDiameter in pipesGroupedByDiameters)
                    {
                        systemNode.Nodes["VK"].Nodes[pipesGroupedBySystemTypeName.Key].Nodes[pipesGroupedBySystemName.Key].Nodes.Add(pipesGroupedByDiameter.Key, pipesGroupedByDiameter.Key);
                        //создаю словарь - имя системы: размер: трубы
                        if (pipesDictionary.ContainsKey(pipesGroupedBySystemName.Key))
                        {
                            pipesDictionary[pipesGroupedBySystemName.Key].Add(pipesGroupedByDiameter.Key, pipesGroupedByDiameter.ToArray());
                        }
                        else
                        {
                            pipesDictionary.Add(pipesGroupedBySystemName.Key, new Dictionary<string, Pipe[]> { { pipesGroupedByDiameter.Key, pipesGroupedByDiameter.ToArray() } });
                        }
                    }

                }
            }
            systemNode.ExpandAll();

            insulationTypes = new FilteredElementCollector(document).OfClass(typeof(PipeInsulationType)).Cast<PipeInsulationType>().ToArray();
            foreach (var insulationType in insulationTypes)
            {
                insulationTypesComboBox.Items.Add(insulationType.Name);
            }

        }

        private void nodeWasSelected(object sender, TreeViewEventArgs e)
        {
            label1.Text = systemsTreeView.SelectedNode.Text;
        }

        private void PlaceInsulation_Click(object sender, EventArgs e)
        {
            var diametertext = systemsTreeView.SelectedNode.Text;
            var diameter = int.Parse(string.Join("", systemsTreeView.SelectedNode.Text.Where(c => char.IsDigit(c))));
            var insulationType = insulationTypes.First(ins => ins.Name == (string)insulationTypesComboBox.SelectedItem);
            int insulationWidth = int.Parse(insulationWidthTextBox.Text);
            string systemName = systemsTreeView.SelectedNode.Parent.Text;
            var pipes = pipesDictionary[systemName][diametertext];
            using (Transaction transaction = new Transaction(document))
            {
                transaction.Start("Изоляция");
                foreach (var pipe in pipes)
                {

                    PipeInsulation.Create(document,
                              pipe.Id,
                              insulationType.Id,
                              ConvertUnit(document, diameter));
                }
                transaction.Commit();
            }
        }
    }
}
