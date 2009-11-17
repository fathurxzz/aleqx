using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;
using bigs.Controllers;
using System.Linq.Expressions;
using System.Text;
using System.Globalization;
using System.Web.Caching;
using System.Web.Routing;
using Microsoft.Web.Mvc;
using System.Web.Mvc;



namespace bigs.Helpers
{
    public static class Helpers
    {
        public static string ResourceActionLink(this System.Web.Mvc.HtmlHelper helper, string resourceName, string actionName, string conrollerName)
        {
            string linkText = Controllers.ResourcesHelper.GetResourceString(resourceName);
            return helper.ActionLink(linkText, actionName, conrollerName);
        }
        /*
        public static string ActionLink<TController>(this System.Web.Mvc.HtmlHelper helper, Expression<Action<TController>> action) where TController : Controller
        {
            return helper.ActionLink<TController>(action);
        }*/
    }
}
