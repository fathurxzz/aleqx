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
              "UpdateCart", // Route name
              "updatecart", // URL with parameters
              new { controller = "Cart", action = "Update", id = UrlParameter.Optional, quantity = UrlParameter.Optional },
              new string[1] { "Shop.WebSite.Controllers" }
          );

            routes.MapRoute(
              "AddToCart", // Route name
              "addtocart/{id}", // URL with parameters
              new { controller = "Cart", action = "Add", id = "" },
              new string[1] { "Shop.WebSite.Controllers" }
          );

          //  routes.MapRoute(
          //    "GetProducts", // Route name
          //    "getproducts/{id}", // URL with parameters
          //    new { controller = "Home", action = "GetProducts", id = "" },
          //    new string[1] { "Shop.WebSite.Controllers" }
          //);
            


            routes.MapRoute(
               "Cart", // Route name
               "{lang}/cart/{action}", // URL with parameters
               new { controller = "Cart", action = "Index" },
               new { lang = @"ru|ua" },
               new string[1] { "Shop.WebSite.Controllers" }
           );



            routes.MapRoute(
              "Content",
              "{lang}/content/{id}",
              new { controller = "Home", action = "Index", id = UrlParameter.Optional },
              new { lang = @"ru|ua" },
              new[] { "Shop.WebSite.Controllers" }
          );

            routes.MapRoute(
              "Main",
              "{lang}",
              new { controller = "Home", action = "Index" },
              new { lang = @"ru|ua" },
              new[] { "Shop.WebSite.Controllers" }
            );

            routes.MapRoute(
               "Catalogue",
               "{lang}/catalogue/{category}/{filter}",
               new { controller = "Home", action = "Catalogue", category = UrlParameter.Optional, filter = UrlParameter.Optional },
               new { lang = @"ru|ua" },
               new[] { "Shop.WebSite.Controllers" }
           );

            //routes.MapRoute(
            //    "Catalogue",
            //    "{lang}/catalogue/{category}/{subcategory}",
            //    new { controller = "Home", action = "Catalogue", category = UrlParameter.Optional, subcategory = UrlParameter.Optional },
            //    new { lang = @"ru|ua" },
            //    new[] { "Shop.WebSite.Controllers" }
            //);

            routes.MapRoute(
               "Product",
               "{lang}/product/{product}",
               new { controller = "Home", action = "ProductDetails", product = UrlParameter.Optional },
               new { lang = @"ru|ua" },
               new[] { "Shop.WebSite.Controllers" }
           );

            routes.MapRoute(
               "ArticleDetails",
               "{lang}/articles/{article}",
               new { controller = "Home", action = "ArticleDetails", article = UrlParameter.Optional },
               new { lang = @"ru|ua" },
               new[] { "Shop.WebSite.Controllers" }
           );



            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional, lang = "ru" },
                new { lang = @"ru|ua" },
                new[] { "Shop.WebSite.Controllers" }
            );
        }
    }
}