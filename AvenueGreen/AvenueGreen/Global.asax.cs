using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AvenueGreen
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Admin",                                              // Route name
                "Admin/{action}",                           // URL with parameters
                new { controller = "Admin", action = "Index", id = "" }  // Parameter defaults
            );


            /*
            routes.MapRoute(
                "DefaultContent",                                              // Route name
                "Content/{id}",                           // URL with parameters
                new { controller = "Content", action = "Index", id = "About" }  // Parameter defaults
            );
            */


            routes.MapRoute(
            "Content",                                              // Route name
            "{id}",                           // URL with parameters
            new { controller = "Content", action = "Index", id = "About" }  // Parameter defaults
            );

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Content", action = "Index", id = "About" }  // Parameter defaults
            );

            routes.MapRoute(
                "Default1",                                              // Route name
                "{controller}/{action}",                           // URL with parameters
                new { controller = "Content", action = "Index", id = "About" }  // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
        }
    }
}