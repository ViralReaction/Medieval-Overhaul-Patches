using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Verse;

namespace MO_Patches
{
    public class PatchOperation_ToggleSettings : PatchOperation
    {
        public List<string> settings;
        public bool inverse = false;
        private readonly List<PatchOperation> operations = new List<PatchOperation>();
        public PatchOperation active;

        public PatchOperation inactive;

        protected override bool ApplyWorker(XmlDocument xml)
        {
            if (HasSetting())
            {
                if (active != null)
                {
                    return active.Apply(xml);
                }
            }
            else if (inactive != null)
            {
                return inactive.Apply(xml);
            }
            return true;
        }

        public bool HasSetting()
        {
            for (int i = 0; i < settings.Count(); i++)
            {
                if (Mod_Patches.settings.toggleSettings.Contains(settings[i]))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
