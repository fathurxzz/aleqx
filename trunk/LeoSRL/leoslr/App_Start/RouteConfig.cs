using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Leo
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
                new { controller = "Auth", action = "Login"},
                new[] { "Leo.Controllers" }
            );

            routes.MapRoute(
               "Product",
               "{lang}/{category}/{subcategory}/{product}",
               new { controller = "Home", action = "Index", category = UrlParameter.Optional, subcategory = UrlParameter.Optional, product = UrlParameter.Optional },
               new { lang = @"ru|en" },
               new[] { "Leo.Controllers" }
           );

            routes.MapRoute(
                "Category",
                "{lang}/{category}/{subcategory}",
                new { controller = "Home", action = "Index", category = UrlParameter.Optional, subcategory = UrlParameter.Optional },
                new { lang = @"ru|en" },
                new[] { "Leo.Controllers" }
            );

            routes.MapRoute(
                "CategoryParent",
                "{lang}/{category}",
                new { controller = "Home", action = "Index", category = UrlParameter.Optional },
                new { lang = @"ru|en" },
                new[] { "Leo.Controllers" }
            );

            routes.MapRoute(
                "Home",
                "{lang}/{controller}/{action}/{id}",
                new { controller = "Home", action = "Intro", id = UrlParameter.Optional, lang = "ru" },
                new { lang = @"ru|en" },
                new[] { "Leo.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Intro", id = UrlParameter.Optional }
            );
        }
    }
}