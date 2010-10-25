using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Excursions.Helpers.Validation;

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
               "Search",                                              // Route name
               "Search/",                           // URL with parameters
               new { controller = "Search", action = "Index", id = "" },
               new string[1] { "Excursions.Controllers" }// Parameter defaults
            );

            routes.MapRoute(
               "Excursions",                                              // Route name
               "Excursions/",                           // URL with parameters
               new { controller = "Excursions", action = "Index", type = "1" },
               new string[1] { "Excursions.Controllers" }// Parameter defaults
            );
            
            routes.MapRoute(
               "Sights",                                              // Route name
               "Sights/",                           // URL with parameters
               new { controller = "Excursions", action = "Index", type = "2" },
               new string[1] { "Excursions.Controllers" }// Parameter defaults
            );
            
            routes.MapRoute(
               "ExcursionDetails",                                              // Route name
               "Excursions/{id}",                           // URL with parameters
               new { controller = "Excursions", action = "Details", id = "" },
               new string[1] { "Excursions.Controllers" }// Parameter defaults
            );

            routes.MapRoute(
            "Contacts",                                              // Route name
            "Contacts",                           // URL with parameters
            new { controller = "Contacts", action = "Index" },
             new string[1] { "Excursions.Controllers" }// Parameter defaults
            );

            
            routes.MapRoute(
            "Content",                                              // Route name
            "{id}",                           // URL with parameters
            new { controller = "Content", action = "Index" },
             new string[1] { "Excursions.Controllers" }// Parameter defaults
            );
            
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Excursions", action = "Index", type = "0",id="" } // Parameter defaults
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

            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RemoteAttribute), typeof(RemoteAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(CaptchaAttribute), typeof(CaptchaAttributeAdapter));
        }
    }
}