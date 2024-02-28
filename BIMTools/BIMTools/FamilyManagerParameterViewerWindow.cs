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


        private void sharedParametersCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox) sender;
            var columns = dataGridView1.Columns;
            if (!checkBox.Checked)
            {
                foreach (DataGridViewColumn column in columns)
                {
                    if (column.Name.Contains("общий"))
                    {
                        column.Visible = false;
                    }
                }
            }
            else
            {
                foreach (DataGridViewColumn column in columns)
                {
                    if (column.Name.Contains("общий"))
                    {
                        column.Visible = true;
                    }
                }
            }
        }
    }
}
