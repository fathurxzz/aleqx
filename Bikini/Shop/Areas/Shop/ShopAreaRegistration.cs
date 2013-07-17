using System.Web.Mvc;

namespace Shop.Areas.Shop
{
    public class ShopAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Shop";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Shop_default",
                "shop/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
