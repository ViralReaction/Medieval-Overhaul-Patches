using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace MO_Patches
{
    public class ModSettings_MOPatches : ModSettings
    {
        public static Dictionary<string, bool> settingMode = [];

        // Debug Mode
        public bool debugMode = false;

        // Debloat Settings
        public bool building_bloat = true;
        public bool plant_bloat = true;

        // Map Generation Stuff
        public bool alchemical_drug = false;
        public bool mo_wild_plants = true;

        public int esoteric_cost = 1000;
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref building_bloat, "building_bloat", true);
            Scribe_Values.Look(ref plant_bloat, "plant_bloat", true);
            Scribe_Values.Look(ref alchemical_drug, "alchemical_drug", true);
            Scribe_Values.Look(ref mo_wild_plants, "mo_wild_plants", true);
            Scribe_Values.Look(ref esoteric_cost, "esoteric_cost", 1000);
            Scribe_Collections.Look(ref settingMode, "settingMode", LookMode.Value, LookMode.Value);

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                settingMode ??= [];
            }

        }
        public IEnumerable<string> ToggleSettings
        {
            get
            {
                return GetType().GetFields().Where(p => p.FieldType == typeof(bool) && (bool)p.GetValue(this)).Select(p => p.Name);
            }
        }

   //     <li Class = "XmlExtensions.Setting.Numeric" >

   //             < key > esoteric_cost </ key >

   //             < label > Esoteric Studies:</label>
			//	<defaultValue>1000</defaultValue>
			//	<min>500</min>
			//	<max>10000</max>
			//</li>
        public void DoSettingsWindowContents(Rect inRect)
        {
            Rect rect = new (inRect.x, inRect.y, inRect.width, inRect.height);
            Listing_MO_Patches options = new ();
            options.Begin(rect);
            options.Label("MO_Patches_Settings_Debloat".Translate());
            options.CheckboxLabeled("MO_Patches_Settings_Plant_Deboat_Title".Translate(), ref this.plant_bloat, "MO_Patches_Settings_Plant_Deboat_Title".Translate());
            options.CheckboxLabeled("MO_Patches_Settings_Building_Deboat_Title".Translate(), ref this.building_bloat, "MO_Patches_Settings_Building_Deboat_Desc".Translate());
            options.GapLine();
            options.Gap();
            options.Label("MO_Patches_Individual_Tweak".Translate());
            options.CheckboxLabeled("MO_Patches_Settings_Medieval_Druglab_Title".Translate(), ref this.alchemical_drug, "MO_Patches_Settings_Medieval_Druglab_Desc".Translate());
            options.CheckboxLabeled("MO_Patches_Settings_Wild_Plants_Title".Translate(), ref this.mo_wild_plants, "MO_Patches_Settings_Wild_Plants_Desc".Translate());
            esoteric_cost = options.CustomSliderLabelInt("MO_Patches_Settings_Esoteric_Research_Title".Translate(), esoteric_cost, 500, 10000, 0.5f, null, "esoteric_cost".ToString(), null, null, -1f);
            if (options.ButtonText("Reset to Defaults"))
            {
                ResetSettingsToDefault();
            }
            options.End();
        }
        public void ResetSettingsToDefault()
        {
            plant_bloat = true;
            building_bloat = true;
            alchemical_drug = false;
            mo_wild_plants = true;
            esoteric_cost = 1000;

        }
    }
}