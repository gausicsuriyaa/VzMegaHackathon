using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Vz.MegaHack.Core;

namespace Vz.MegaHack.Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            PathManager.Initialize(Server.MapPath("~/Content/Data"));
        }
    }
}