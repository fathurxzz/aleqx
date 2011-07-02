using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Babich;

namespace Dev.Mvc.Helpers
{
    public static class ResourceHelper
    {
        public static string GetResourceString(string resourceName)
        {
            return HttpContext.GetGlobalResourceObject("Resources", resourceName, GetSelectedCulture()).ToString();
        }

        public static string ResourceString(this HtmlHelper helper, string resourceName)
        {
            return GetResourceString(resourceName);
        }

        public static MvcHtmlString ResourceActionLink(this HtmlHelper helper, string resourceName, string actionName, string controllerName, object routeValues, object htmlAttributes)
        {
            return helper.ActionLink(GetResourceString(resourceName), actionName, controllerName, routeValues, htmlAttributes);
        }
        public static CultureInfo GetSelectedCulture()
        {
            return CultureInfo.GetCultureInfo(SiteSettings.GetCurrentLanguage);
        }
    }
}