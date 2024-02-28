using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIMTools
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    internal class FamilyManagerParameterViewer : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            Autodesk.Revit.ApplicationServices.Application app = commandData.Application.Application;

            var fmDoc = doc.FamilyManager;
            var window = new FamilyManagerParameterViewerWindow();
            window.familyManager = fmDoc;
            window.document = doc;

            window.dataGridView1.AllowUserToAddRows = false;
            var column = new DataGridViewColumn();
            column.HeaderText = "Типоразмеры";
            column.Name = "tiporazmer";
            column.CellTemplate = new DataGridViewTextBoxCell();
            window.dataGridView1.Columns.Add(column);

            var parameters = fmDoc.Parameters.Cast<FamilyParameter>().Where(p => p.IsShared);
            foreach (var parameter in parameters)
            {
                column = new DataGridViewColumn();
                column.HeaderText = parameter.Definition.Name;
                column.Name = parameter.Definition.Name;
                column.CellTemplate = new DataGridViewTextBoxCell();
                window.dataGridView1.Columns.Add(column);
            }

            var types = fmDoc.Types.Cast<FamilyType>();

            using (Transaction transaction = new Transaction(doc))
            {
                transaction.Start("CreateTable");
                foreach (var type in types)
                {
                    int rowId = window.dataGridView1.Rows.Add();
                    DataGridViewRow row = window.dataGridView1.Rows[rowId];
                    row.Cells["tiporazmer"].Value = type.Name;
                    foreach (var parameter in parameters)
                    {
                        row.Cells[parameter.Definition.Name].Value = getParameterValue(type, parameter);
                    }
                }
                transaction.Commit();
            }

            window.Show();

            return Result.Succeeded;
        }

        public object getParameterValue(FamilyType familyType, FamilyParameter parameter)
        {
            var storageType = parameter.StorageType;
            if (storageType == StorageType.Integer)
            {
                return familyType.AsInteger(parameter);
            }
            else if (storageType == StorageType.Double)
            {
                double value = (double) familyType.AsDouble(parameter);
                return UnitUtils.ConvertFromInternalUnits(value, parameter.DisplayUnitType);
            }
            else if (storageType == StorageType.ElementId)
            {
                return familyType.AsElementId(parameter);
            }
            else
            {
                return familyType.AsString(parameter);
            }


        }
    }
}
