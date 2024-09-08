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
        public static ModSettings_MOPatches Instance { get; private set; }
        public static Dictionary<string, bool> settingMode = [];

        // Debug Mode
        public bool debugMode = false;

        // Debloat Settings
        public bool building_bloat = true;
        public bool plant_bloat = true;

        // Individual Tweaks
        public bool alchemical_drug = false;
        public bool medieval_drug = false;
        public bool mo_wild_plants = true;
        
        public bool schematic_gunpowder = false;
        public bool lindwurm_giddyup = true;

        public int esoteric_cost = 1000;
        public int arcane_mo_cost = 1000;
        public int tribal_medicine_mo_cost = 1000;

        // Experimental Toggles
        public bool experimental_patches = false;
        public bool chemfuel_replace = false;
        public bool component_replace = false;

        public enum DarkForestDifficulty : byte
        {
            Easy,
            Normal,
            Hard,
            Nightmare,
            Ultra_Nightmare
        }
        public static DarkForestDifficulty darkForestDifficultyMode = DarkForestDifficulty.Normal;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref building_bloat, "building_bloat", true);
            Scribe_Values.Look(ref plant_bloat, "plant_bloat", true);
            Scribe_Values.Look(ref alchemical_drug, "alchemical_drug", true);
            Scribe_Values.Look(ref alchemical_drug, "medieval_drug", true);
            Scribe_Values.Look(ref mo_wild_plants, "mo_wild_plants", true);
            
            Scribe_Values.Look(ref schematic_gunpowder, "schematic_gunpowder", false);
            Scribe_Values.Look(ref lindwurm_giddyup, "lindwurm_giddyup", true);
            Scribe_Values.Look(ref esoteric_cost, "esoteric_cost", 1000);
            Scribe_Values.Look(ref arcane_mo_cost, "arcane_mo_cost", 1000);
            Scribe_Values.Look(ref tribal_medicine_mo_cost, "tribal_medicine_mo_cost", 1000);
            Scribe_Values.Look(ref darkForestDifficultyMode, "darkForestDifficultyMode", DarkForestDifficulty.Normal);

            Scribe_Values.Look(ref chemfuel_replace, "experimental_patches", false);
            Scribe_Values.Look(ref chemfuel_replace, "chemfuel_replace", false);
            Scribe_Values.Look(ref chemfuel_replace, "component_replace", false);
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
        private Vector2 scrollPosition;
        public void DoSettingsWindowContents(Rect inRect)
        {
            Rect rect = new(inRect.x, inRect.y, inRect.width - 20f, 2000f);
            float contentHeight = 700f;
            Widgets.BeginScrollView(inRect, ref this.scrollPosition, new Rect(inRect.x, inRect.y, rect.width, contentHeight), true);
            Listing_MO_Patches options = new();
            options.Begin(rect);
            options.GapLine();
            Text.Font = GameFont.Medium;
            options.Label("MO_Patches_Settings_Debloat".Translate());
            options.Gap();
            Text.Font = GameFont.Small;
            options.CheckboxLabeled("MO_Patches_Settings_Plant_Deboat_Title".Translate(), ref this.plant_bloat, "MO_Patches_Settings_Plant_Deboat_Desc".Translate());
            options.CheckboxLabeled("MO_Patches_Settings_Building_Deboat_Title".Translate(), ref this.building_bloat, "MO_Patches_Settings_Building_Deboat_Desc".Translate());
            options.GapLine();
            options.Gap();
            Text.Font = GameFont.Medium;
            options.Label("MO_Patches_Individual_Tweak".Translate());
            options.Gap();
            Text.Font = GameFont.Small;
            options.CheckboxLabeled("MO_Patches_Settings_Medieval_Druglab_Title".Translate(), ref this.alchemical_drug, "MO_Patches_Settings_Medieval_Druglab_Desc".Translate());
            options.CheckboxLabeled("MO_Patches_Settings_Medieval_DrugProduction_Title".Translate(), ref this.medieval_drug, "MO_Patches_Settings_Medieval_DrugProduction_Desc".Translate());
            options.CheckboxLabeled("MO_Patches_Settings_Wild_Plants_Title".Translate(), ref this.mo_wild_plants, "MO_Patches_Settings_Wild_Plants_Desc".Translate());
            if (Utilities.GiddyUpIsEnabled)
            {
                options.CheckboxLabeled("MO_Patches_Settings_GiddyUp_Lindwurm_Title".Translate(), ref this.lindwurm_giddyup, "MO_Patches_Settings_GiddyUp_Lindwurm_Desc".Translate());
            }
            if (Utilities.CEIsEnabled)
            {
                options.CheckboxLabeled("MO_Patches_Settings_CE_Gunpowder_Title".Translate(), ref this.schematic_gunpowder, "MO_Patches_Settings_CE_Gunpowder_Desc".Translate());
            }
            var enumActions = new Dictionary<DarkForestDifficulty, Action>
            {
                { DarkForestDifficulty.Easy, () => darkForestDifficultyMode = DarkForestDifficulty.Easy },
                { DarkForestDifficulty.Normal, () => darkForestDifficultyMode = DarkForestDifficulty.Normal },
                { DarkForestDifficulty.Hard, () => darkForestDifficultyMode = DarkForestDifficulty.Hard },
                { DarkForestDifficulty.Nightmare, () => darkForestDifficultyMode = DarkForestDifficulty.Nightmare },
                { DarkForestDifficulty.Ultra_Nightmare, () => darkForestDifficultyMode = DarkForestDifficulty.Ultra_Nightmare }

            };
            options.CustomDropdownLabeledEnum("MO_Patches_Settings_Schrat_Spawn_Title".Translate(), ref darkForestDifficultyMode, enumActions, "MO_Patches_Settings_Schrat_Spawn_Desc".Translate(), "MO_Patches_Settings_", 0f, 1f, 0.25f);
            options.Gap();
            options.GapLine();
            Text.Font = GameFont.Medium;
            options.Label("MO_Patches_Settings_Research".Translate());
            options.Gap();
            Text.Font = GameFont.Small;
            options.CustomIntBoxWithButtons("MO_Patches_Settings_Esoteric_Research_Title".Translate(), ref this.esoteric_cost, 500, 10000, 100, "MO_Patches_Settings_Esoteric_Research_Desc".Translate());
            if (Utilities.RimFantasyIsEnabled)
            {
                options.CustomIntBoxWithButtons("MO_Patches_Settings_RimFantasy_Research_Title".Translate(), ref this.arcane_mo_cost, 500, 10000, 100, "MO_Patches_Settings_RimFantasy_Research_Desc".Translate());
            }
            if (Utilities.TribalMedicineIsEnabled)
            {
                options.CustomIntBoxWithButtons("MO_Patches_Settings_TribalMedicine_Research_Title".Translate(), ref this.tribal_medicine_mo_cost, 500, 10000, 100, "MO_Patches_Settings_TribalMedicine_Research_Desc".Translate());
            }
            options.Gap();
            options.GapLine();
            Text.Font = GameFont.Medium;
            options.CheckboxLabeled("MO_Patches_Settings_Experimental".Translate(), ref this.experimental_patches, "MO_Patches_Settings_Experimental_Desc".Translate());
            Text.Font = GameFont.Small;
            options.Gap();
            if (experimental_patches)
            {
                options.CheckboxLabeled("MO_Patches_Settings_Chemfuel_Replace_Title".Translate(), ref this.chemfuel_replace, "MO_Patches_Settings_Chemfuel_Replace_Desc".Translate());
                options.CheckboxLabeled("MO_Patches_Settings_Component_Replace_Title".Translate(), ref this.component_replace, "MO_Patches_Settings_Component_Replace_Desc".Translate());
            }

            // Reset to Default Button
            options.GapLine();
            options.Gap();
            if (options.ButtonText("Reset to Defaults"))
            {
                ResetSettingsToDefault();
            }
            contentHeight = options.CurHeight +100f;
            options.End();
            Widgets.EndScrollView();
            rect.height = contentHeight;
        }
        public void ResetSettingsToDefault()
        {
            plant_bloat = true;
            building_bloat = true;
            alchemical_drug = false;
            medieval_drug = false;
            mo_wild_plants = true;
            lindwurm_giddyup = true;
            schematic_gunpowder = false;

            esoteric_cost = 1000;
            arcane_mo_cost = 1000;
            tribal_medicine_mo_cost = 1000;

            darkForestDifficultyMode = DarkForestDifficulty.Normal;

            experimental_patches = false;
            chemfuel_replace = false;
            component_replace = false;

        }
    }
}