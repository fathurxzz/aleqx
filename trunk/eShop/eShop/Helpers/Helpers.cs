using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.Helpers
{
    public static class Helpers
    {
        public static string ResourceString(this System.Web.Mvc.HtmlHelper helper, string resourceName)
        {
            return Controllers.ResourcesHelper.GetResourceString(resourceName);
        }
    }
}
