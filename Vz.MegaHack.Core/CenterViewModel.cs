using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vz.MegaHack.Entities;

namespace Vz.MegaHack.Core
{
    public class CenterViewModel
    {
       public UserDetails getSupervisors(int intCenterID)
       {
           DataSet ds = new DataSet();

           DataTable dt = new DataTable("SupervisorInfo");
           DataColumn dColName = dt.Columns.Add("uname");
           DataColumn dColScore = dt.Columns.Add("uscore");
           DataColumn dColTopKPIs = dt.Columns.Add("topkpi");
           DataColumn dColBottomKPIs = dt.Columns.Add("bottomkpi");

           DataRow dr1 = dt.NewRow();
           dr1[dColName] = "Tucker Otis";
           dr1[dColScore] = "70";
           dr1[dColTopKPIs] = "FCR: 20 / ABC: 10";
           dr1[dColBottomKPIs] = "ACW: 40 / XYZ: 55";
           dt.Rows.Add(dr1);

           DataRow dr2 = dt.NewRow();
           dr2[dColName] = "Tucker Otis2";
           dr2[dColScore] = "30";
           dr2[dColTopKPIs] = "FCR: 20 / ABC: 10";
           dr2[dColBottomKPIs] = "ACW: 40 / XYZ: 55";
           dt.Rows.Add(dr2);

           DataRow dr3 = dt.NewRow();
           dr3[dColName] = "Tucker Otis3";
           dr3[dColScore] = "50";
           dr3[dColTopKPIs] = "FCR: 20 / ABC: 10";
           dr3[dColBottomKPIs] = "ACW: 40 / XYZ: 55";
           dt.Rows.Add(dr3);

           DataRow dr4 = dt.NewRow();
           dr4[dColName] = "Tucker Otis4";
           dr4[dColScore] = "20";
           dr4[dColTopKPIs] = "FCR: 20 / ABC: 10";
           dr4[dColBottomKPIs] = "ACW: 40 / XYZ: 55";
           dt.Rows.Add(dr4);
           ds.Tables.Add(dt);


           UserDetails objUserDetails = new UserDetails();
           string[] strSplitName = new string[] { };
           string[] strSplitScore = new string[] { };
           string[] strSplitTopKPI = new string[] { };
           string[] strSplitBottomKPI = new string[] { };

           string strName = string.Empty;
           string strScore = string.Empty;
           string strTopKPI = string.Empty;
           string strBottomKPI = string.Empty;

           if (ds != null)
           {
               if (ds.Tables.Count > 0)
               {
                   if (ds.Tables["SupervisorInfo"].Rows.Count > 0)
                   {
                       foreach (DataRow dr in ds.Tables["SupervisorInfo"].Rows)
                       {
                           strName += dr["uname"].ToString() + ",";
                           strScore += dr["uscore"].ToString() + ",";
                           strTopKPI += dr["topkpi"].ToString() + ",";
                           strBottomKPI += dr["bottomkpi"].ToString() + ",";
                       }

                       strName = strName.Remove(strName.Length - 1);
                       strScore = strScore.Remove(strScore.Length - 1);
                       strTopKPI = strTopKPI.Remove(strTopKPI.Length - 1);
                       strBottomKPI = strBottomKPI.Remove(strBottomKPI.Length - 1);
                       strSplitName = strName.Split(',');
                       strSplitScore = strScore.Split(',');
                       strSplitTopKPI = strTopKPI.Split(',');
                       strSplitBottomKPI = strBottomKPI.Split(',');
                   }
               }

               objUserDetails.UserName = strSplitName;
               objUserDetails.UserScore = strSplitScore;
               objUserDetails.TopKPIs = strSplitTopKPI;
               objUserDetails.BottomKPIs = strSplitBottomKPI;
           }

           return objUserDetails;
       }
    }
}
