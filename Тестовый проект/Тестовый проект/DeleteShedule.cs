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
    internal class DeleteShedule : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            var app = commandData.Application;

            using (Transaction transaction = new Transaction(doc, "Delete fitting insulation"))
            {
                transaction.Start();
                new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Schedules).Where(s => s.Name == "00_ВК_Обязательные параметры").ToList().ForEach(e => doc.Delete(e.Id));

                transaction.Commit();
            }
            TaskDialog.Show("HSD", "dfg");
            return Result.Succeeded;
        }
    }
}
