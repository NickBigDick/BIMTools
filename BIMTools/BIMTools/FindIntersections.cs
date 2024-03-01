using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //get active view
            View view = doc.ActiveView;

            //create categories
            List<ElementId> categories = new List<ElementId>()
            {
                new ElementId(BuiltInCategory.OST_DuctAccessory),
                new ElementId(BuiltInCategory.OST_DuctCurves),
                new ElementId(BuiltInCategory.OST_DuctFitting),
                new ElementId(BuiltInCategory.OST_DuctTerminal),
                //new ElementId(BuiltInCategory.OST_DuctFittingInsulation),
                //new ElementId(BuiltInCategory.OST_DuctInsulations),
                new ElementId(BuiltInCategory.OST_PipeAccessory),
                new ElementId(BuiltInCategory.OST_PipeCurves),
                new ElementId(BuiltInCategory.OST_PipeFitting),
                //new ElementId(BuiltInCategory.OST_PipeFittingInsulation),
                //new ElementId(BuiltInCategory.OST_PipeInsulations),

            };
            var elementMulticategoryFilter = new ElementMulticategoryFilter(categories);

            var categoryFEC = new FilteredElementCollector(doc, view.Id).WherePasses(elementMulticategoryFilter);
            var categoryelelems = categoryFEC.ToElements();
            ElementClassFilter instFilter = new ElementClassFilter(typeof(FamilyInstance));
            ElementClassFilter notinstFilter = new ElementClassFilter(typeof(Pipe));
            var notinst = categoryFEC.WherePasses(notinstFilter).ToElements().ToList();
            var insts = categoryFEC.WherePasses(instFilter).Cast<FamilyInstance>().Where(e => e.SuperComponent is null).Select(e => e as Element).ToList();
            var elems = insts.Concat(notinst);


            Options options = new Options();
            using (Transaction transaction = new Transaction(doc))
            {
                transaction.Start("FindIntersections");
                foreach (Element elem in elems)
                {
                    ElementIntersectsElementFilter elementFilter = new ElementIntersectsElementFilter(elem);
                    LogicalAndFilter lAF = new LogicalAndFilter(elementMulticategoryFilter, elementFilter);
                    FilteredElementCollector FEC = new FilteredElementCollector(doc).WherePasses(lAF);
                    if (FEC.Any())
                    {
                        elem.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS).Set("Пересекается");
                    }
                    else
                    {
                        elem.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS).Set(" ");

                    }
                }
                transaction.Commit();
            }

            var parameterFilter = new FilteredElementCollector(doc).OfClass(typeof(ParameterFilterElement)).Cast<ParameterFilterElement>().Where(f => f.Name == "Фильтр пересечений").FirstOrDefault();
            


            //create rules
            ElementId parameterId = new ElementId(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS);
            FilterRule rule = ParameterFilterRuleFactory.CreateContainsRule(parameterId, "Пересекается", false);

            //create logicalandfilter
            ElementParameterFilter filter = new ElementParameterFilter(rule, true);
            LogicalAndFilter logicalAndFilter = new LogicalAndFilter(new[] { filter });

            using (Transaction transaction = new Transaction(doc))
            {
                transaction.Start("Create and apply filter");
                //createparameterfilter
                if (parameterFilter == default(ParameterFilterElement))
                {
                    parameterFilter = ParameterFilterElement.Create(doc, "Фильтр пересечений", categories);
                    parameterFilter.SetElementFilter(logicalAndFilter);
                }
                //setgrafic override
                OverrideGraphicSettings overrides = new OverrideGraphicSettings();
                overrides.SetCutLineColor(new Color(255, 0, 0));
                //apply filter to view
                if (!view.GetFilters().Contains(parameterFilter.Id))
                {
                    view.AddFilter(parameterFilter.Id);
                }
                view.SetFilterVisibility(parameterFilter.Id, false);
                view.SetFilterOverrides(parameterFilter.Id, overrides);
                transaction.Commit();

            }

            return Result.Succeeded;
        }
    }
}
