<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="bigs.Helpers" %>
<%@ Import Namespace="bigs.Models" %>
<%@ Import Namespace="bigs.Controllers" %>
<%
    string shortLang = SystemSettings.CurrentLanguageShort;
    string controllerName = ViewContext.RouteData.Values["controller"].ToString().ToUpperInvariant();

    using (DataStorage context = new DataStorage())
    {

        List<SiteContent> siteContent = (from content in context.SiteContent where content.Parent.Name.ToLower() == controllerName.ToLower() && content.Parent.Language == SystemSettings.CurrentLanguage select content).ToList();
        if (siteContent.Count > 0)
        {
        
%>
<div id="subMenu">
<div>
<%=Html.ActionLink(Html.ResourceString("Transfers"), "Index", "Services", new { contentUrl = Html.ResourceString("Transfers") }, null)%>
</div>        
<div>
<%=Html.ActionLink(Html.ResourceString("Insurance"), "Index", "Services", new { contentUrl = Html.ResourceString("Insurance") }, null)%>
</div>        
<div>
<%=Html.ActionLink(Html.ResourceString("Logistics"), "Index", "Services", new { contentUrl = Html.ResourceString("Logistics") }, null)%>
</div>                
</div>

<%
    }
    }
%>
