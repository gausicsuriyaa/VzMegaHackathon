using Vz.MegaHack.Entities;
using Vz.MegaHack.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Vz.MegaHack.Engines
{
    public static class EvaluationEngine {

        public static LeaderBoardResponse GetLeaderBoard(string centerId) {
            List<LeaderBoardItem> leaderBoard = new List<LeaderBoardItem>();
            List<AgentInfo> agents = AgentReader.GetAgentsForCenter(centerId);

            int rank = 0;
            foreach (AgentInfo agent in agents.OrderByDescending(a => a.Points)) {
                leaderBoard.Add(new LeaderBoardItem() {
                    Name = agent.AgentName,
                    TotalPoints = agent.Points,
                    PhotoFileName = agent.PhotoFileName,
                    Rank = ++rank
                });

                if (rank >= 10) {
                    break;
                }
            }

            return new LeaderBoardResponse() { 
                CenterName = AgentReader.GetCenterName(centerId), 
                LeaderBoard = leaderBoard 
            };
        }

        public static CenterViewResponse GetCenterView(string centerId) {
            List<KPIItem> centerView = new List<KPIItem>();
            List<AgentKPIInfo> allKPIs = KPIReader.GetAgentKPI();
            List<KPIInfo> kpiNames = KPIReader.GetAllKPIInfo();

            List<SupervisorInfo> supervisors = AgentReader.GetSupervisorsForCenter(centerId);
            foreach (var supervisor in supervisors) {
                List<KPIItem> supervisorKPI = GetSupervisorView(supervisor.SupervisorId).SupervisorView;

                if (supervisorKPI.Count > 0) {
                    List<AgentInfo> agents = AgentReader.GetAgentsForSupervisor(supervisor.SupervisorId);
                    var agentKPIs = from allK in allKPIs
                                    join a in agents on allK.AgentId equals a.AgentId
                                    group allK by allK.KpiId into g
                                    select new { KpiId = g.First().KpiId, KpiTotal = g.Average(a => a.KpiValue) };

                    string topKpiString = string.Empty;
                    string bottomKpiString = string.Empty;

                    var topKPI = agentKPIs.OrderByDescending(a => a.KpiTotal).Take(2);
                    foreach (var row in topKPI) {
                        string kpiName = kpiNames.Where(k => k.KpiId.Equals(row.KpiId)).FirstOrDefault().KpiName;
                        topKpiString += string.Format("{0}: {1};", kpiName, row.KpiTotal);
                    }

                    var bottomKPI = agentKPIs.OrderBy(a => a.KpiTotal).Take(2);
                    foreach (var row in bottomKPI) {
                        string kpiName = kpiNames.Where(k => k.KpiId.Equals(row.KpiId)).FirstOrDefault().KpiName;
                        bottomKpiString += string.Format("{0}: {1};", kpiName, row.KpiTotal);
                    }

                    double avgScore = supervisorKPI.Average(a => a.Score);

                    centerView.Add(new KPIItem() {
                        AgentId = supervisor.SupervisorId,
                        AgentName = supervisor.SupervisorName,
                        Score = Convert.ToInt32(avgScore),
                        TopKPI = topKpiString,
                        BottomKPI = bottomKpiString
                    });
                }
            }

            return new CenterViewResponse() {
                CenterName = AgentReader.GetCenterName(centerId),
                CenterView = centerView
            };
        }

        public static SupervisorViewResponse GetSupervisorView(string supervisorId) {
            List<KPIItem> supervisorView = new List<KPIItem>();

            List<KPIInfo> kpis = KPIReader.GetKPIInfo();
            List<AgentKPIInfo> agentKpis = KPIReader.GetAgentKPI();
            List<AgentInfo> agents = AgentReader.GetAgentsForSupervisor(supervisorId);

            var result = from k in kpis
                         join ak in agentKpis on k.KpiId equals ak.KpiId
                         join a in agents on ak.AgentId equals a.AgentId
                         orderby a.AgentName, ak.KpiValue ascending
                         select new { a.AgentName, k.KpiName, ak.KpiValue };

            var agentGroups = result.GroupBy(a => a.AgentName).Select(g => g.FirstOrDefault());

            foreach (var agentGroup in agentGroups) {

                var agentRows = result.Where(a => a.AgentName.Equals(agentGroup.AgentName));
                double avgKpi = agentRows.Average(a => a.KpiValue);
                string topKpiString = string.Empty;
                string bottomKpiString = string.Empty;

                var topKPI = agentRows.OrderByDescending(a => a.KpiValue).Take(2);
                foreach (var row in topKPI) {
                    topKpiString += string.Format("{0}: {1};", row.KpiName, row.KpiValue);
                }

                var bottomKPI = agentRows.OrderBy(a => a.KpiValue).Take(2);
                foreach (var row in bottomKPI) {
                    bottomKpiString += string.Format("{0}: {1};", row.KpiName, row.KpiValue);
                }

                supervisorView.Add(new KPIItem() {
                    AgentName = agentGroup.AgentName,
                    Score = Convert.ToInt32(avgKpi),
                    TopKPI = topKpiString,
                    BottomKPI = bottomKpiString
                });
            }

            return new SupervisorViewResponse() {
                SupervisorName = AgentReader.GetSupervisorName(supervisorId),
                SupervisorView = supervisorView.OrderByDescending(i => i.Score).ToList()
            };
        }

        public static HeatMapViewResponse GetHeatMapView(string supervisorId) {
            var heatMapItems = new List<Dictionary<string, string>>();

            List<KPIInfo> kpis = KPIReader.GetAllKPIInfo();
            List<AgentKPIInfo> agentKpis = KPIReader.GetAgentKPI();
            List<AgentInfo> agents = AgentReader.GetAgentsForSupervisor(supervisorId);

            string centerId = AgentReader.GetCenterIdForSupervisor(supervisorId);
            List<AgentInfo> centerAgents = AgentReader.GetAgentsForCenter(centerId);

            var result = from k in kpis
                         join ak in agentKpis on k.KpiId equals ak.KpiId
                         join a in agents on ak.AgentId equals a.AgentId
                         orderby k.Category, k.KpiId, a.AgentName
                         select new { a.AgentId, a.AgentName, a.Date, k.KpiId, k.KpiName, ak.KpiValue, k.Category };

            var kpiGroups = result.GroupBy(a => a.KpiName).Select(g => g.FirstOrDefault());

            foreach (var kpiGroup in kpiGroups) {

                var kpiRows = from a in result
                              where a.KpiName.Equals(kpiGroup.KpiName)
                              group a by a.AgentName into g
                              select g.OrderByDescending(i => i.Date).FirstOrDefault();

                var item = new Dictionary<string, string>();
                item.Add("..Category", kpiGroup.Category);
                item.Add(".Behavior Attribute", kpiGroup.KpiName);

                var centerResult = from ak in agentKpis
                              join ca in centerAgents on ak.AgentId equals ca.AgentId
                              where ak.KpiId == kpiGroup.KpiId
                              group ak by ak.KpiId into g
                              select new { CenterAverage = g.Average(a => a.KpiValue) };

                var sdResult = from ak in agentKpis
                                   join ca in centerAgents on ak.AgentId equals ca.AgentId
                                   where ak.KpiId == kpiGroup.KpiId
                                   select new { ak.KpiValue };
                List<double> sdList = new List<double>();
                foreach (var row in sdResult) {
                    sdList.Add(row.KpiValue);
                }

                double centerAverage = Convert.ToInt32(centerResult.First().CenterAverage);
                item.Add(".Center Average", centerAverage.ToString("F"));

                double standardDeviation = StatisticsHelper.GetStandardDeviation(sdList);
                item.Add(".Standard Deviation", standardDeviation.ToString("F"));

                foreach (var row in kpiRows) {
                    item.Add(row.AgentName, row.KpiValue.ToString("F"));
                }

                heatMapItems.Add(item);
            }

            return new HeatMapViewResponse() {
                SupervisorName = AgentReader.GetSupervisorName(supervisorId),
                HeatMapView = heatMapItems
            }; 
        }

        public static AgentViewResponse GetAgentView(string agentId) {
            List<AgentKPIScore> agentView = new List<AgentKPIScore>();

            List<KPIInfo> kpis = KPIReader.GetKPIInfo();
            List<AgentKPIInfo> agentKpis = KPIReader.GetAgentKPI();
            AgentInfo agentInfo = AgentReader.GetAgentInfo(agentId);

            var result = from k in kpis
                         join ak in agentKpis on k.KpiId equals ak.KpiId
                         where ak.AgentId == agentId
                         orderby k.KpiName
                         select new { k.KpiId, k.KpiName, ak.Date, ak.KpiValue, ak.HadTraining, ak.IsAwarded, ak.Description };

            var kpiGroups = result.GroupBy(k => k.KpiName).Select(g => g.FirstOrDefault());

            foreach (var kpiGroup in kpiGroups) {
                var kpiRows = result.Where(k => k.KpiName.Equals(kpiGroup.KpiName));

                List<KPIScoreInfo> scoreList = new List<KPIScoreInfo>();
                foreach(var row in kpiRows) {
                    var dateRows = kpiRows.Where(k => Convert.ToDateTime(k.Date) <= row.Date);
                    double centerAverage = dateRows.Average(k => k.KpiValue);

                    scoreList.Add(new KPIScoreInfo() {
                                    Date = row.Date,
                                    Score = row.KpiValue.ToString(),
                                    CenterAverage = centerAverage.ToString("F"),
                                    HadTraining = row.HadTraining,
                                    IsAwarded = row.IsAwarded,
                                    Description = row.Description
                    });
                }

                agentView.Add(new AgentKPIScore() {
                    KPIName = kpiGroup.KpiName,
                    ScoreDetails = scoreList
                });
            }

            return new AgentViewResponse() {
                AgentName = AgentReader.GetAgentInfo(agentId).AgentName,
                AgentView = agentView
            };
        }

    }

}
