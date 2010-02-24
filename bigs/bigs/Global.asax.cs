using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace bigs
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.aspx/{*pathInfo}");
            routes.IgnoreRoute("captcha.ashx");
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("Upload.aspx");

            routes.MapRoute(
                "Captcha",                                              // Route name
                "Security/Captcha",                           // URL with parameters
                new { controller = "Security", action = "Captcha" }  // Parameter defaults
            );

            routes.MapRoute(
                "FileNotFound",                                              // Route name
                "Errors/NotFound",                           // URL with parameters
                new { controller = "Errors", action = "FileNotFound" }  // Parameter defaults
            );

            routes.MapRoute(
                "UpdateEmail",                                              // Route name
                "Admin/UpdateEmail",                           // URL with parameters
                new { controller = "Admin", action = "UpdateEmail" }  // Parameter defaults
            );

            routes.MapRoute(
                "UpdateButtonsState",                                              // Route name
                "Admin/UpdateButtonsState",                           // URL with parameters
                new { controller = "Admin", action = "UpdateButtonsState" }  // Parameter defaults
            );

            routes.MapRoute(
                "AddSubMenuItem",                                              // Route name
                "Admin/AddSubMenuItem",                           // URL with parameters
                new { controller = "Admin", action = "AddSubMenuItem" }  // Parameter defaults
            );

            routes.MapRoute(
              "EditPicture",                                              // Route name
              "Admin/EditPicture",                           // URL with parameters
              new { controller = "Admin", action = "EditPicture", id = "" }  // Parameter defaults
            );

            routes.MapRoute(
              "DeletePicture",                                              // Route name
              "Admin/DeletePicture/{id}",                           // URL with parameters
              new { controller = "Admin", action = "DeletePicture", id = "" }  // Parameter defaults
            );

            routes.MapRoute(
              "DeleteSubMenuItem",                                              // Route name
              "Admin/DeleteSubMenuItem/{id}",                           // URL with parameters
              new { controller = "Admin", action = "DeleteSubMenuItem", id = "" }  // Parameter defaults
            );


            routes.MapRoute(
              "Account",                                              // Route name
              "Account/{action}",                           // URL with parameters
              new { controller = "Account", action = "LogOn" }  // Parameter defaults
            );

            routes.MapRoute(
                "Languages",                                              // Route name
                "Languages/SetLanguage",                           // URL with parameters
                new { controller = "Languages", action = "SetLanguage" }  // Parameter defaults
            );

            routes.MapRoute(
                "Content",                                              // Route name
                "{controller}/{contentUrl}",                           // URL with parameters
                new { controller = "Home", action = "Index", contentUrl = "О компании" }  // Parameter defaults
            );

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{contentUrl}",                           // URL with parameters
                new { controller = "{controller}", action = "{action}", contentUrl = "" }  // Parameter defaults
            );





        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
        }
    }
}