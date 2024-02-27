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
        public FamilyManager familyManager { get; set; }
        public Document document { get; set; }
        public FamilyManagerParameterViewerWindow()
        {
            InitializeComponent();

        }


     

        private void getDataButton_Click(object sender, EventArgs e)
        {
            var column = new DataGridViewColumn();
            column.HeaderText = "Типоразмеры";
            column.Name = "tiporazmer";
            column.CellTemplate = new DataGridViewTextBoxCell();
            dataGridView1.Columns.Add(column);
            var parameters = familyManager.Parameters.Cast<FamilyParameter>().Where(p => p.IsShared);
            foreach (var parameter in parameters)
            {
                column = new DataGridViewColumn();
                column.HeaderText = parameter.Definition.Name;
                column.Name = parameter.Definition.Name;
                column.CellTemplate = new DataGridViewTextBoxCell();
                dataGridView1.Columns.Add(column);
            }
            var types = familyManager.Types.Cast<FamilyType>().Select(t => t.Name);
            foreach (var type in types)
            {
                int rowId = dataGridView1.Rows.Add();
                DataGridViewRow row = dataGridView1.Rows[rowId];
                row.Cells["tiporazmer"].Value = type;


            }
            //using (Transaction transaction = new Transaction(document))
            //{
            //    transaction.Start("CreateTable");
            //    transaction.Commit();
            //}
        }
    }
}
