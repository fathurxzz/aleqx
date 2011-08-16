using System;
using System.Web.Mvc;
using System.Web.Routing;
using Exchange.Models;

namespace Exchange
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
                "Accounts", // Route name
                "Accounts/GetAccounts/{officeId}/{operId}/{curId}/{operSign}", // URL with parameters
                new
                    {
                        controller = "Accounts",
                        action = "GetAccounts",
                        officeId = UrlParameter.Optional,
                        operId = UrlParameter.Optional,
                        curId = UrlParameter.Optional,
                        operSign = UrlParameter.Optional
                    } // Parameter defaults
                );

            routes.MapRoute(

                "Clients", // Route name
                "Tenders/GetClients/{podrid}/{okpo}/{contrCode}", // URL with parameters
                new
                    {
                        controller = "Tenders",
                        action = "GetClients",
                        podrid = UrlParameter.Optional,
                        okpo = UrlParameter.Optional,
                        contrCode = UrlParameter.Optional
                    } // Parameter defaults
                );


            routes.MapRoute(
                "TendersBuy",
                "TendersBuy",
                new { controller = "ExchangeTenders", action = "Index", operId = 1 },
                new string[1] { "Exchange.Controllers" });

            routes.MapRoute(
                "TendersSell",
                "TendersSell",
                new { controller = "ExchangeTenders", action = "Index", operId = 2 },
                new string[1] { "Exchange.Controllers" });


            routes.MapRoute(
                "Tenders", 
                "Tenders", 
                new {controller = "Tenders", action = "Index"},
                new string[1] {"Exchange.Controllers"});




            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start(object sender, EventArgs e)
        {


            //if (Request.QueryString["action"] == "login")
            //{
            //}
            //Response.Redirect("~/");

#if(DEBUG)
            Response.Redirect(string.Format("~/{0}Users/Login", ""));
            return;
                /*var userId = 2; //Москаленко
                user = new User(userId);*/
            
#else
            User user = new User();
            if(user.SiteRoleIds.Count<=0)
            {
                Session.Abandon();
                Response.Redirect("~/Error/Autentification/");
            }
#endif

            //Session["user"] = user;
            
        }
    }
}