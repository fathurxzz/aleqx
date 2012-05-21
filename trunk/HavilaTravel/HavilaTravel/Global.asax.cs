using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HavilaTravel
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");

            routes.MapRoute(
                "Subscribe", // Route name
                "Subscribe", // URL with parameters
                new { controller = "Subscribe", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                new string[1] { "HavilaTravel.Controllers" }
            );

            routes.MapRoute(
                "UnSubscribe", // Route name
                "unsubscribe/{id}", // URL with parameters
                new { controller = "Subscribe", action = "Unsubscribe", id = UrlParameter.Optional }, // Parameter defaults
                new string[1] { "HavilaTravel.Controllers" }
            );

            routes.MapRoute(
                "Articles", // Route name
                "Articles", // URL with parameters
                new { controller = "Articles", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                new string[1]{"HavilaTravel.Controllers"}
            );

            routes.MapRoute(
                "ArticleDetails", // Route name
                "Articles/{id}", // URL with parameters
                new { controller = "Articles", action = "Details", id = UrlParameter.Optional }, // Parameter defaults
                new string[1] { "HavilaTravel.Controllers" }
            );

            routes.MapRoute(
                "Countries", // Route name
                "Countries", // URL with parameters
                new { controller = "Place", action = "Index", id = UrlParameter.Optional, placeKind=1 }, // Parameter defaults
                new string[1] { "HavilaTravel.Controllers" }
            );

            routes.MapRoute(
                "Spa", // Route name
                "Spa", // URL with parameters
                new { controller = "Place", action = "Index", id = UrlParameter.Optional, placeKind = 11 }, // Parameter defaults
                new string[1] { "HavilaTravel.Controllers" }
            );

            routes.MapRoute(
                "Places", // Route name
                "Places/{id}", // URL with parameters
                new { controller = "Place", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                new string[1] { "HavilaTravel.Controllers" }
            );

            routes.MapRoute(
                "Default1", // Route name
                "{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}