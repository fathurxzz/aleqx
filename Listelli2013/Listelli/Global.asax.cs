using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

            //routes.MapRoute(
            //    "NotFoundPage1", // Route name
            //    "NotFoundPage", // URL with parameters
            //    new { controller = "Error", action = "NotFoundPage" }, // Parameter defaults
            //     //new { lang = @"ru|en" },
            //    new[] { "Listelli.Controllers" }
            //);

            routes.MapRoute(
                "NotFoundPage", // Route name
                "{lang}/NotFoundPage", // URL with parameters
                new { controller = "Error", action = "NotFoundPage" }, // Parameter defaults
                 new { lang = @"ru|en" },
                new[] { "Listelli.Controllers" }
            );

            routes.MapRoute(
                "ErrorPage", // Route name
                "Error", // URL with parameters
                new { controller = "Error", action = "Index" }, // Parameter defaults
                new[] { "Listelli.Controllers" }
            );

            routes.MapRoute(
                "subscribe", // Route name
                "subscribe/{id}", // URL with parameters
                new { controller = "Home", action = "ConfirmSubscribe" }, // Parameter defaults
                new[] { "Listelli.Controllers" }
            );


            //routes.MapRoute(
            //   "Catalogue", // Route name
            //   "{lang}/catalogue", // URL with parameters
            //   new { controller = "Category", action = "Index", id = UrlParameter.Optional },
            //   new { lang = @"ru|en" },
            //   new[] { "Listelli.Controllers" }
            //);

            //routes.MapRoute(
            //   "CategoryDetails", // Route name
            //   "{lang}/category/{id}", // URL with parameters
            //   new { controller = "Category", action = "Details", id = UrlParameter.Optional },
            //   new { lang = @"ru|en" },
            //   new[] { "Listelli.Controllers" }
            //);


            routes.MapRoute(
               "News", // Route name
               "{lang}/news", // URL with parameters
               new { controller = "Home", action = "Articles", id = UrlParameter.Optional },
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



            routes.MapRoute("lang", "{lang}/{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional }, new { lang = @"ru|en" }, new[] { "Listelli.Controllers" }
            );


            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional, lang = "ru" } // Parameter defaults
                , new[] { "Listelli.Controllers" }
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);


            if (Application["mailSender"] == null)
            {
                Thread newThread = new Thread(new ThreadStart(Work.DoWork));
                
                newThread.Start();

                Application["mailSender"] = newThread;
            }
        }

        class Work
        {
            Work() { }

            public static void DoWork()
            {
                while (true)
                {
                    Thread.Sleep(100);
                }
            }
        }
    }
}