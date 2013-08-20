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
             "to/{id}",
             new { controller = "Designer", action = "Details" }
            );

            context.MapRoute(
             "DesignerLogon",
             "to/{id}/logon",
             new { controller = "Designer", action = "LogOn" }
            );

            context.MapRoute(
             "DesignerRoomDetails",
             "to/{id}/living",
             new { controller = "Designer", action = "RoomDetails", roomType=1 }
            );

            context.MapRoute(
             "DesignerRoomDetails1",
             "to/{id}/notliving",
             new { controller = "Designer", action = "RoomDetails", roomType = 2 }
            );


            context.MapRoute(
                "DesignersPortfolio_default",
                "to/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
