using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UnadvancedRobPlugin
{
    public class UnadvancedRobPluginConfig : IRocketPluginConfiguration
    {
        public string MsgColor { get; set; }
        public void LoadDefaults()
        {
            MsgColor = "purple";
        }
    }
}