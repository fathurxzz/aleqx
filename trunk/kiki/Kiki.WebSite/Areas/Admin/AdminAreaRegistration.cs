using System.Web.Mvc;

namespace Kiki.WebSite.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(
                  "Admin",
                  "admin",
                  new { controller = "Admin", action = "Default", lang = "ru" },
                  new { lang = @"ru|ua" },
                  new[] { "Kiki.WebSite.Areas.Admin.Controllers" }
              );


            context.MapRoute(
                "Admin_default",
                "{lang}/admin/{controller}/{action}/{id}",
                new { controller="Admin", action = "Default", id = UrlParameter.Optional },
                new { lang = @"ru|ua" },
                new[] { "Kiki.WebSite.Areas.Admin.Controllers" }
            );
        }
    }
}
