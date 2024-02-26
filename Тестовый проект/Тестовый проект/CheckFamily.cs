using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Тестовый_проект
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    internal class CheckFamily : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            var checker = new FilteredElementCollector(doc).OfClass(typeof(Family)).Cast<Family>().Any(f => f.Name.Contains("с КВ") && f.GetFamilySymbolIds().Select(doc.GetElement).Any(e => e.Name.Contains("без КВ")));
            TaskDialog.Show("Инфо", checker.ToString());

            return Result.Succeeded;
        }
    }
}
