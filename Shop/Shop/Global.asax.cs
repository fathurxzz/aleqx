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


            routes.MapRoute(
                "Default1", // Route name
                "", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[1] { "Shop.Controllers" }
            );

            routes.MapRoute(
                "Brands", // Route name
                "Brands/{id}", // URL with parameters
                new { controller = "Shop", action = "Brands", id = "" },
                new string[1] { "Shop.Controllers" }
            );

            routes.MapRoute(
                "Categories", // Route name
                "{id}", // URL with parameters
                new { controller = "Shop", action = "Categories", id = "" },
                new string[1] { "Shop.Controllers" }
            );

            routes.MapRoute(
                "Products", // Route name
                "{category}/{id}", // URL with parameters
                new { controller = "Shop", action = "ProductDetails", id = "" },
                new string[1] { "Shop.Controllers" }
            );


            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[1] { "Shop.Controllers" }
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