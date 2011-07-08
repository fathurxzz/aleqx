using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Che
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              "Sites", // Route name
              "sites/{id}", // URL with parameters
              new { controller = "Home", action = "Content", id = UrlParameter.Optional, contentType=1 } // Parameter defaults
            );

            routes.MapRoute(
              "Styles", // Route name
              "id/{id}", // URL with parameters
              new { controller = "Home", action = "Content", id = UrlParameter.Optional, contentType = 2 } // Parameter defaults
            );
            routes.MapRoute(
              "Adv", // Route name
              "ad/{id}", // URL with parameters
              new { controller = "Home", action = "Content", id = UrlParameter.Optional, contentType = 3 } // Parameter defaults
            );

            routes.MapRoute(
              "Main", // Route name
              "{id}", // URL with parameters
              new { controller = "Home", action = "Index", id = UrlParameter.Optional, contentType = 0 } // Parameter defaults
            );

            

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }
    }
}