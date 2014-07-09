using System.Web.Mvc;

namespace Leo.Areas.Admin
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
               "Admin_Category",
               "{lang}/admin/{controller}/{action}/{id}",
               new { controller = "Admin", action = "Default", id = UrlParameter.Optional },
               new { lang = @"ru|en" },
               new[] { "Leo.Areas.Admin.Controllers" }
           );

            //context.MapRoute(
            //    "Admin_default",
            //    "admin/{controller}/{action}/{id}",
            //    new { controller = "Admin", action = "Default", id = UrlParameter.Optional},
            //    new[] { "Leo.Areas.Admin.Controllers" }
            //);
        }
    }
}
