using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SiteExtensions
{
    public static class MVCHelper
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

        public static bool IsCurrentAction(this HtmlHelper helper, string actionName, string controllerName)
        {
            string currentControllerName = (string)helper.ViewContext.RouteData.Values["controller"];
            string currentActionName = (string)helper.ViewContext.RouteData.Values["action"];

            if (currentControllerName.Equals(controllerName, StringComparison.CurrentCultureIgnoreCase) && currentActionName.Equals(actionName, StringComparison.CurrentCultureIgnoreCase))
                return true;

            return false;
        }

        public static bool IsCurrentController(this HtmlHelper helper, string controllerName)
        {
            string currentControllerName = (string)helper.ViewContext.RouteData.Values["controller"];

            if (currentControllerName.Equals(controllerName, StringComparison.CurrentCultureIgnoreCase))
                return true;

            return false;
        }
    }
}
