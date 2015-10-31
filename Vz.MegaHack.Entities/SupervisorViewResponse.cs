using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vz.MegaHack.Entities {
    public class SupervisorViewResponse {
        public string SupervisorName { get; set; }
        public List<KPIItem> SupervisorView { get; set; }
    }
}
