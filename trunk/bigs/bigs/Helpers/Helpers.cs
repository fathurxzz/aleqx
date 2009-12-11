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
using bigs.Models;



namespace bigs.Helpers
{
    public static class Helpers
    {
        public static string ResourceActionLink(this System.Web.Mvc.HtmlHelper helper, string resourceName, string actionName, string conrollerName)
        {
            string linkText = Controllers.ResourcesHelper.GetResourceString(resourceName);
            return helper.ActionLink(linkText, actionName, conrollerName);
        }

        public static string ResourceActionLink<TController>(this System.Web.Mvc.HtmlHelper helper, object htmlAttributes, Expression<Action<TController>> action) where TController : Controller
        {
            return helper.ActionLink<TController>(action, " ", htmlAttributes);
        }

        public static string ResourceString(this System.Web.Mvc.HtmlHelper helper, string resourceName)
        {
            return Controllers.ResourcesHelper.GetResourceString(resourceName);
        }

        public static string CaptchaImage(this HtmlHelper helper, int height, int width)
        {
            CaptchaImage image = new CaptchaImage
            {
                Height = height,
                Width = width,
            };

            HttpRuntime.Cache.Add(
                image.UniqueId,
                image,
                null,
                DateTime.Now.AddSeconds(bigs.CaptchaImage.CacheTimeOut),
                Cache.NoSlidingExpiration,
                CacheItemPriority.NotRemovable,
                null);

            StringBuilder stringBuilder = new StringBuilder(256);
            stringBuilder.Append("<input type=\"hidden\" name=\"captcha-guid\" value=\"");
            stringBuilder.Append(image.UniqueId);
            stringBuilder.Append("\" />");
            stringBuilder.AppendLine();
            stringBuilder.Append("<img src=\"");
            stringBuilder.Append("/Security/Captcha/?guid=" + image.UniqueId);
            stringBuilder.Append("\" alt=\"CAPTCHA\" width=\"");
            stringBuilder.Append(width);
            stringBuilder.Append("\" height=\"");
            stringBuilder.Append(height);
            stringBuilder.Append("\" />");

            return stringBuilder.ToString();
        }

    }
}
