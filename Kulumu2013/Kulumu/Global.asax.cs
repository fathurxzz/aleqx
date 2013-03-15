using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Kulumu
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

            routes.MapRoute(
                "Tour", // Route name
                "tour/{id}", // URL with parameters
                new { controller = "Home", action = "Tour", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "Gallery", // Route name
                "gallery", // URL with parameters
                new { controller = "Home", action = "Gallery", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "GalleryDetails", // Route name
                "gallery/{id}", // URL with parameters
                new { controller = "Home", action = "Gallery", id = UrlParameter.Optional } // Parameter defaults
            );


            routes.MapRoute(
                "Articles", // Route name
                "articles", // URL with parameters
                new { controller = "Home", action = "Articles", id = UrlParameter.Optional } // Parameter defaults
            );


            routes.MapRoute(
                "Content", // Route name
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