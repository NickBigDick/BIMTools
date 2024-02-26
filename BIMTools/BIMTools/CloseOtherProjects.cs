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
    internal class CloseOtherProjects : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var app = commandData.Application.Application;
            var doc = commandData.Application.ActiveUIDocument.Document;
            var documents = app.Documents.Cast<Document>().Where(d => d.Title != doc.Title);
            foreach (var item in documents)
            {
                item.Close(false);
            }
            return Result.Succeeded;
        }
    }
}
