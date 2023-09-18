using Verse;
using System.Collections.Generic;

namespace Medieval_Overhaul_Patches
{
    public class PlantProperties : DefModExtension
    {
        public ThingDef secondaryDrop = null;
        public IntRange secondaryDropAmountRange;
        public float secondaryDropChance = 1f;
        public bool secondaryNotWhenLeafless = false;
        public static PlantProperties Get(Def def)
        {
            return def.GetModExtension<PlantProperties>();
        }
    }
}