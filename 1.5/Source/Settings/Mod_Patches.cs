using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace MO_Patches
{
    public class Mod_Patches : Mod
    {
        public static ModSettings_MOPatches settings;

        public Mod_Patches(ModContentPack content) : base(content)
        {
            settings = GetSettings<ModSettings_MOPatches>();
            PostLoad();
        }

        public override string SettingsCategory()
        {
            return "MO_Patches.ModNameShort".Translate();
        }
        public override void DoSettingsWindowContents(Rect inRect)
        {
            settings.DoSettingsWindowContents(inRect);
        }
        public override void WriteSettings()
        {
            base.WriteSettings();
        }
        public void PostLoad()
        {
            LongEventHandler.ExecuteWhenFinished( () => Utilities.ChangeResearch(MOPatchDefOf.Esoteric_Studies, settings.esoteric_cost));
        }

    }
}
