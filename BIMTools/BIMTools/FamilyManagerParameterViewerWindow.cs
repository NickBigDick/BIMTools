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
        public List<MyCellData> myCellDatas = new List<MyCellData>();
        public class MyCellData
        {
            public FamilyType familyType { get; set; }
            public FamilyParameter familyParameter { get; set; }
            public object value { get; set; }
            public MyCellData(FamilyType familyTypee, FamilyParameter familyParameterr, object valuee)
            {
                familyType = familyTypee;
                familyParameter = familyParameterr;
                value = valuee;
            }
        }
        public FamilyManagerParameterViewerWindow()
        {
            InitializeComponent();

        }

        private void sharedParametersCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
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



        private void CellIsChanged(object sender, DataGridViewCellEventArgs e)
        {

            DataGridViewTextBoxCell cell = (DataGridViewTextBoxCell)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var familyTypeName = dataGridView1.Rows[cell.RowIndex].Cells[0];
            FamilyType familyType = familyManager.Types.Cast<FamilyType>().ToArray().First(t => t.Name == familyTypeName.Value.ToString());
            var parameterName = dataGridView1.Columns[cell.ColumnIndex].Name.Replace("общий", "").Replace("семейство", "");
            FamilyParameter familyParameter = familyManager.get_Parameter(parameterName);
            var value = cell.Value.ToString();
            myCellDatas.Add(new MyCellData(familyType, familyParameter, value));

        }

        private void AcceptButton_Click(object sender, EventArgs e)
        {
            using (Transaction transaction = new Transaction(document))
            {
                transaction.Start("New transaction");
                foreach (var item in myCellDatas)
                {
                    familyManager.CurrentType = item.familyType;
                    setParameterValue(familyManager, item.familyType, item.familyParameter, item.value);
                }
                transaction.Commit();
            }
        }
        public void setParameterValue(FamilyManager familyManager, FamilyType familyType, FamilyParameter parameter, object value)
        {
            var storageType = parameter.StorageType;
            if (storageType == StorageType.Integer)
            {
                familyManager.Set(parameter, int.Parse((string) value));
            }
            else if (storageType == StorageType.Double)
            {
                //familyManager.Set(parameter, UnitUtils.ConvertToInternalUnits((double)value, parameter.DisplayUnitType));
                familyManager.Set(parameter, UnitUtils.ConvertToInternalUnits(double.Parse((string) value), parameter.DisplayUnitType));
            }
            else if (storageType == StorageType.ElementId)
            {
                familyManager.Set(parameter, (ElementId) value);
            }
            else
            {
                familyManager.Set(parameter, (string)value);
            }
        }

    }
}
