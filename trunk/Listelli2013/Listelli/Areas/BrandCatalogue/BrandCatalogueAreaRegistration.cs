using System.Web.Mvc;

namespace Listelli.Areas.BrandCatalogue
{
    public class BrandCatalogueAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "BrandCatalogue";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(
                "BrandCatalogue",
                "{lang}/brandcatalogue",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new { lang = @"ru|en" },
                new[] { "Listelli.Areas.BrandCatalogue.Controllers" }
            );

            context.MapRoute(
                "BrandCatalogueBrands",
                "{lang}/brandcatalogue/brand/{id}",
                new { controller = "Home", action = "BrandGroupDetails", id = UrlParameter.Optional },
                new { lang = @"ru|en" },
                new[] { "Listelli.Areas.BrandCatalogue.Controllers" }
            );

            context.MapRoute(
                "BrandCatalogueBrandDetails",
                "{lang}/brandcatalogue/brand/{brandGroup}/{id}",
                new { controller = "Home", action = "BrandDetails", id = UrlParameter.Optional },
                new { lang = @"ru|en" },
                new[] { "Listelli.Areas.BrandCatalogue.Controllers" }
            );


            context.MapRoute(
                "BrandCatalogue_default",
                "{lang}/brandcatalogue/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new { lang = @"ru|en" },
                new[] { "Listelli.Areas.BrandCatalogue.Controllers" }
            );
        }
    }
}
