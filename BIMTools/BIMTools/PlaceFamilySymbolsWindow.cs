using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;

namespace BIMTools
{
    public partial class PlaceFamilySymbolsWindow : System.Windows.Forms.Form
    {
        public PlaceFamilySymbolsWindow()
        {
            InitializeComponent();
        }

        public Document doc { get; set; }
        public string FamilyName {  get; set; }
        public List<Element> FamilySymbols {  get; set; }

        private void SelectedFamily(object sender, EventArgs e)
        {
            FamilySymbolsCheckedListBox.Items.Clear();
            string familyName = FamiliiesBox.SelectedItem as string;
            var familySymbolsIds = new FilteredElementCollector(doc).OfClass(typeof(Family)).Cast<Family>().Where(f => f.Name == familyName).First().GetFamilySymbolIds();
            FamilySymbols = familySymbolsIds.Select(i => doc.GetElement(i)).ToList();
            foreach ( var familySymbol in FamilySymbols)
            {
                FamilySymbolsCheckedListBox.Items.Add(familySymbol.Name);
            }
            for (int i = 0; i < FamilySymbolsCheckedListBox.Items.Count; i ++)
            {
                FamilySymbolsCheckedListBox.SetItemChecked(i, true);
            }

        }

        private void Start(object sender, EventArgs e)
        {
            var selectedSymbolsNames = FamilySymbolsCheckedListBox.CheckedItems;
            var symbolsToCreate = FamilySymbols.Where(fs => selectedSymbolsNames.Contains(fs.Name)).Cast<FamilySymbol>();
            var point = new XYZ();
            var level = new FilteredElementCollector(doc).OfClass(typeof(Level)).FirstElement();
            using (Transaction transaction = new Transaction(doc))
            {
                transaction.Start("Place Symbols");
                foreach (var symbol in symbolsToCreate)
                {
                    if (!symbol.IsActive)
                    {
                        symbol.Activate();
                        doc.Regenerate();
                    }
                    var instance = doc.Create.NewFamilyInstance(
                        point, symbol, level, StructuralType.UnknownFraming);
                    doc.Regenerate();
                    var bb = instance.get_BoundingBox(null);
                    var off_set = bb.Max.Y - bb.Min.Y;
                    point = new XYZ(point.X, point.Y + off_set, point.Z);
                }
                transaction.Commit();
            };

               
        }
    }
}
