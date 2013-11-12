using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CashMachine
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");

            routes.MapRoute(
                name: "LogOut",
                url: "logout",
                defaults: new { controller = "Auth", action = "LogOut", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "EnterCardNumber",
                url: "cardnumber",
                defaults: new { controller = "Auth", action = "CardNumber", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "EnterCardPin",
                url: "pin",
                defaults: new { controller = "Auth", action = "CardPin", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}