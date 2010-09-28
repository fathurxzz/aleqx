using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Excursions
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("captcha.ashx");

            routes.MapRoute(
                "Admin",                                              // Route name
                "Admin/{action}",                           // URL with parameters
                new { controller = "Admin", action = "Index", id = "" }  // Parameter defaults
            );

            routes.MapRoute(
               "Excursions",                                              // Route name
               "Excursions/{action}",                           // URL with parameters
               new { controller = "Excursions", action = "Index", id = "" },
               new string[1] { "Excursions.Controllers" }// Parameter defaults
            );

            routes.MapRoute(
               "ExcursionDetails",                                              // Route name
               "Excursions/{action}/{id}",                           // URL with parameters
               new { controller = "Excursions", action = "Details", id = "" },
               new string[1] { "Excursions.Controllers" }// Parameter defaults
            );

            routes.MapRoute(
            "Content",                                              // Route name
            "{id}",                           // URL with parameters
            new { controller = "Excursions", action = "Index", id = "About" },
             new string[1] { "Excursions.Controllers" }// Parameter defaults
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
                ,new string[1] { "Excursions.Controllers" }
            );

            routes.MapRoute(
                "Default1",                                              // Route name
                "{controller}/{action}",                           // URL with parameters
                new { controller = "Content", action = "Index", id = "About" }  // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }
    }
}