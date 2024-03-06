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
    internal class PlaceFamilyInstances : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            PlaceFamilyInstancesWindow window = new PlaceFamilyInstancesWindow();
            window.document = doc;
            window.uidocument = uidoc;
            var elementId = uidoc.Selection.PickObject(ObjectType.Element, "Выберите экземпляр семейства для расстановки").ElementId;
            window.elementId = elementId;

            window.ShowDialog();
            return Result.Succeeded;
        }
    }
}
