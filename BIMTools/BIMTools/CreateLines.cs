using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIMTools
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    internal class CreateLines : IExternalCommand
    {
        public static double convertInt(int value)
        {
            return UnitUtils.ConvertToInternalUnits(value, DisplayUnitType.DUT_MILLIMETERS);
        }
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            View view = doc.ActiveView as View;

            Line line1 = Line.CreateBound(new XYZ(convertInt(-500), 0, 0), new XYZ(convertInt(500), 0, 0));
            Line line2 = Line.CreateBound(new XYZ(0, convertInt(-500), 0), new XYZ(0, convertInt(500), 0));
            CurveArray curvearray = new CurveArray() { };
            curvearray.Append(line1); curvearray.Append(line2);
            var cat = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_LinesHiddenLines);
            using (Transaction transaction = new Transaction(doc, "Create Line"))
            {
                transaction.Start();
                ModelCurveArray newcurvearray = doc.Create.NewModelCurveArray(curvearray, view.SketchPlane);
                foreach (ModelCurve curve in newcurvearray)
                {
                    curve.LineStyle = curve.GetLineStyleIds().Select(doc.GetElement).Where(e => e.Name == "Невидимые линии").First();

                }
                transaction.Commit();
            }

            TaskDialog.Show("Инфо", $"Успешно создал линии");
            return Result.Succeeded;
        }
    }
}
