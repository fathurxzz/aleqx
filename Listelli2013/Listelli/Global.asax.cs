using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Listelli
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
               "Catalogue", // Route name
               "{lang}/catalogue", // URL with parameters
               new { controller = "Category", action = "Index", id = UrlParameter.Optional },
               new { lang = @"ru|en" },
               new[] { "Listelli.Controllers" }
            );

            routes.MapRoute(
               "CategoryDetails", // Route name
               "{lang}/category/{id}", // URL with parameters
               new { controller = "Category", action = "Details", id = UrlParameter.Optional },
               new { lang = @"ru|en" },
               new[] { "Listelli.Controllers" }
            );


            routes.MapRoute(
               "Brands", // Route name
               "{lang}/brands", // URL with parameters
               new { controller = "Home", action = "Gallery", id = UrlParameter.Optional },
               new { lang = @"ru|en" },
               new[] { "Listelli.Controllers" }
            );

            routes.MapRoute(
               "BrandDetails", // Route name
               "{lang}/brand/{id}", // URL with parameters
               new { controller = "Home", action = "BrandDetails", id = UrlParameter.Optional },
               new { lang = @"ru|en" },
               new[] { "Listelli.Controllers" }
            );

            routes.MapRoute(
               "Content", // Route name
               "{lang}/{id}", // URL with parameters
               new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               new { lang = @"ru|en" },
               new[] { "Listelli.Controllers" }
            );



            routes.MapRoute(
                name: "lang",
                url: "{lang}/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                constraints: new { lang = @"ru|en" },
                namespaces: new[] { "Listelli.Controllers" }
            );


            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional, lang = "ru" } // Parameter defaults
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