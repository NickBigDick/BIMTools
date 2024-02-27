using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BIMTools
{
    internal class Application : IExternalApplication

    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            string assemblyPath = Assembly.GetExecutingAssembly().Location;

            string tabName = "BIMToolsTab";
            application.CreateRibbonTab(tabName);
            RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, "BIMМастер");
            AddButton(ribbonPanel, "Task", assemblyPath, $"{nameof(BIMTools)}.{nameof(BIMTools.CreateLines)}", "Текущая задача", "icons8-алмазный-уход-24");
            AddButton(ribbonPanel, "PlaceFamilySymbols", assemblyPath, $"{nameof(BIMTools)}.{nameof(BIMTools.PlaceFamilySymbols)}", "Расставляет выбранные типоразмеры", "icons8-алмазный-уход-24");
            AddButton(ribbonPanel, "CloseOtherDocuments", assemblyPath, $"{nameof(BIMTools)}.{nameof(BIMTools.CloseOtherProjects)}", "Закрывает не активные документы", "CloseOtherProjects24");

            return Result.Succeeded;
        }

        private void AddButton(RibbonPanel ribbonPanel, string buttonName, string path, string linkToCommand, string toolTip, string imageName)
        {
            PushButtonData buttonData = new PushButtonData(
                buttonName, buttonName, path, linkToCommand);
            PushButton button = ribbonPanel.AddItem(buttonData) as PushButton;
            button.ToolTip = toolTip;
            button.LargeImage = (ImageSource)new BitmapImage(new Uri($@"/BIMTools;component/Resources/{imageName}.png", UriKind.RelativeOrAbsolute)); 
        }
    }
}
