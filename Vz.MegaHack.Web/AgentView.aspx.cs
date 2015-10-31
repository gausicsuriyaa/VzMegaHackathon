using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vz.MegaHack.Entities;
using Vz.MegaHack.Engines;

namespace Vz.MegaHack.Web
{
    public partial class AgentView : System.Web.UI.Page
    {
        public AgentViewResponse agentViewInfo = new AgentViewResponse();
        public List<string> kpiScores = new List<string>();
        public List<string> centerAverage = new List<string>();
        public List<string> Training = new List<string>();
        public List<string> Award = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            string agentID = Request.QueryString["aid"];
            agentViewInfo = EvaluationEngine.GetAgentView(agentID);
            List<string> agentKPIs = new List<string>();
            foreach (AgentKPIScore kpiScore in agentViewInfo.AgentView)
            {
                agentKPIs.Add(kpiScore.KPIName);
            }
            foreach (string KPI in agentKPIs)
            {
                foreach (AgentKPIScore agentKPI in agentViewInfo.AgentView.Where(row => row.KPIName == KPI))
                {
                    List<string> tempKPI = new List<string>();
                    List<string> tempCenterAvg = new List<string>();
                    List<string> tempTraining = new List<string>();
                    List<string> tempAward = new List<string>();
                    foreach (KPIScoreInfo kpiScore in agentKPI.ScoreDetails)
                    {
                        string tempkpival = String.Format("{{ x: Date.UTC({0}, {1}, {2}), y: {3}, name: '{3}' }}", kpiScore.Date.Year, kpiScore.Date.Month - 1, kpiScore.Date.Day, kpiScore.Score);
                        string tempAvg = String.Format("{{ x: Date.UTC({0}, {1}, {2}), y: {3}, name: '{3}' }}", kpiScore.Date.Year, kpiScore.Date.Month - 1, kpiScore.Date.Day, kpiScore.CenterAverage);
                        tempKPI.Add(tempkpival);
                        tempCenterAvg.Add(tempAvg);
                    }
                    foreach (KPIScoreInfo kpiScore in agentKPI.ScoreDetails.Where(record => record.HadTraining == true))
                    {
                        string tempTrainingInfo = String.Format("{{ x: Date.UTC({0}, {1}, {2}), text: '{3}', title: 'Training' }}", kpiScore.Date.Year, kpiScore.Date.Month - 1, kpiScore.Date.Day, kpiScore.Description);
                        tempTraining.Add(tempTrainingInfo);
                    }
                    foreach (KPIScoreInfo kpiScore in agentKPI.ScoreDetails.Where(record => record.IsAwarded == true))
                    {
                        string tempAwardInfo = String.Format("{{ x: Date.UTC({0}, {1}, {2}), text: '{3}', title: 'Award' }}", kpiScore.Date.Year, kpiScore.Date.Month - 1, kpiScore.Date.Day, kpiScore.Description);
                        tempAward.Add(tempAwardInfo);
                    }
                    kpiScores.Add(string.Join(",", tempKPI.ToArray()));
                    centerAverage.Add(string.Join(",", tempCenterAvg.ToArray()));
                    Training.Add(string.Join(",", tempTraining.ToArray()));
                    Award.Add(string.Join(",", tempAward.ToArray()));
                }
            }

        }
    }
}
