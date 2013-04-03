using System.Web.Mvc;

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
                "Survey",
                "presentation/survey",
                new { controller = "Survey", action = "Index" }
            );


            context.MapRoute(
                "SurveyDetails",
                "presentation/surveys/{id}",
                new { controller = "Survey", action = "Details", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Categories",
                "presentation/categories",
                new { controller = "Category", action = "Index" }
            );

            context.MapRoute(
                "ClientAuthentication",
                "presentation/customerlogon/{id}",
                new { controller = "Customer", action = "LogOn", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "ClientDetails",
                "presentation/usercabinet/{id}",
                new { controller = "UserCabinet", action = "Details", id = UrlParameter.Optional }
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
