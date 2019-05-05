using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatNameChanger
{
    public class configuration : IRocketPluginConfiguration
    {
        public bool Enabled;
        public bool Announce_Name_Change;

        public void LoadDefaults()
        {
            Enabled = true;
            Announce_Name_Change = true;
        }
    }
}
