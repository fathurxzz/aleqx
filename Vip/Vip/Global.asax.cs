﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Vip
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


            //routes.MapRoute(
            //    "Catalogue", // Route name
            //    "catalogue/{category}/{filter}/{brand}", // URL with parameters
            //    new { controller = "Catalogue", action = "Index", category = UrlParameter.Optional, filter = UrlParameter.Optional, brand = UrlParameter.Optional } // Parameter defaults
            //);

            //routes.MapRoute(
            //    "Catalogue1", // Route name
            //    "catalogue/{category}/{filter}", // URL with parameters
            //    new { controller = "Catalogue", action = "Index", category = UrlParameter.Optional, filter = UrlParameter.Optional } // Parameter defaults
            //);

            //routes.MapRoute(
            //    "Catalogue2", // Route name
            //    "catalogue/{category}", // URL with parameters
            //    new { controller = "Catalogue", action = "Index", category = UrlParameter.Optional } // Parameter defaults
            //);

          
            //routes.MapRoute(
            //    "Projects", // Route name
            //    "portfolio/{project}", // URL with parameters
            //    new { controller = "Projects", action = "Index", project = UrlParameter.Optional } // Parameter defaults
            //);

            //routes.MapRoute(
            //    "Articles", // Route name
            //    "articles", // URL with parameters
            //    new { controller = "Articles", action = "Index" } // Parameter defaults
            //);

            //routes.MapRoute(
            //    "Content", // Route name
            //    "{id}", // URL with parameters
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            //);

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