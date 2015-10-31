using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vz.MegaHack.Entities
{
    public class UserDetails
    {
        public string[] UserName { get; set; }
        public int[] UserScore { get; set; }
        public string[] TopKPIs { get; set; }
        public string[] BottomKPIs { get; set; }

        public string[] AgentID { get; set; }

        public string HeaderName { get; set; }
    }
}
