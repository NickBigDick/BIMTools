using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Linq;

namespace BIMTools
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    internal class CopyParametersFromSample : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //1. Получить sample
            Document doc = commandData.Application.ActiveUIDocument.Document;
            var app = commandData.Application.Application;
            var sample = app.Documents.Cast<Document>().Where(d => d.Title != doc.Title).First();

            //2. Получить общие параметры sample
            FamilyManager fmSample = sample.FamilyManager;
            var parametersSample = fmSample.Parameters.Cast<FamilyParameter>().Where(p => p.IsShared);
            //3. Выбрать только те, которых нет в тек. документе
            FamilyManager fmDoc = doc.FamilyManager;
            var parametersDoc = fmDoc.Parameters.Cast<FamilyParameter>().Where(p => p.IsShared);
            var parameterNamesDoc = parametersDoc.Select(p => p.Definition.Name).ToArray();
            var parametersToAdd = parametersSample.Where(p => !parameterNamesDoc.Contains(p.Definition.Name));
            //3. Найти параметры в ФОП
            var shfile = app.OpenSharedParameterFile();
            DefinitionGroup[] definitionGroups = shfile.Groups.ToArray();
            var definitions = definitionGroups.SelectMany(dg => dg.Definitions.Cast<ExternalDefinition>().ToArray()).ToArray();
            //4. Добавить в тек семейство
            using (Transaction transaction = new Transaction(doc))
            {
                transaction.Start("AddParameter");
                foreach (var parameter in parametersToAdd)
                {
                    var definition = definitions.First(d => d.Name == parameter.Definition.Name);
                    var parameterGroup = parameter.Definition.ParameterGroup;
                    var instOrType = parameter.IsInstance;
                    fmDoc.AddParameter(definition, parameterGroup, instOrType);
                }
                transaction.Commit();
            }



            return Result.Succeeded;
        }

    }
}
