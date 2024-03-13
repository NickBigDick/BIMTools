﻿using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIMTools
{
    public static class MyRevitAPI
    {
        public static UIApplication UIApplication { get; set; }
        public static UIDocument UIDocument { get; set; }
        public static Document Document { get; set; }
        public static void Initialize(ExternalCommandData commandData)
        {
            UIApplication = commandData.Application;
        }
    }
}
