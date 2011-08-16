using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.UI.WebControls;

namespace Exchange.Models.Helpers
{
    public static partial class Helper
    {
        public static string RegisterCss(this HtmlHelper helper, string cssFileName)
        {
            string cssPath = VirtualPathUtility.ToAbsolute("~/Content");
            string linkSource = "<link rel=\"Stylesheet\" href=\"{0}/{1}\" />";
            return string.Format(linkSource, cssPath, cssFileName);
        }

        public static string RegisterJS(this HtmlHelper helper, string scriptLib)
        {
            //get the directory where the scripts are
            string scriptRoot = VirtualPathUtility.ToAbsolute("~/Scripts");
            string scriptFormat = "<script src=\"{0}/{1}\" type=\"text/javascript\"></script>\r\n";
            return string.Format(scriptFormat, scriptRoot, scriptLib);
        }

        public static string ResourceString(this HtmlHelper helper, string resourceName)
        {
            return GetResourceString(resourceName);
        }

        public static string ResourceLabel(this HtmlHelper helper, string resourceName)
        {
            return string.Format("<label>{0}</label>", GetResourceString(resourceName));
        }

        public static string DropDownList(this HtmlHelper helper, string name, IEnumerable<ClientsSelectListItem> selectList)
        {
            var s = new StringBuilder();
            s.AppendFormat("<select id=\"{0}\" name=\"{0}\">", name);
            foreach (var item in selectList)
            {
                s.AppendFormat("<option value=\"{0}\" subjId=\"{1}\" cntrCode=\"{2}\" okpo=\"{3}\" {4} >{5}</option>", HttpUtility.HtmlEncode(item.Value), item.SubjId, item.CntrCode, item.Okpo, item.Selected ? " selected " : "", item.Text);
            }
            s.Append("</select>");
            return s.ToString();
        }

        public static MvcHtmlString ResourceActionLink(this HtmlHelper helper, string resourceName, string actionName, string conrollerName)
        {
            string linkText = GetResourceString(resourceName);
            return helper.ActionLink(linkText, actionName, conrollerName);
        }

        public static MvcHtmlString ResourceActionLink(this HtmlHelper helper, string resourceName, string actionName, string conrollerName, object routeValues, object htmlAttributes)
        {
            string linkText = GetResourceString(resourceName);
            return helper.ActionLink(linkText, actionName, conrollerName, routeValues, htmlAttributes);
        }

        public static MvcHtmlString ResourceActionLink(this HtmlHelper helper, string resourceName, string actionName, object routeValues)
        {
            string linkText = GetResourceString(resourceName);
            return helper.ActionLink(linkText, actionName, routeValues);
        }

        public static MvcHtmlString ResourceActionLink(this HtmlHelper helper, string resourceName, string actionName, object routeValues, object htmlAttributes)
        {
            string linkText = GetResourceString(resourceName);
            return helper.ActionLink(linkText, actionName, routeValues, htmlAttributes);
        }

        public static MvcHtmlString ResourceActionLink(this HtmlHelper helper, string resourceName, string actionName)
        {
            string linkText = GetResourceString(resourceName);
            return helper.ActionLink(linkText, actionName);
        }

        public static MvcHtmlString ResourceActionLink(this HtmlHelper helper, string resourceName, string actionName, IEnumerable<SiteRoles> roles)
        {
            if (!WebSession.CurrentUser.HasRole(roles))
                return MvcHtmlString.Empty;

            string linkText = GetResourceString(resourceName);
            return helper.ActionLink(linkText, actionName);
        }

        public static MvcHtmlString ResourceActionLink(this HtmlHelper helper, string resourceName, string actionName, string conrollerName, object routeValues, object htmlAttributes, IEnumerable<SiteRoles> roles)
        {
            if (!WebSession.CurrentUser.HasRole(roles))
                return MvcHtmlString.Empty;

            

            string linkText = GetResourceString(resourceName);
            return helper.ActionLink(linkText, actionName,conrollerName,routeValues, htmlAttributes);
        }
        
        
        public static MvcHtmlString ResourceMenuActionLink(this HtmlHelper helper, string resourceName, string actionName, string conrollerName, object routeValues, IEnumerable<SiteRoles> roles, Dictionary<object,object> currentEntityValues)
        {
            if (!WebSession.CurrentUser.HasRole(roles))
                return MvcHtmlString.Empty;

            string className = "";

            if (currentEntityValues.ContainsKey("controller") && (string)currentEntityValues["controller"] == conrollerName &&
                currentEntityValues.ContainsKey("action") && (string)currentEntityValues["action"] == actionName)
            {
                className = "selected";
            }

            if (currentEntityValues.ContainsKey("operId") && currentEntityValues["operId"]!=null)
            {
                className = "";
                if (((int)currentEntityValues["operId"] == 1 && conrollerName == "TendersBuy") || ((int)currentEntityValues["operId"] == 2 && conrollerName == "TendersSell"))
                {
                    className = "selected";
                }
            }
            string linkText = GetResourceString(resourceName);
            return helper.ActionLink(linkText, actionName, conrollerName, routeValues, new {@class = className});
        }
        

        public static MvcHtmlString ResourceActionLink(this HtmlHelper helper, string resourceName, string actionName, string conrollerName, IEnumerable<SiteRoles> roles)
        {
            if (!WebSession.CurrentUser.HasRole(roles))
                return MvcHtmlString.Empty;

            string linkText = GetResourceString(resourceName);
            return helper.ActionLink(linkText, actionName, conrollerName);
        }


        public static MvcHtmlString ResourceActionLink(this HtmlHelper helper, string resourceName, string actionName, RouteValueDictionary routeValues)
        {
            string linkText = GetResourceString(resourceName);
            return helper.ActionLink(linkText, actionName, routeValues);
        }

        public static string ResourceButton(this HtmlHelper helper, string resourceName, string onclickScript)
        {
            string linkText = GetResourceString(resourceName);
            return string.Format(
                "<input class=\"UIButton\" type=\"button\" value=\"{0}\" onclick=\"{1}\" />", linkText,
                onclickScript);
        }

        public static string ResourceSubmitButton(this HtmlHelper helper, string resourceName)
        {
            string linkText = GetResourceString(resourceName);
            return string.Format("<input class=\"UIButton\" type=\"submit\" value=\"{0}\" />", linkText);
        }

        public static string ResourceSubmitButton(this HtmlHelper helper, string resourceName, FormAction formAction)
        {
            string linkText = GetResourceString(resourceName);
            return string.Format("<input class=\"UIButton\" type=\"submit\" value=\"{0}\" name=\"fa{1}\" />", linkText, formAction);
        }

        public static string ResourceSubmitButton(this HtmlHelper helper, string resourceName, FormAction formAction, string resourseConfirmMessage)
        {
            string linkText = GetResourceString(resourceName);
            string confirmMessage = GetResourceString(resourseConfirmMessage);
            return string.Format("<input class=\"UIButton\" type=\"submit\" value=\"{0}\" name=\"fa{1}\" onclick=\"return confirm('{2}')\" />", linkText, formAction, confirmMessage);
        }

        //public static string ResourceLabelFor(this MvcHtmlString htmlHelper)

    }
}