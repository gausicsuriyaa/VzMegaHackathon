using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vz.MegaHack.Entities
{
    public class KPIScoreInfo
    {
        public string Score { get; set; }
        public string CenterAverage { get; set; }
        public DateTime Date { get; set; }
        public bool HadTraining { get; set; }
        public bool IsAwarded { get; set; }
        public string Description { get; set; }

        public override string ToString() {
            return string.Format("Score:{0}, CenterAverage:{1}, Date:{2}, HadTraining={3}, IsAwarded={4}, Description={5}", 
                Score, CenterAverage, Date, HadTraining, IsAwarded, Description);
        }
    }
}
