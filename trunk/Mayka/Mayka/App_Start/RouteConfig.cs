using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mayka
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("robots.txt");


            routes.MapRoute(
                name: "LogOn",
                url: "logon",
                defaults: new { controller = "Auth", action = "LogIn", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "LogOff",
                url: "logoff",
                defaults: new { controller = "Auth", action = "Logout", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ContentItem",
                url: "content/{id}",
                defaults: new { controller = "Home", action = "Index"}
            );

            routes.MapRoute(
                name: "GalleryItem",
                url: "gallery/{id}",
                defaults: new { controller = "Home", action = "Gallery", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Product",
                url: "products/{id}",
                defaults: new { controller = "Home", action = "Products", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}