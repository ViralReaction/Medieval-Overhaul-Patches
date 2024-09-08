using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MO_Patches
{
    [StaticConstructorOnStartup]
    public class Utilities
    {
        public static bool CEIsEnabled = LoadedModManager.RunningModsListForReading.Any<ModContentPack>((Predicate<ModContentPack>)(x => x.PackageId == "ceteam.combatextended"));
        public static bool MOToggleStuffIsEnabled = LoadedModManager.RunningModsListForReading.Any<ModContentPack>((Predicate<ModContentPack>)(x => x.PackageId == "accurex.medievaloverhaul.stuff"));
        public static bool RimFantasyIsEnabled = LoadedModManager.RunningModsListForReading.Any<ModContentPack>((Predicate<ModContentPack>)(x => x.PackageId == "sierra.rf.medievaloverhaul"));
        public static bool GiddyUpIsEnabled = LoadedModManager.RunningModsListForReading.Any<ModContentPack>((Predicate<ModContentPack>)(x => x.PackageId == "owlchemist.giddyup"));
        public static bool TribalMedicineIsEnabled = LoadedModManager.RunningModsListForReading.Any<ModContentPack>((Predicate<ModContentPack>)(x => x.PackageId == "argon.tribalmedicineremake"));
        public static ResearchProjectDef ChangeResearch(ResearchProjectDef researchDef, int researchCost)
        {
            string defName = researchDef.defName;
            ResearchProjectDef def = (DefDatabase<ResearchProjectDef>.GetNamed(defName, true));
            def.baseCost = researchCost;
            return def;
        }
    }
}
