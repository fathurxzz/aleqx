﻿using System.Web.Mvc;

namespace Filimonov.Areas.Presentation
{
    public class PresentationAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Presentation";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Root",
                "presentation",
                new { controller = "Home", action = "Index" }
            );

            context.MapRoute(
                "Clients",
                "presentation/customers",
                new { controller = "Customer", action = "Index"}
            );

            context.MapRoute(
                "Categories",
                "presentation/categories",
                new { controller = "Category", action = "Index" }
            );

            context.MapRoute(
                "ClientDetails",
                "presentation/clients/{id}",
                new { controller = "Client", action = "Details", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "CategoryDetails",
                "presentation/categories/{id}",
                new { controller = "Category", action = "Details", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Presentation_default",
                "presentation/{controller}/{action}/{id}",
                new { controller="Home", action = "Index", id = UrlParameter.Optional }
            );

            
        }
    }
}
