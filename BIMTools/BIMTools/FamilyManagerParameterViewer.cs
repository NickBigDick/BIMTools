using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Linq;
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

            window.dataGridView1.AllowUserToAddRows = true;
            var column = new DataGridViewColumn();
            column.HeaderText = "Типоразмеры";
            column.Name = "tiporazmer";
            column.CellTemplate = new DataGridViewTextBoxCell();
            column.Frozen = true;
            window.dataGridView1.Columns.Add(column);

            var types = fmDoc.Types.Cast<FamilyType>().OrderBy(t => t.Name).ToArray();
            //  Заполняем общие параметры
            var familyParameters = fmDoc.Parameters.Cast<FamilyParameter>().Where(p => doc.GetElement(p.Id) != null).OrderBy(p => !p.IsShared).ThenBy(p => p.Definition.Name).ToArray();
            CreateCells(window, familyParameters, types);


            window.ShowDialog();
            return Result.Succeeded;
        }

        public void CreateCells(FamilyManagerParameterViewerWindow window, FamilyParameter[] parameters, FamilyType[] types)
        {

            foreach (var parameter in parameters)
            {
                var suffix = parameter.IsShared ? "общий" : "семейство";
                var column = new DataGridViewColumn();
                column.HeaderText = parameter.Definition.Name;
                column.Name = parameter.Definition.Name + suffix;
                column.CellTemplate = new DataGridViewTextBoxCell();
                window.dataGridView1.Columns.Add(column);
            }

            using (Transaction transaction = new Transaction(window.document))
            {
                transaction.Start("CreateTable");
                foreach (var type in types)
                {
                    int rowId = window.dataGridView1.Rows.Add();
                    DataGridViewRow row = window.dataGridView1.Rows[rowId];
                    row.Cells["tiporazmer"].Value = type.Name;
                    foreach (var parameter in parameters)
                    {
                        var suffix = parameter.IsShared ? "общий" : "семейство";
                        row.Cells[parameter.Definition.Name + suffix].Value = getParameterValue(type, parameter);
                        if (parameter.IsDeterminedByFormula)
                        {
                            row.Cells[parameter.Definition.Name + suffix].ReadOnly = true;
                            row.Cells[parameter.Definition.Name + suffix].Style.ForeColor = System.Drawing.Color.Gray;
                        }
                    }
                }
                transaction.Commit();
            }
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
                double value = (double)familyType.AsDouble(parameter);
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
