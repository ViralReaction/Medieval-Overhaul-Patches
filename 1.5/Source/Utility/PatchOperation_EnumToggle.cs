using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using System.Xml;
using System.Reflection;

namespace MO_Patches
{
    public class PatchOperation_EnumToggle : PatchOperation
    {
        public string enumTypeString;  // The enum type as a string (e.g., "MO_Patches.DarkForestDifficulty")
        public string enumSettingName; // The name of the enum field in the settings (e.g., "CurrentEnumSetting")
        public string settingsTypeString;  // The settings class that holds the enum (e.g., "MO_Patches.ModSettings_MOPatches")

        public List<PatchOperationMapping> operationMapping;  // List of mappings between enum value and operations

        protected override bool ApplyWorker(XmlDocument xml)
        {
            // Dynamically get the current enum value
            object currentEnumSetting = GetCurrentEnumSetting(enumSettingName, settingsTypeString);
            if (currentEnumSetting == null)
            {
                Log.Error("Current enum setting could not be retrieved.");
                return false;
            }

            string enumValue = currentEnumSetting.ToString();

            // Find the corresponding PatchOperation for the current enum value
            foreach (var mapping in operationMapping)
            {
                if (mapping.enumValue == enumValue)
                {
                    return mapping.patchOperation.Apply(xml);
                }
            }

            Log.Warning($"No patch operation found for enum value {enumValue}.");
            return false;
        }

        private object GetCurrentEnumSetting(string enumSettingName, string settingsTypeString)
        {
            // Get the settings type dynamically
            Type settingsType = Type.GetType(settingsTypeString);
            if (settingsType == null)
            {
                Log.Error($"Invalid settings type: {settingsTypeString}");
                return null;
            }

            // Use reflection to find the field or property containing the enum value
            var enumField = settingsType.GetField(enumSettingName, BindingFlags.Public | BindingFlags.Static);
            if (enumField != null)
            {
                return enumField.GetValue(null);
            }

            var enumProperty = settingsType.GetProperty(enumSettingName, BindingFlags.Public | BindingFlags.Static);
            if (enumProperty != null)
            {
                return enumProperty.GetValue(null);
            }

            Log.Error($"Could not find enum setting: {enumSettingName} in {settingsTypeString}");
            return null;
        }
    }

    // Class to hold the mapping of enum values to patch operations
    public class PatchOperationMapping
    {
        public string enumValue;
        public PatchOperation patchOperation;
    }

}