using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SpaceGame.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");

            routes.MapRoute(
                name: "LogIn",
                url: "login",
                defaults: new { controller = "Auth", action = "LogIn" }
            );

            routes.MapRoute(
                name: "LogOut",
                url: "logout",
                defaults: new { controller = "Auth", action = "LogIOut" }
            );

            routes.MapRoute(
                name: "Register",
                url: "register",
                defaults: new { controller = "Auth", action = "Register" }
            );

            routes.MapRoute(
                name: "Overview",
                url: "overview",
                defaults: new { controller = "Home", action = "Overview" }
            );

            routes.MapRoute(
                name: "Resources",
                url: "resources",
                defaults: new { controller = "Home", action = "Resources"}
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}