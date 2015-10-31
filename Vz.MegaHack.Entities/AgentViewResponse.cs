using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vz.MegaHack.Entities {
    public class AgentViewResponse {
        public string AgentName { get; set; }
        public List<AgentKPIScore> AgentView { get; set; }
    }
}
