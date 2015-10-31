using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Vz.MegaHack.Core;
using Vz.MegaHack.Entities;
using Vz.MegaHack.Engines;

namespace Vz.MegaHack.Web.Services
{
    /// <summary>
    /// Summary description for CCTService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CCTService : System.Web.Services.WebService
    {

        [WebMethod]
        public UserDetails GetSupervisorInfo(string strCenterID)
        {
            UserDetails objUserDetails = new UserDetails();
            CenterViewResponse objCenterViewResponse = EvaluationEngine.GetCenterView(strCenterID);
            //List<KPIItem> lstKPIItems = EvaluationEngine.GetCenterView(strCenterID);
            List<KPIItem> lstKPIItems = objCenterViewResponse.CenterView;
            objUserDetails.UserName = lstKPIItems.Select(x => x.AgentName).ToArray();
            objUserDetails.UserScore = lstKPIItems.Select(x => x.Score).ToArray();
            objUserDetails.TopKPIs = lstKPIItems.Select(x => x.TopKPI).ToArray();
            objUserDetails.BottomKPIs = lstKPIItems.Select(x => x.BottomKPI).ToArray();
            objUserDetails.AgentID = lstKPIItems.Select(x => x.AgentId).ToArray();
            objUserDetails.HeaderName = objCenterViewResponse.CenterName;
            return objUserDetails;
            //CenterViewModel objCenterViewModel = new CenterViewModel();
            //return objCenterViewModel.getSupervisors(strCenterID);
        }


        [WebMethod]
        public UserDetails GetAgentInfo(string strSupID)
        {
            UserDetails objUserDetails = new UserDetails();
            SupervisorViewResponse objSupervisorViewResponse = EvaluationEngine.GetSupervisorView(strSupID);
            List<KPIItem> lstKPIItems = objSupervisorViewResponse.SupervisorView;
            objUserDetails.UserName = lstKPIItems.Select(x => x.AgentName).ToArray();
            objUserDetails.UserScore = lstKPIItems.Select(x => x.Score).ToArray();
            objUserDetails.TopKPIs = lstKPIItems.Select(x => x.TopKPI).ToArray();
            objUserDetails.BottomKPIs = lstKPIItems.Select(x => x.BottomKPI).ToArray();
            objUserDetails.HeaderName = objSupervisorViewResponse.SupervisorName;
            objUserDetails.AgentID = lstKPIItems.Select(x => x.AgentId).ToArray();
            return objUserDetails;
            //CenterViewModel objCenterViewModel = new CenterViewModel();
            //return objCenterViewModel.getSupervisors(strCenterID);
        }

        [WebMethod]
        public LeaderBoardItem[] GetLeaderBoardDetails(string strCenterID)
        {
            LeaderBoardResponse objLeaderBoardResponse = EvaluationEngine.GetLeaderBoard(strCenterID);
            return objLeaderBoardResponse.LeaderBoard.ToArray();
            //return EvaluationEngine.GetLeaderBoard(strCenterID).ToArray();
        }

        [WebMethod]
        public HeatMapViewResponse GetHeatMapDetails(string strSupID)
        {
            return EvaluationEngine.GetHeatMapView(strSupID); ;
            //HeatMapViewResponse objHeatMapViewResponse = EvaluationEngine.GetHeatMapView(strSupID);
            //List<Dictionary<string, string>> objlistdict = new List<Dictionary<string, string>>();
            //objlistdict = EvaluationEngine.GetHeatMapView(strSupID);
        }
    
    }
}
