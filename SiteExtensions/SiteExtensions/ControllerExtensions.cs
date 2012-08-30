using System.Web.Mvc;
namespace SiteExtensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ControllerExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="model"></param>
        public static void SetSeoContent(this Controller controller, ISiteModel model)
        {
            controller.ViewBag.Title = model.Title;
            controller.ViewBag.SeoDescription = model.SeoDescription;
            controller.ViewBag.SeoKeywords = model.SeoKeywords;
        }
    }
}
