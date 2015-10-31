using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vz.MegaHack.Entities {
    public class HeatMapViewResponse {
        public string SupervisorName { get; set; }
        public List<Dictionary<string, string>> HeatMapView { get; set; }
    }
}
