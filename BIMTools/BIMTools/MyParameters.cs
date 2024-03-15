using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIMTools
{
    internal class MyParameters
    {
        public static object getFamilyParameterValue(FamilyType familyType, FamilyParameter parameter)
        {
            var storageType = parameter.StorageType;
            if (storageType == StorageType.Integer)
            {
                return familyType.AsInteger(parameter);
            }
            else if (storageType == StorageType.Double)
            {
                double value = (double)familyType.AsDouble(parameter);
                return UnitUtils.ConvertFromInternalUnits(value, parameter.GetUnitTypeId());
            }
            else if (storageType == StorageType.ElementId)
            {
                return familyType.AsElementId(parameter);
            }
            else
            {
                return familyType.AsString(parameter);
            }
        }

        public static void setFamilyParameterValue(FamilyManager familyManager, FamilyType familyType, FamilyParameter parameter, object value)
        {
            var storageType = parameter.StorageType;
            if (storageType == StorageType.Integer)
            {
                familyManager.Set(parameter, int.Parse((string)value));
            }
            else if (storageType == StorageType.Double)
            {
                //familyManager.Set(parameter, UnitUtils.ConvertToInternalUnits((double)value, parameter.DisplayUnitType));
                familyManager.Set(parameter, UnitUtils.ConvertToInternalUnits(double.Parse((string)value), parameter.GetUnitTypeId()));
            }
            else if (storageType == StorageType.ElementId)
            {
                familyManager.Set(parameter, (ElementId)value);
            }
            else
            {
                familyManager.Set(parameter, (string)value);
            }
        }
    }
}
