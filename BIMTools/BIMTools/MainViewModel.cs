using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace BIMTools
{
    public class MainViewModel
    {
        public MainViewModel(Autodesk.Revit.DB.Reference reference)
        {
            CollectParameters(reference);
        }
        public string Prefix {  get; set; }
        public string StartValue { get;set; }
        public List<Parameter> Parameters { get; set; } = new List<Parameter> { };
        public Parameter SelectedParameter { get; set; }

        public void CollectParameters(Autodesk.Revit.DB.Reference reference)
        {
            var element = MyRevitAPI.Document.GetElement(reference);
            var elementParameters = element.Parameters;
            foreach ( Parameter parameter in elementParameters )
            {
                if (!parameter.IsReadOnly && parameter.StorageType != StorageType.ElementId)
                {
                    Parameters.Add(parameter);
                }
            }
        }
    }

}
