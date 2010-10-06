using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Excursions.Helpers
{
    public static class AjaxExtensions
    {
        public static string DynamicCssInclude(this AjaxHelper helper, string url)
        {
            var tracker = new ResourceTracker(helper.ViewContext.HttpContext);
            if (tracker.Contains(url))
                return String.Empty;

            var sb = new StringBuilder();
            sb.AppendLine("<script type='text/javascript'>");
            sb.AppendLine("var link=document.createElement('link')");
            sb.AppendLine("link.setAttribute('rel', 'stylesheet');");
            sb.AppendLine("link.setAttribute('type', 'text/css');");
            sb.AppendFormat("link.setAttribute('href', '{0}');", url);
            sb.AppendLine();
            sb.AppendLine("var head = document.getElementsByTagName('head')[0];");
            sb.AppendLine("head.appendChild(link);");
            sb.AppendLine("</script>");
            return sb.ToString();
        }
    }
}