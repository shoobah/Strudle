using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Strudle
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (Context.Request.Path.Contains("/signalr/negotiate"))
            {
                Context.Response.AddHeader("Access-Control-Allow-Origin", "*");
                Context.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
                Context.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
                Context.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            }
        }
    }
}