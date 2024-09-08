using RimWorld;
using Verse;

namespace MO_Patches
{
    [DefOf]
    public static class MOPatchDefOf
    {
        public static ResearchProjectDef Esoteric_Studies;
        static MOPatchDefOf() => DefOfHelper.EnsureInitializedInCtor(typeof(MOPatchDefOf));
    }
}
