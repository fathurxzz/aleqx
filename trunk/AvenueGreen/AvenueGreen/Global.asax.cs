using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.IO;
using AvenueGreen.Helpers;

namespace AvenueGreen
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                "Admin",                                              // Route name
                "Admin/{action}",                           // URL with parameters
                new { controller = "Admin", action = "Index", id = "" }  // Parameter defaults
            );


            routes.MapRoute(
                "News",                                              // Route name
                "News/{action}",                           // URL with parameters
                new { controller = "News", action = "Index", id = "" }  // Parameter defaults
            );

            routes.MapRoute(
                "Galleries",                                              // Route name
                "Galleries/{id}",                           // URL with parameters
                new { controller = "Galleries", action = "Index", id = "" }  // Parameter defaults
            );

            routes.MapRoute(
                "Widgets",                                              // Route name
                "Widgets/{type}",                           // URL with parameters
                new { controller = "Widget", action = "Index", type = "" }  // Parameter defaults
            );

            routes.MapRoute(
                "Search",                                              // Route name
                "Search/{action}",                           // URL with parameters
                new { controller = "Search", action = "Index", id = "" }  // Parameter defaults
            );

            routes.MapRoute(
            "Content",                                              // Route name
            "{id}",                           // URL with parameters
            new { controller = "Content", action = "Index", id = "Services" }  // Parameter defaults
            );

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Content", action = "Index", id = "Services" }  // Parameter defaults
            );
            
            
            routes.MapRoute(
                "Default1",                                              // Route name
                "{controller}/{action}",                           // URL with parameters
                new { controller = "Content", action = "Index", id = "Services" }  // Parameter defaults
            );
            

        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_BeginRequest()
        {
            if (Request.Path.Contains("/ImageCache/"))
            {
                string fileName = Path.GetFileName(Server.MapPath(Request.Path));

                string folder = Request.Path.Replace("/" + fileName, "");
                folder = folder.Substring(folder.LastIndexOf("/") + 1);

                string path = GraphicsHelper.GetCachedImage("~/Content/GalleryImages", fileName, folder);
            }
        }
    }
}