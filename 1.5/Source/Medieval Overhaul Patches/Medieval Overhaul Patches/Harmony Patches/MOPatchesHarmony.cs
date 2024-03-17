using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Verse;
using ProcessorFramework;

namespace Medieval_Overhaul_Patches
{
    [StaticConstructorOnStartup]
    internal static class MOPatchesHarmony
    {
        static MOPatchesHarmony()
        {
            Harmony harmony = new Harmony("rw.mopatches");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            harmony.Patch((MethodBase)AccessTools.Method(typeof(CompProcessor), "TakeOutProduct"), postfix: new HarmonyMethod(typeof(MOPatchesHarmony), "TakeOutProduct_Postfix"));
        }

        public static void TakeOutProduct_Postfix(ActiveProcess activeProcess, ref Thing __result)
        {
            if ((__result.TryGetComp<CompIngredients>() is CompIngredients compIngredients) && compIngredients.ingredients.NullOrEmpty())
            {
                List<Thing> ingredientThingList = activeProcess.ingredientThings;
                for (int index = 0; index < ingredientThingList.Count; index++)
                {
                    Thing ingredientThing = ingredientThingList[index];
                    compIngredients.RegisterIngredient(ingredientThing.def);

                }
            }
        }
    }
}
