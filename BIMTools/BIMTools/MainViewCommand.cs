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
    internal class MainViewCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            MyRevitAPI.Initialize(commandData);
            Autodesk.Revit.DB.Reference reference = MyRevitAPI.UIDocument.Selection.PickObject(ObjectType.Element, "Выберите элемент для сбора параметров");
            var mainViewModel = new MainViewModel(reference);


            var window = new MainView();
            window.ShowDialog();

            return Result.Succeeded;
        }
    }
}
