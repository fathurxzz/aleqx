using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EM2013
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
                "SecretLink", // Route name
                "secretlink", // URL with parameters
                new { controller = "Home", action = "SiteContent", id = "secretlink" } // Parameter defaults
            );

            

            routes.MapRoute(
                "Feedback", // Route name
                "feedback", // URL with parameters
                new { controller = "Home", action = "SiteContent", id = "feedback" } // Parameter defaults
            );

            routes.MapRoute(
                "Account", // Route name
                "account/logon", // URL with parameters
                new { controller = "Account", action = "LogOn" } // Parameter defaults
            );

            routes.MapRoute(
                "FeedbackForm", // Route name
                "Home/Feedback", // URL with parameters
                new { controller = "Home", action = "Feedback" } // Parameter defaults
            );

            //routes.MapRoute(
            //    "Category", // Route name
            //    "{category}", // URL with parameters
            //    new { controller = "Home", action = "Index", category = UrlParameter.Optional } // Parameter defaults
            //);

            routes.MapRoute(
                "CategoryProduct", // Route name
                "{category}/{product}", // URL with parameters
                new { controller = "Home", action = "Index", category = UrlParameter.Optional, product = UrlParameter.Optional } // Parameter defaults
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