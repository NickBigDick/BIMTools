using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIMTools
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    internal class PlaceFamilySymbols : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var app = commandData.Application.Application;
            var doc = commandData.Application.ActiveUIDocument.Document;
            var families = new FilteredElementCollector(doc).OfClass(typeof(Family));
            PlaceFamilySymbolsWindow window = new PlaceFamilySymbolsWindow();
            window.doc = doc;
            window.FEC = families;
            foreach (var family in families)
            {
                window.FamiliiesBox.Items.Add(family.Name);
            }


            window.ShowDialog();
            return Result.Succeeded;
        }
    }
}
