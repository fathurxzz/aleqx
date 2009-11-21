using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace bigs
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                "Languages",                                              // Route name
                "Languages/{action}/{returnUrl}",                           // URL with parameters
                new { controller = "Languages", action = "Index", returnUrl = "" }  // Parameter defaults
            );


           
            routes.MapRoute(
                "Content",                                              // Route name
                "{controller}/{contentUrl}",                           // URL with parameters
                new { controller = "Home", action = "Index", contentUrl = "О компании" }  // Parameter defaults
            );
           /* 
            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{contentUrl}",                           // URL with parameters
                new { controller = "Home", action = "Index", contentUrl = "О компании" }  // Parameter defaults
            );*/

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{contentUrl}",                           // URL with parameters
                new { controller = "Home", action = "Index", contentUrl = "О компании" }  // Parameter defaults
            );
            
        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
        }
    }
}