using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIMTools
{
    public partial class FamilyManagerParameterViewerWindow : System.Windows.Forms.Form
    {
        public FamilyManager familyManager { get; set; }
        public Document document { get; set; }
        public FamilyManagerParameterViewerWindow()
        {
            InitializeComponent();

        }
     
    }
}
