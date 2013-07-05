using System.Web.Mvc;

namespace Listelli.Areas.Admin
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
                "AdminCategoryBrandItem",
                "{lang}/admin/categorybranditem/{brandId}/{action}",
                new { controller = "CategoryBrandItem", brandId = UrlParameter.Optional },
                constraints: new { lang = @"ru|en" }
            );

            context.MapRoute(
                "AdminBrand",
                "{lang}/admin/brand/{brandId}/{action}",
                new { controller = "Brand", brandId = UrlParameter.Optional },
                constraints: new { lang = @"ru|en" }
            );

            context.MapRoute(
                "AdminCategoryBrand",
                "{lang}/admin/categorybrand/{category}/{action}",
                new { controller = "CategoryBrand", category = UrlParameter.Optional},
                constraints: new { lang = @"ru|en" }
            );

            

            context.MapRoute(
                "Admin_default",
                "admin/{lang}/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                constraints: new { lang = @"ru|en" }
            );
        }
    }
}
