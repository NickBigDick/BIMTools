using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIMTools
{
    public partial class FamilyManagerParameterViewerWindow : System.Windows.Forms.Form
    {
        public FamilyManagerParameterViewerWindow()
        {
            InitializeComponent();
        }

        public FamilyManager familyManager { get; set; }

        private void FamilyManagerParameterViewerWindow_Load(object sender, EventArgs e)
        {
            var column = new DataGridViewColumn();
            column.HeaderText = "Параметры";
            column.CellTemplate = new DataGridViewTextBoxCell();
            dataGridView1.Columns.Add(column);

            var types = familyManager.Types.Cast<FamilyType>().ToArray();
            var parameters = familyManager.Parameters.Cast<FamilyParameter>().Where(p => p.IsShared);
            foreach (var type in types)
            {
                column = new DataGridViewColumn();
                column.HeaderText = type.Name;
                column.Name = type.Name;
                column.CellTemplate = new DataGridViewTextBoxCell();
                dataGridView1.Columns.Add(column);
            }
        }
    }
}
