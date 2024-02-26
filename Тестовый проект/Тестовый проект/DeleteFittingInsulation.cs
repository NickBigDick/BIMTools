using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
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
    internal class DeleteFittingInsulation : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            var app = commandData.Application;

            using (Transaction transaction = new Transaction(doc, "Delete fitting insulation"))
            {
                transaction.Start();
                var fitinsnum = 0;
                var accesinsnum = 0;
                var results = new FilteredElementCollector(doc)
                    .OfClass(typeof(PipeInsulation))
                    .WhereElementIsNotElementType().Cast<PipeInsulation>()
                    .Where(el => doc.GetElement(el.HostElementId).Category.Name == "Соединительные детали трубопроводов");
                fitinsnum = results.Count();
                results.ToList().ForEach(element => doc.Delete(element.Id));
                
                results = new FilteredElementCollector(doc)
                    .OfClass(typeof(PipeInsulation))
                    .WhereElementIsNotElementType().Cast<PipeInsulation>()
                    .Where(el => doc.GetElement(el.HostElementId).Category.Name == "Арматура трубопроводов");
                accesinsnum = results.Count();
                results.ToList().ForEach(element => doc.Delete(element.Id));
                TaskDialog.Show("инфо", $"Арматуры - {accesinsnum} \n Соединительные детали - {fitinsnum}");
                transaction.Commit();
            }
            
            return Result.Succeeded;
        }
    }
}
