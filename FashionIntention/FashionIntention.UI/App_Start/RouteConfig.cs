using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FashionIntention.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               "Login",
               "login",
               new { controller = "Auth", action = "Login" },
               new[] { "NewVision.UI.Controllers" }
            );

            routes.MapRoute(
               "Logout",
               "logout",
               new { controller = "Auth", action = "Logout" },
               new[] { "NewVision.UI.Controllers" }
            );


            routes.MapRoute(
               "post",
               "post/{id}",
               new { controller = "Home", action = "PostDetails" },
               new[] { "NewVision.UI.Controllers" }
           );
            
            routes.MapRoute(
               "posts",
               "posts/{year}/{month}",
               new { controller = "Home", action = "Posts" },
               new[] { "NewVision.UI.Controllers" }
           );

            routes.MapRoute(
              "about",
              "about",
              new { controller = "Home", action = "About" },
              new[] { "NewVision.UI.Controllers" }
          );

            routes.MapRoute(
              "media",
              "media",
              new { controller = "Home", action = "Media" },
              new[] { "NewVision.UI.Controllers" }
          );

            routes.MapRoute(
              "travel",
              "travel",
              new { controller = "Home", action = "Travel" },
              new[] { "NewVision.UI.Controllers" }
          );
            routes.MapRoute(
            "press",
            "press",
            new { controller = "Home", action = "Articles" },
            new[] { "NewVision.UI.Controllers" }
        );

            routes.MapRoute(
          "pressDetails",
          "press/{id}",
          new { controller = "Home", action = "ArticleDetails" },
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