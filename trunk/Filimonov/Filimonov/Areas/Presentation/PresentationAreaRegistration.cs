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
                "platform",
                new { controller = "Home", action = "Index" }
            );

            context.MapRoute(
                "Survey",
                "platform/survey",
                new { controller = "Survey", action = "Index" }
            );


            context.MapRoute(
                "SurveyDetails",
                "platform/surveys/{id}",
                new { controller = "Survey", action = "Details", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Categories",
                "platform/categories",
                new { controller = "Category", action = "Index" }
            );

            context.MapRoute(
                "Notes",
                "platform/notes",
                new { controller = "Note", action = "Index" }
            );

            context.MapRoute(
                "ClientAuthentication",
                "platform/customerlogon/{id}",
                new { controller = "Customer", action = "LogOn", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "ClientDetails",
                "platform/usercabinet/{id}",
                new { controller = "UserCabinet", action = "Details", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "CategoryDetails",
                "platform/categories/{id}",
                new { controller = "Category", action = "Details", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Presentation_default",
                "platform/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "Filimonov.Areas.Presentation.Controllers" }
            );

            
        }
    }
}
