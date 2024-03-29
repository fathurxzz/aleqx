﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EM2014
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("robots.txt");
            
            routes.MapRoute(
              name: "NotFound",
              url: "notfound",
              defaults: new { controller = "Home", action = "NotFound" }
            );

            routes.MapRoute(
              name: "LogOn",
              url: "logon",
              defaults: new { controller = "Auth", action = "LogIn"}
            );

            routes.MapRoute(
              name: "LogIn",
              url: "login",
              defaults: new { controller = "Auth", action = "LogIn" }
            );
            routes.MapRoute(
              name: "admin",
              url: "admin",
              defaults: new { controller = "Auth", action = "LogIn" }
            );

            routes.MapRoute(
                name: "LogOff",
                url: "logoff",
                defaults: new { controller = "Auth", action = "Logout"}
            );

            routes.MapRoute(
                name: "CategoryProduct",
                url: "{category}/{product}",
                defaults: new { controller = "Home", action = "Index", category = UrlParameter.Optional, product = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}