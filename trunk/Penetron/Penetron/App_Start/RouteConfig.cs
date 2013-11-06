using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Penetron
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");

            routes.MapRoute(
           "MainPage", // Route name
           "", // URL with parameters
           new { controller = "Home", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "LogIn", // Route name
                "login", // URL with parameters
                new { controller = "Auth", action = "Login" } // Parameter defaults
            );

            routes.MapRoute(
                "LogOut", // Route name
                "logout", // URL with parameters
                new { controller = "Auth", action = "Logout" } // Parameter defaults
            );

            routes.MapRoute(
                name: "Technology",
                url: "technologies/{categoryId}/{subCategoryId}",
                defaults: new { controller = "Home", action = "Technologies", categoryId = UrlParameter.Optional, subCategoryId = UrlParameter.Optional }
            );


            routes.MapRoute(
           "SiteContent", // Route name
           "{id}", // URL with parameters
           new { controller = "Home", action = "SiteContent", id = UrlParameter.Optional } // Parameter defaults
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute("NotFound", "{*url}", new { controller = "Error", action = "NotFound" });
        }
    }
}