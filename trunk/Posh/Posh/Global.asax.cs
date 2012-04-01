﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Posh
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
            /*
            routes.MapRoute(
               "Default1", // Route name
               "", // URL with parameters
               new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               new string[1] { "Shop.Controllers" }
           );
            */

            routes.MapRoute(
                "Catalogue", // Route name
                "catalogue/{id}", // URL with parameters
                new { controller = "Albums", action = "Index", id=""},
                new string[1] { "Shop.Controllers" }
            );


            routes.MapRoute(
                "News", // Route name
                "news", // URL with parameters
                new { controller = "News", action = "Index"},
                new string[1] { "Shop.Controllers" }
            );

            //routes.MapRoute(
            //    "Products", // Route name
            //    "catalogue/{id}", // URL with parameters
            //    new { controller = "Albums", action = "Details" },
            //    new string[1] { "Shop.Controllers" }
            //);


            routes.MapRoute(
              "Content", // Route name
              "{id}", // URL with parameters
              new { controller = "Home", action = "Index", id = "" },
              new string[1] { "Shop.Controllers" }
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