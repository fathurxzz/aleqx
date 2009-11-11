    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;
using CollectStickers.Models;

namespace CollectStickers.Helpers
{
    public static class Helpers
    {
        public static string RegisterCss(this System.Web.Mvc.HtmlHelper helper, string relativePath)
        {
            string cssPath = VirtualPathUtility.ToAbsolute(relativePath);
            string linkSource = "<link rel=\"Stylesheet\" href=\"{0}\" />";
            return string.Format(linkSource, cssPath);
        }

        public static string RegisterJS(this System.Web.Mvc.HtmlHelper helper, string scriptLib)
        {
            //get the directory where the scripts are
            string scriptRoot = VirtualPathUtility.ToAbsolute("~/Scripts");
            string scriptFormat = "<script src=\"{0}/{1}\" type=\"text/javascript\"></script>\r\n";
            return string.Format(scriptFormat, scriptRoot, scriptLib);
        }

        public static string ResourceString(this System.Web.Mvc.HtmlHelper helper, string resourceName)
        {
            return Controllers.ResourcesHelper.GetResourceString(resourceName);
        }

        public static string ResourceActionLink(this System.Web.Mvc.HtmlHelper helper, string resourceName, string actionName, string controllerName)
        {
            string linkText = Controllers.ResourcesHelper.GetResourceString(resourceName);
            return helper.ActionLink(linkText, actionName, controllerName);
        }

        public class SortStickers : IComparer<StickerPresentation>
        {
            public int Compare(StickerPresentation x, StickerPresentation y)
            {
                if (x == null)
                {
                    if (y == null)
                        return 0;
                    else
                        return -1;
                }
                else if (y == null)
                    return 1;
                else
                    return x.Number.CompareTo(y.Number);
            }
        }
    }
}
