using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;

namespace eShop.Helpers
{
    public static class Helpers
    {
        public static string ResourceString(this System.Web.Mvc.HtmlHelper helper, string resourceName)
        {
            return Controllers.ResourcesHelper.GetResourceString(resourceName);
        }
        
        public static string ResourceActionLink(this System.Web.Mvc.HtmlHelper helper, string resourceName, string actionName, string conrollerName)
        {
            string linkText = Controllers.ResourcesHelper.GetResourceString(resourceName);
            return helper.ActionLink(linkText, actionName, conrollerName);
        }
    }


}
