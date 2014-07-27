using System.Web.Mvc;

namespace Shop.WebSite.Areas.Admin
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
                  new { lang = @"ru|en" },
                  new[] { "Shop.WebSite.Areas.Admin.Controllers" }
              );

            context.MapRoute(
               "Admin_Category",
               "{lang}/admin/{controller}/{action}/{id}",
               new { controller = "Admin", action = "Default", id = UrlParameter.Optional },
               new { lang = @"ru|en" },
               new[] { "Shop.WebSite.Areas.Admin.Controllers" }
           );
        }
    }
}
