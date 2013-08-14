using System.Web.Mvc;

namespace Listelli.Areas.DesignersPortfolio
{
    public class DesignersPortfolioAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "DesignersPortfolio";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
             "DesignerDetails",
             "portfolio/{id}",
             new { controller="Designer", action = "Details" }
         );

            context.MapRoute(
                "DesignersPortfolio_default",
                "portfolio/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
