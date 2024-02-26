using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Linq;


namespace Тестовый_проект
{
    [Regeneration(RegenerationOption.Manual)]
    [Transaction(TransactionMode.Manual)]
    public class CurrentTask : IExternalCommand
    {
        public static double convertInt(int value)
        {
            return UnitUtils.ConvertToInternalUnits(value, DisplayUnitType.DUT_MILLIMETERS);

        }

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            var scriptInfo = "";
            var parameter = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_ProjectInformation)
                .First().LookupParameter("BI_Требования BIM");
            scriptInfo += $"Проверка BI_Требования BIM: {(parameter.AsString() == "BIM 1.0ГПМСК").ToString()}\n";
            //TaskDialog.Show("Проверка BI_Требования BIM", (parameter.AsString() == "BIM 1.0ГПМСК").ToString());

            var grouptypes = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_IOSModelGroups).WhereElementIsElementType();
            scriptInfo += $"{(grouptypes.Count() > 1 ? "Групп больше чем одна. Удалите" : "Число групп в проекте = 1")}\n";
            //TaskDialog.Show("Количество групп в проекте", (grouptypes.Count() > 1? "Групп больше чем одна. Удалите": "Число групп в проекте = 1"));

            var groupinsts = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_IOSModelGroups).WhereElementIsNotElementType();
            scriptInfo += $"Количество экземпляров групп в проекте: {(groupinsts.Count() > 1 ? "Экземпляров групп больше чем одна. Удалите" : "Число экземпляров групп в проекте = 1")}\n";
            //TaskDialog.Show("Количество экземпляров групп в проекте", (groupinsts.Count() > 1 ? "Экземпляров групп больше чем одна. Удалите" : "Число экземпляров групп в проекте = 1"));

            //TaskDialog.Show("Сравнение имени групппы и проекта", (grouptypes.First().Name == doc.Title? "Имя группы совпадает с именем файла": "Имя группы не совпадает с именем файла"));

            var locationPoint = groupinsts.FirstElement().Location as LocationPoint;
            scriptInfo += $"Проверка точки вставки группы: {(locationPoint.Point.IsAlmostEqualTo(new XYZ()) ? "Точки вставки группы совпадает с началом координат" : "Точки вставки группы НЕ совпадает с началом координат")}\n";
            //TaskDialog.Show("Проверка точки вставки группы", (locationPoint.Point.IsAlmostEqualTo(new XYZ())? "Точки вставки группы совпадает с началом координат" : "Точки вставки группы НЕ совпадает с началом координат"));
                    
            var familyInsts = new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance)).WhereElementIsViewIndependent();
            var allInstBelongsToGroup = familyInsts.Where(el => el.GroupId.IntegerValue == -1).Count() == 0;
            scriptInfo += $"Проверка принадлежности всех элементов одной группе: {(allInstBelongsToGroup.ToString())}\n";
            //TaskDialog.Show("Проверка принадлежности всех элементов одной группе", allInstBelongsToGroup.ToString());
           
            var base_levelId = familyInsts.FirstElement().LevelId;
            var commonBaseLevel = familyInsts.ToElements().All(el => el.LevelId == base_levelId);
            scriptInfo += $"Проверка принадлежности всех элементов одному уровню: {commonBaseLevel.ToString()}\n";
            //TaskDialog.Show("Проверка принадлежности всех элементов одному уровню", commonBaseLevel.ToString());

            var families = new FilteredElementCollector(doc).OfClass(typeof(Family)).Cast<Family>().Where(f => char.IsDigit(f.Name.Last()) & char.IsWhiteSpace(f.Name[f.Name.Length - 2])) ;
            scriptInfo += $"Семейства похожие на дубликаты: \n{string.Join("\n", families.Select(f => f.Name))}\n";
            //TaskDialog.Show("Семейства похожие на дубликаты", string.Join("\n", families.Select(f => f.Name)));

            TaskDialog.Show("Отчет", scriptInfo);

            using (Transaction transaction = new Transaction(doc))
            {
                transaction.Start("Delete Detail groups");

                var detgroups = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_IOSDetailGroups);
                doc.Delete(detgroups.ToElementIds());

                transaction.Commit();
            }
            return Result.Succeeded;
        }
    }
}
