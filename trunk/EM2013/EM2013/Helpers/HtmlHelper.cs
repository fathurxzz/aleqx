using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace EM2013.Helpers
{
    public static class HtmlHelper
    {
        public static MvcHtmlString ImageActionLink(this System.Web.Mvc.HtmlHelper helper, string originalPath, string fileName, string actionName, string controllerName, object routeValues, object htmlAttributes)
        {
            return ImageActionLink(helper, originalPath, fileName, actionName, controllerName, routeValues, htmlAttributes, 0, 0);
        }

        public static MvcHtmlString ImageActionLink(this System.Web.Mvc.HtmlHelper helper, string originalPath, string fileName, string actionName, string controllerName, object routeValues, object htmlAttributes, int imageWidth, int imageHeight)
        {
            StringBuilder sb = new StringBuilder();
            string formatString = "<img src=\"{0}\" alt=\"{1}\" width=\"{2}px\" height=\"{3}px\" />";
            sb.AppendFormat(formatString, Path.Combine(originalPath, fileName), fileName, imageWidth, imageHeight);
            return new MvcHtmlString(helper.ActionLink("[IMAGE]", actionName, controllerName, routeValues, htmlAttributes).ToString().Replace("[IMAGE]", sb.ToString()));
        }
    }
}