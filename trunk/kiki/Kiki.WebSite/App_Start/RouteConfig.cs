using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Kiki.WebSite
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
                new[] { "Kiki.WebSite.Controllers" }
            );

            routes.MapRoute(
                "LogOut",
                "logout",
                new { controller = "Auth", action = "Logout" },
                new[] { "Kiki.WebSite.Controllers" }
            );

            routes.MapRoute(
                 "Default",
                 "{controller}/{action}/{id}",
                 new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                 new[] { "Kiki.WebSite.Controllers" }
            );
        }
    }
}