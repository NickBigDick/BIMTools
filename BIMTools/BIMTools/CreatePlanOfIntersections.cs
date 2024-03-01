using Autodesk.Revit.Attributes;
using Autodesk.Revit.Creation;
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
    internal class CreatePlanOfIntersections : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var doc = commandData.Application.ActiveUIDocument.Document;
            var app = commandData.Application.Application;

            var elems = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_DuctCurves).WhereElementIsNotElementType().ToElements();
            var options = new Options();
            foreach ( var elem in elems )
            {
                var filter = new ElementIntersectsElementFilter( elem );
                var FEC = new FilteredElementCollector(doc).WherePasses(filter);
                if ( FEC != null)
                {
                    TaskDialog.Show("Info", FEC.First().Name);

                }
            }
            return Result.Succeeded;
        }
    }
}
