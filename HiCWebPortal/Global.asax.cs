using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using Kitchen;
using System.Configuration;

namespace WebPortal
{
    public class Global : HttpApplication
    {
        private static readonly string envir = (string)ConfigurationManager.AppSettings["Environment"];
        private static readonly bool needSSL = Boolean.Parse((string)ConfigurationManager.AppSettings["SSL_" + envir]);

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            /*
            string requestIp = MainHelper.GetIPAddress();
            if (HttpContext.Current.Request.IsLocal.Equals(false)  && requestIp != "118.140.132.22" && Request.ServerVariables["HTTP_HOST"] != "coke.kitchen-digital.com")
            {
                //not Kitchen request, response end
                Response.Redirect("http://coca-cola.hk");
                Response.End();
            }
            */

            //Because AWS testing server use cheap cheap fake SSL, need to check X-Forwarded-Proto as well.
            var loadbalancerReceivedSSLRequest = string.Equals(Request.Headers["X-Forwarded-Proto"], "https");
            var serverReceivedSSLRequest = Request.IsSecureConnection;

            if (!(loadbalancerReceivedSSLRequest || serverReceivedSSLRequest) &&
                (Request.ServerVariables["HTTP_HOST"] == "www.hi-c.com.hk" || needSSL)
               )
            {
                Response.Redirect("https://" + Request.ServerVariables["HTTP_HOST"] + HttpContext.Current.Request.RawUrl);
                Response.End();
                //Response.Write("https://" + Request.ServerVariables["HTTP_HOST"] + HttpContext.Current.Request.RawUrl);

            }
        }


        protected void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            Exception objErr = Server.GetLastError().GetBaseException();

            if (HttpContext.Current != null && HttpContext.Current.Request.IsLocal.Equals(false))
            {
                AppHelper.ApplicationErrorHandler(sender, e, objErr);
                Response.Redirect("~/error.html");
                Server.ClearError();
            }

            /*
            //cannot write log on AWS server.
            string msg = objErr.Message.ToString() + Environment.NewLine + objErr.StackTrace.ToString() + Environment.NewLine;
            if (objErr.InnerException != null) msg += objErr.InnerException.ToString() + Environment.NewLine;

            msg += objErr.Source.ToString();

            AppHelper.WriteLog(msg);
            */

            
        }

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_PostAuthorizeRequest()
        {
            if (IsWebApiRequest())
            {
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            }
        }

        
        private bool IsWebApiRequest()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith("~/api");
        }
        
    }
}
