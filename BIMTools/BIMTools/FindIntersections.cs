using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;


namespace BIMTools
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    internal class FindIntersections: IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            Autodesk.Revit.ApplicationServices.Application app = commandData.Application.Application;
            FindIntersectionsWindow window = new FindIntersectionsWindow();
            window.currentdocument = doc;
            //get active view
            View view = doc.ActiveView;
            window.view = view;

            //create categories
            List<ElementId> categories = new List<ElementId>()
            {
                new ElementId(BuiltInCategory.OST_DuctAccessory),
                new ElementId(BuiltInCategory.OST_DuctCurves),
                new ElementId(BuiltInCategory.OST_DuctFitting),
                new ElementId(BuiltInCategory.OST_DuctTerminal),
                new ElementId(BuiltInCategory.OST_DuctInsulations),
                new ElementId(BuiltInCategory.OST_PipeAccessory),
                new ElementId(BuiltInCategory.OST_PipeCurves),
                new ElementId(BuiltInCategory.OST_PipeFitting),
                new ElementId(BuiltInCategory.OST_PipeInsulations),
                new ElementId(BuiltInCategory.OST_MechanicalEquipment),
                new ElementId(BuiltInCategory.OST_PlumbingFixtures),
                new ElementId(BuiltInCategory.OST_FlexPipeCurves),
            };
            window.categories = categories;



            window.ShowDialog();
            return Result.Succeeded;
        }
    }
}
