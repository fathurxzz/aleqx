using System.Web.Mvc;

namespace Listelli.Areas.FactoryCatalogue
{
    public class FactoryCatalogueAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "FactoryCatalogue";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "FactoryCatalogue",
                "{lang}/factorycatalogue",
                new { controller="Category", action = "Index", id = UrlParameter.Optional },
                new { lang = @"ru|en" }
                
            );

            context.MapRoute(
                "FactoryCatalogueLogOn",
                "{lang}/factorycatalogue/logon",
                new { controller = "Customer", action = "LogOn", id = UrlParameter.Optional },
                constraints: new { lang = @"ru|en" }
            );

            context.MapRoute(
                "FactoryCatalogueRegister",
                "{lang}/factorycatalogue/register",
                new { controller = "Customer", action = "Register", id = UrlParameter.Optional },
                constraints: new { lang = @"ru|en" }
            );

            context.MapRoute(
                "FactoryCatalogueCategoryDetails",
                "{lang}/factorycatalogue/categories/{id}",
                new { controller = "Category", action = "Details", id = UrlParameter.Optional },
                constraints: new { lang = @"ru|en" }
            );

            context.MapRoute(
                "FactoryCatalogueCategoryBrandDetails",
                "{lang}/factorycatalogue/categories/{categoryId}/{id}",
                new { controller = "Brand", action = "Details", categoryId = UrlParameter.Optional, id = UrlParameter.Optional },
                constraints: new { lang = @"ru|en" }
            );

            context.MapRoute(
                "FactoryCatalogue_default",
                "{lang}/factorycatalogue/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                constraints: new { lang = @"ru|en" }
            );
        }
    }
}
