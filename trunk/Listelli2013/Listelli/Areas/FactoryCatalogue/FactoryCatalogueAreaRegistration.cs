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
