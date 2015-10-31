using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vz.MegaHack.Entities {
    public class LeaderBoardResponse {
        public string CenterName { get; set; }
        public List<LeaderBoardItem> LeaderBoard { get; set; }
    }
}
