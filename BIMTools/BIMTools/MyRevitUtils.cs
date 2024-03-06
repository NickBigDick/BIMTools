using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BIMTools
{
    public class MyRevitUtils
    {
        public static double ConvertUnit(Document document, double value, bool toInternal = true, int numberOfDigits = 0)
        {
            var display_units = document.GetUnits().GetFormatOptions(UnitType.UT_Length).DisplayUnits;
            var convertedValue = toInternal? UnitUtils.ConvertToInternalUnits(value, display_units) : UnitUtils.ConvertFromInternalUnits(value, display_units);
            if (numberOfDigits == 0)
            {
                return convertedValue;
            }
            else
            {
                return Math.Round(convertedValue);
            }
        }
    }
}
