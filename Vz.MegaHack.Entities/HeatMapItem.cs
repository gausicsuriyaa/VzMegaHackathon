using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vz.MegaHack.Entities {
    public class HeatMapItem {
        public string AgentId { get; set; }
        public string AgentName { get; set; }
        public string KpiId { get; set; }
        public string KpiName { get; set; }
        public int KpiScore { get; set; }

        public override string ToString() {
            return string.Format("Agent:{0}, KPI:{1}, Score={2}", AgentName, KpiName, KpiScore);
        }
    }
}
