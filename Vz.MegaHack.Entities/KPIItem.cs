using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vz.MegaHack.Entities {
    public class KPIItem {
        public string AgentName { get; set; }
        public int Score { get; set; }
        public string TopKPI { get; set; }
        public string BottomKPI { get; set; }
    }
}
