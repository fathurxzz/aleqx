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
                "Search",
                "search/{q}",
                new { controller = "Service", action = "Search", q = UrlParameter.Optional },
                new[] { "Kiki.WebSite.Controllers" }
            );

            routes.MapRoute(
                "Subscribe",
                "subscribe",
                new { controller = "Service", action = "Subscribe"},
                new[] { "Kiki.WebSite.Controllers" }
            );

            routes.MapRoute(
                "Services",
                "{lang}/services/{id}",
                new { controller = "Home", action = "ServiceDetails", id = UrlParameter.Optional, contentName="services" },
                new { lang = @"ru|en" },
                new[] { "Kiki.WebSite.Controllers" }
            );


            routes.MapRoute(
                 "Default",
                 "{lang}/{controller}/{action}/{id}",
                 new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                 new { lang = @"ru|en" },
                 new[] { "Kiki.WebSite.Controllers" }
            );
        }
    }
}