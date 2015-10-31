using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vz.MegaHack.Entities {
    public class CenterViewResponse {
        public string CenterName { get; set; }
        public List<KPIItem> CenterView { get; set; }
    }
}
