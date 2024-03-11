using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
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
    internal class SystemsIsolation : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document document = commandData.Application.ActiveUIDocument.Document;
            UIDocument uidocument = commandData.Application.ActiveUIDocument;
            SystemsIsolationWindow window = new SystemsIsolationWindow();
            window.document = document;
            window.uidocument = uidocument;

            window.ShowDialog();
            return Result.Succeeded;
        }
    }
}
