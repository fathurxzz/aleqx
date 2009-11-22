<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="bigs.Helpers" %>
<%@ Import Namespace="bigs.Models" %>
<%@ Import Namespace="bigs.Controllers" %>
<%
    string shortLang = SystemSettings.CurrentLanguageShort;
    string controllerName = ViewContext.RouteData.Values["controller"].ToString().ToUpperInvariant();

    using (DataStorage context = new DataStorage())
    {
        List<SiteContent> siteContent = (from content in context.SiteContent.Include("Parent") where content.Parent.Name == controllerName && content.Language == SystemSettings.CurrentLanguage orderby content.SortOrder ascending select content).ToList();
        if (siteContent.Count > 0)
        {
            %>
            <div id="subMenu">
            <%
            foreach (var content in siteContent)
            {
                    %>
                    <div>
                    <%=Html.ActionLink(Html.ResourceString(content.Name), "Index", content.Parent.Name, new { contentUrl = Html.ResourceString(content.Name) }, null)%>
                    </div>
                    <%
            }
            %>
            </div>
            <%
        }

    }
%>
