using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NewVision.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");

            routes.MapRoute(
               "Login",
               "login",
               new { controller = "Auth", action = "Login" },
               new[] { "NewVision.UI.Controllers" }
           );


            routes.MapRoute(
               "contacts",
               "contacts",
               new { controller = "Home", action = "Contacts" },
               new[] { "NewVision.UI.Controllers" }
           );

            routes.MapRoute(
               "feedback",
               "feedback",
               new { controller = "Home", action = "Feedback" },
               new[] { "NewVision.UI.Controllers" }
           );

            routes.MapRoute(
               "events",
               "events",
               new { controller = "Home", action = "Events" },
               new[] { "NewVision.UI.Controllers" }
           );

            routes.MapRoute(
               "partnership",
               "partnership",
               new { controller = "Home", action = "Partnership" },
               new[] { "NewVision.UI.Controllers" }
           );
            routes.MapRoute(
              "news",
              "news",
              new { controller = "Home", action = "News" },
              new[] { "NewVision.UI.Controllers" }
          );
            routes.MapRoute(
              "media",
              "media",
              new { controller = "Home", action = "Media" },
              new[] { "NewVision.UI.Controllers" }
          );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}