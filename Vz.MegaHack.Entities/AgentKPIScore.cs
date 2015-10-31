using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vz.MegaHack.Entities
{
    public class AgentKPIScore
    {
        public string KPIName { get; set; }
        public List<KPIScoreInfo> ScoreDetails { get; set; }

        public override string ToString() {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("KPIName:{0}", KPIName);
            builder.AppendLine();
            if (ScoreDetails != null && ScoreDetails.Count > 0) {
                int scoreIndex = 0;
                foreach (KPIScoreInfo scoreInfo in ScoreDetails) {
                    builder.AppendFormat("Score {0}: {1}; ", ++scoreIndex, scoreInfo.ToString());
                    builder.AppendLine();
                }
                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}
