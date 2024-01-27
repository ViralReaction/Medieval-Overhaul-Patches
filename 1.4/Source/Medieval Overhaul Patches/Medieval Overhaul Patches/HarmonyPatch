using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;
using ProcessorFramework;


namespace MyStuff
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = new Harmony("Matsay.MyStuff");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        [HarmonyPatch(typeof(CompProcessor), nameof(CompProcessor.TakeOutProduct))]
        public class IngredientTansfertPatch
        {
            [HarmonyPostfix]
            public static void TakeOutProduct_Postfix(ActiveProcess activeProcess, ref Thing __result)
            {
                if ( (__result.TryGetComp<CompIngredients>() is CompIngredients compIngredients) && compIngredients.ingredients.NullOrEmpty())
                {
                    foreach (Thing ingredientThing in activeProcess.ingredientThings)
                    {
                        compIngredients.RegisterIngredient(ingredientThing.def);
                    }
                }
            }
        }

    }   
}
