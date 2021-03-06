﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using WURFL;
using WURFL.Aspnet.Extensions.Config;

namespace RestService
{
    public class Global : System.Web.HttpApplication
    {
        public const String WurflDataFilePath = "~/App_Data/wurfl-latest.zip";
       
        protected void Application_Start(object sender, EventArgs e)
        {

            var wurflDataFile = HttpContext.Current.Server.MapPath(WurflDataFilePath);
            
            
            var configurer = new WURFL.Config.InMemoryConfigurer()
            .MainFile(wurflDataFile);
             AnaysisStart.wurflContainer = WURFLManagerBuilder.Build(configurer);

        }

        private void registerWurfl()
        {
            
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}