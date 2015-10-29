using Vz.MegaHack.Core;
using Vz.MegaHack.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Vz.MegaHack.Data
{
    public static class KPIReader {
        static string xmlFileName = "KPIInfo.xml";

        public static List<AgentItem> GetAgentKPI(string centerId) {
            List<AgentItem> AgentItems = new List<AgentItem>();

            XDocument doc = XDocument.Load(Path.Combine(PathManager.DataPath, xmlFileName));

            var items = doc.Root.Elements("Agent").Where(i => i.Attribute("centerId").Value.Equals(centerId));

            foreach (var item in items) {
                AgentItems.Add(new AgentItem() {
                    AgentId = Convert.ToString(item.Attribute("id").Value),
                    AgentName = Convert.ToString(item.Attribute("name").Value),
                    Date = Convert.ToDateTime(item.Attribute("date").Value),
                    Points = Convert.ToInt32(item.Attribute("points").Value),
                    PhotoFileName = Convert.ToString(item.Attribute("photoFileName").Value),
                });
            }

            return AgentItems;
        }
    }
}
