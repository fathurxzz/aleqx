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
                "AdminBrandGroupItem",
                "{lang}/admin/brandgroupitem/{brandId}/{type}/{action}",
                new { controller = "BrandGroupItem", brandId = UrlParameter.Optional, type = UrlParameter.Optional }, new { lang = @"ru|en" }
            );

            context.MapRoute(
                "AdminBrandGroupItemImage",
                "{lang}/admin/brandgroupitem/{brandId}/{action}",
                new { controller = "BrandGroupItem", brandId = UrlParameter.Optional }, new { lang = @"ru|en" }
            );

            context.MapRoute(
                "AdminBrandItem",
                "{lang}/admin/branditem/{brandId}/{type}/{action}",
                new { controller = "BrandItem", brandId = UrlParameter.Optional, type=UrlParameter.Optional }, new { lang = @"ru|en" }
            );

            context.MapRoute(
                "AdminBrandItemImage",
                "{lang}/admin/branditem/{brandId}/{action}",
                new { controller = "BrandItem", brandId = UrlParameter.Optional }, new { lang = @"ru|en" }
            );

            context.MapRoute(
                "AdminCategoryBrandItem",
                "{lang}/admin/categorybranditem/{brandId}/{action}",
                new { controller = "CategoryBrandItem", brandId = UrlParameter.Optional }, new { lang = @"ru|en" }
            );

            context.MapRoute(
                "AdminBrand",
                "{lang}/admin/brand/{brandId}/{action}",
                new { controller = "Brand", brandId = UrlParameter.Optional }, new { lang = @"ru|en" }
            );

            context.MapRoute(
                "AdminCategoryBrand",
                "{lang}/admin/categorybrand/{category}/{action}",
                new { controller = "CategoryBrand", category = UrlParameter.Optional}, new { lang = @"ru|en" }
            );

            context.MapRoute(
             "Designers",
             "admin/designers",
             new { controller = "Designer", action = "Index" }
         );

            context.MapRoute(
                "Designer",
                "admin/designer/{action}/{id}",
                new { controller = "Designer", id = UrlParameter.Optional }
            );

           

            

            context.MapRoute(
                "Admin_default",
                "admin/{lang}/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }, new { lang = @"ru|en" }
            );
        }
    }
}
