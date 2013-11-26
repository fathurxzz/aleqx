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
                "ChangePassword", // Route name
                "changepassword", // URL with parameters
                new { controller = "Auth", action = "ChangePassword" } // Parameter defaults
            );

            routes.MapRoute(
                "Search", // Route name
                "search", // URL with parameters
                new { controller = "Home", action = "Search" } // Parameter defaults
            );

            routes.MapRoute(
                "UserArticle", // Route name
                "userarticle/{id}", // URL with parameters
                new { controller = "Home", action = "UserArticle",id =UrlParameter.Optional} // Parameter defaults
            );

            routes.MapRoute(
                name: "Technologies",
                url: "technologies/{categoryId}/{subCategoryId}",
                defaults: new { controller = "Home", action = "Technologies", categoryId = UrlParameter.Optional, subCategoryId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Buildings",
                url: "buildings/{categoryId}/{subCategoryId}",
                defaults: new { controller = "Home", action = "Buildings", categoryId = UrlParameter.Optional, subCategoryId = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "Products",
              url: "products/{categoryId}/{subCategoryId}",
              defaults: new { controller = "Home", action = "Products", categoryId = UrlParameter.Optional, subCategoryId = UrlParameter.Optional }
          );


            routes.MapRoute(
                name: "Documents",
                url: "documents/{categoryId}/{subCategoryId}",
                defaults: new { controller = "Home", action = "Documents", categoryId = UrlParameter.Optional, subCategoryId = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "wheretobuy",
               url: "wheretobuy/{categoryId}/{subCategoryId}",
               defaults: new { controller = "Home", action = "WhereToBuy", categoryId = UrlParameter.Optional, subCategoryId = UrlParameter.Optional }
           );

            routes.MapRoute(
              name: "about",
              url: "about/{categoryId}/{subCategoryId}",
              defaults: new { controller = "Home", action = "About", categoryId = UrlParameter.Optional, subCategoryId = UrlParameter.Optional }
          );

            routes.MapRoute(
             name: "articles",
             url: "articles/{id}",
             defaults: new { controller = "Home", action = "Articles", id = UrlParameter.Optional }
         );


           // routes.MapRoute(
           //"SiteContent", // Route name
           //"{id}", // URL with parameters
           //new { controller = "Home", action = "SiteContent", id = UrlParameter.Optional } // Parameter defaults
           // );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute("NotFound", "{*url}", new { controller = "Error", action = "NotFound" });
        }
    }
}