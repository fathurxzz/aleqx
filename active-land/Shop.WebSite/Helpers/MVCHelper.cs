using System;
using System.Web.Mvc;

namespace Shop.WebSite.Helpers
{
    public static class MVCHelper
    {
        public static void SetSeoContent(this Controller controller, ISiteModel model)
        {
            controller.ViewBag.Title = model.Title;
            controller.ViewBag.SeoDescription = model.SeoDescription;
            controller.ViewBag.SeoKeywords = model.SeoKeywords;
        }

        public static bool IsCurrentAction(this HtmlHelper helper, string actionName, string controllerName)
        {
            var currentControllerName = (string)helper.ViewContext.RouteData.Values["controller"];
            var currentActionName = (string)helper.ViewContext.RouteData.Values["action"];
            return currentControllerName.Equals(controllerName, StringComparison.CurrentCultureIgnoreCase) && currentActionName.Equals(actionName, StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool IsCurrentController(this HtmlHelper helper, string controllerName)
        {
            var currentControllerName = (string)helper.ViewContext.RouteData.Values["controller"];
            return currentControllerName.Equals(controllerName, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}