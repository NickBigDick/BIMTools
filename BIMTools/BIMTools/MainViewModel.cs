using Autodesk.Revit.DB;
using System.Collections.Generic;

namespace BIMTools
{
    public class MainViewModel
    {
        public MainViewModel()
        {

        }
        public string Prefix { get; set; }
        public string StartValue { get; set; }
        public List<Parameter> Parameters { get; set; } = new List<Parameter> { };
        public Parameter SelectedParameter { get; set; }
        public Reference SelectedReference { get; set; }

        public void CollectParameters(Reference reference)
        {
            var element = MyRevitAPI.Document.GetElement(reference);
            var elementParameters = element.Parameters;
            foreach (Parameter parameter in elementParameters)
            {
                if (!parameter.IsReadOnly && parameter.StorageType != StorageType.ElementId)
                {
                    Parameters.Add(parameter);
                }
            }
        }

        public void Numerate()
        {

        }
    }

}
