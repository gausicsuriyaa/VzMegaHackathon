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
        public UserDetails GetSupervisorInfo(int intCenterID)
        {
            CenterViewModel objCenterViewModel = new CenterViewModel();
            return objCenterViewModel.getSupervisors(intCenterID);
        }

        [WebMethod]
        public LeaderBoardItem[] GetLeaderBoardDetails(string strCenterID)
        {
            return EvaluationEngine.GetLeaderBoard(strCenterID).ToArray();
        }
    }
}
