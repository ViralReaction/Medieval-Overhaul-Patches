using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MO_Patches
{
    public class Utilities
    {
        public static ResearchProjectDef ChangeResearch (ResearchProjectDef researchDef, int researchCost)
        {
            string defName = researchDef.defName;
            ResearchProjectDef def = (DefDatabase<ResearchProjectDef>.GetNamed(defName, true));
            def.baseCost = researchCost;
            return def;
        }
    }
}
