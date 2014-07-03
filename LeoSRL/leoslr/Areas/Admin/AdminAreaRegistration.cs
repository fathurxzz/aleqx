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
               new { id = UrlParameter.Optional }, 
               new { lang = @"ru|en" }
           );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
