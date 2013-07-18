using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Shop
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
                "LogOn", // Route name
                "logon", // URL with parameters
                new { controller = "Account", action = "LogOn" } // Parameter defaults
            );

            routes.MapRoute(
                "NotFoundPage", // Route name
                "NotFoundPage", // URL with parameters
                new { controller = "Error", action = "NotFoundPage" }, // Parameter defaults
                new[] { "Shop.Controllers" }
            );

            routes.MapRoute(
                "ErrorPage", // Route name
                "Error", // URL with parameters
                new { controller = "Error", action = "Index" }, // Parameter defaults
                new[] { "Shop.Controllers" }
            );
            routes.MapRoute(
               "Content", // Route name
               "{id}", // URL with parameters
               new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               new[] { "Shop.Controllers" }
            );

            routes.MapRoute(
               "Catalogue", // Route name
               "catalogue/{id}", // URL with parameters
               new { controller = "Home", action = "Shop", id = UrlParameter.Optional },
               new[] { "Shop.Controllers" }
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