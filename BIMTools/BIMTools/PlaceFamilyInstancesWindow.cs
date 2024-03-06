using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BIMTools.MyRevitUtils;

namespace BIMTools
{
    public partial class PlaceFamilyInstancesWindow : System.Windows.Forms.Form
    {
        public Document document {  get; set; }
        public UIDocument uidocument {  get; set; }
        public ElementId elementId { get; set; }
        public PlaceFamilyInstancesWindow()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            var element = document.GetElement(elementId) as FamilyInstance;
            var symbol = element.Symbol;
            var startPoint = (element.Location as LocationPoint).Point;
            var nx = int.Parse(counterXBox.Text) == 0 ? 1: int.Parse(counterXBox.Text);
            var ny = int.Parse(counterYBox.Text) == 0 ? 1 : int.Parse(counterYBox.Text);
            var dx = dxBox.Text != "0"? ConvertUnit(document, int.Parse(dxBox.Text)): ConvertUnit(document, int.Parse(maxXBox.Text) / (nx == 1? 1: nx - 1));
            var dy = dyBox.Text != "0"? ConvertUnit(document, int.Parse(dyBox.Text)): ConvertUnit(document, int.Parse(maxYBox.Text) / (ny == 1? 1: ny - 1));
            bool data_check = false;
            if ((nx != (0 | 1) & dx != 0) | (ny != (0 | 1) & dy != 0) )
            {
                data_check = true;
            }
            else
            {
                TaskDialog.Show("Ошибка", "Ошибка ввода данных");
            }
            if (data_check)
            {
                List<XYZ> points = new List<XYZ>();
                for ( var nX = 0; nX < nx; nX++)
                {
                    var dX = startPoint.X + dx * nX;
                    for ( var nY = 0; nY < ny; nY++)
                    {
                        var dY = startPoint.Y + dy * nY;
                        var point = new XYZ(dX, dY, startPoint.Z);
                        points.Add(point);
                    }
                }
                points.RemoveAt(0);
                var level = new FilteredElementCollector(document).OfClass(typeof(Level)).FirstElement();
                using (Transaction transaction = new Transaction(document))
                {
                    transaction.Start("Place Instances");

                    foreach ( var point in points)
                    {
                        var instance = document.Create.NewFamilyInstance(
                                        point, symbol, level, StructuralType.UnknownFraming);
                    }

                    transaction.Commit();
                }
            }
        }


    }
}
