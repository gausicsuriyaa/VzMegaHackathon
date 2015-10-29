using Vz.MegaHack.Core;
using Vz.MegaHack.Entities;
using Vz.MegaHack.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vz.MegaHack.Engines
{
    public static class EvaluationEngine
    {
        
        public static List<LeaderBoardItem> GetLeaderBoard(string centerId) {
            List<LeaderBoardItem> leaderBoard = new List<LeaderBoardItem>();
            List<AgentItem> agents = AgentReader.GetAgentItems(centerId);

            int rank = 0;
            foreach(AgentItem agent in agents.OrderByDescending(a => a.Points)) {
                leaderBoard.Add(new LeaderBoardItem() {
                    Name = agent.AgentName,
                    TotalPoints = agent.Points,
                    PhotoFileName = agent.PhotoFileName,
                    Rank = ++rank});
            }

            return leaderBoard;
        }

    }
}
