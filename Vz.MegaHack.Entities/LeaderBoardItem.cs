using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vz.MegaHack.Entities {

    public class LeaderBoardItem {
        public string Name { get; set;  }
        public int TotalPoints { get; set; }
        public int Rank { get; set; }
        public string PhotoFileName { get; set; }

        public override string ToString() {
            return string.Format("Rank:{0}, Name:{1}, Points={2}, PhotoPath={3}", Rank, Name, TotalPoints, PhotoFileName);
        }
    }
}
