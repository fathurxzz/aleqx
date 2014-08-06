using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Shop.WebSite
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
                new[] { "Shop.WebSite.Controllers" }
            );

            routes.MapRoute(
                "LogOut",
                "logout",
                new { controller = "Auth", action = "Logout" },
                new[] { "Shop.WebSite.Controllers" }
            );

            routes.MapRoute(
              "Main",
              "{lang}",
              new { controller = "Home", action = "Index"},
              new { lang = @"ru|en" },
              new[] { "Shop.WebSite.Controllers" }
          );

            routes.MapRoute(
               "Catalogue",
               "{lang}/catalogue/{category}/{subcategory}",
               new { controller = "Home", action = "Catalogue", category = UrlParameter.Optional, subcategory = UrlParameter.Optional },
               new { lang = @"ru|en" },
               new[] { "Shop.WebSite.Controllers" }
           );

            routes.MapRoute(
               "Product",
               "{lang}/product/{product}",
               new { controller = "Home", action = "ProductDetails", product = UrlParameter.Optional },
               new { lang = @"ru|en" },
               new[] { "Shop.WebSite.Controllers" }
           );
            

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional, lang = "ru" },
                new { lang = @"ru|en" },
                new[] { "Shop.WebSite.Controllers" }
            );
        }
    }
}