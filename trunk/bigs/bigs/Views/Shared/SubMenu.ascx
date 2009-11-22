<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="bigs.Helpers" %>
<%@ Import Namespace="bigs.Models" %>
<%@ Import Namespace="bigs.Controllers" %>
<%
    string shortLang = SystemSettings.CurrentLanguageShort;
    string controllerName = ViewContext.RouteData.Values["controller"].ToString().ToUpperInvariant();




    using (DataStorage context = new DataStorage())
    {
        int parentId = context.SiteContent.Where(p => p.Name == controllerName && p.Language == SystemSettings.CurrentLanguage).Select(p => p.Id).FirstOrDefault();
        string parentName = context.SiteContent.Where(p => p.Name == controllerName && p.Language == SystemSettings.CurrentLanguage).Select(p => p.Name).FirstOrDefault();
        if (parentId != 0)
        {
            List<SiteContent> siteContent = (from content in context.SiteContent where content.ParentId == parentId && content.Language == SystemSettings.CurrentLanguage orderby content.SortOrder ascending select content).ToList();
            if (siteContent.Count > 0)
            {
                %>
                <div id="subMenu">
                <%
                foreach (var content in siteContent)
                {
                    %>
                    <div>
                    <%=Html.ActionLink(Html.ResourceString(content.Name), "Index", parentName, new { contentUrl = Html.ResourceString(content.Name) }, null)%>
                    </div>
                    <%
                }
                %>
                </div>
                <%
            }
        }
    }
%>
