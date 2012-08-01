using System.Web.Mvc;
namespace SiteExtensions
{
    public static class ControllerExtensions
    {
        public static void SetSeoContent(this Controller controller, ISiteModel model)
        {
            controller.ViewBag.Title = model.Title;
            controller.ViewBag.SeoDescription = model.SeoDescription;
            controller.ViewBag.SeoKeywords = model.SeoKeywords;
        }
    }
}
