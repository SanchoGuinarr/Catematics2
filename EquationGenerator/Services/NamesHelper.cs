using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatematicsMnaui.Services
{
    public static class NamesHelper
    {
        public static string[] ComputingObjectsLevelNames = new string[]
        {
            "studentík",
            "student",
            "premiant",
            "počtář",
            "učitel",
            "inženýr",
            "matematik",
            "asistent",
            "docent",
            "profesor",
        };
        
        public static string GetCurentLevelName(int level, int maxLevel)
        {
            // e.g. if max level is 30 there are 10 level names sub level is 3
            // therefore every ComputingObjectsLevelNames will have 3 sub levels
            // so lvels goes as folows: studentík 1 úrovně, studentík 2. úrovně, studentík 3.úrovně, student 1. úrovně, student 3. úrovně... 
            int subLevel = maxLevel / ComputingObjectsLevelNames.Length; 
            int index = level / subLevel;
            int subLevelIndex = level % subLevel;
            string subLevelIndexString = subLevelIndex == 0 ? "" : " " + subLevelIndex.ToString() + ". úrovně";
            return ComputingObjectsLevelNames[index] + subLevelIndexString;
        }
    }
}
