<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="bigs.Helpers" %>
<%@ Import Namespace="bigs.Controllers" %>
<%
    string shortLang = SystemSettings.CurrentLanguageShort;
    string controllerName = ViewContext.RouteData.Values["controller"].ToString().ToUpperInvariant();
%>

<div id="subMenu">

<div>
<%=Html.ActionLink(Html.ResourceString("Transfers"), "Index", "Services", new { contentUrl = Html.ResourceString("Transfers") },null)%>
</div>        
<div>
<%=Html.ActionLink(Html.ResourceString("Insurance"), "Index", "Services", new { contentUrl = Html.ResourceString("Insurance") }, null)%>
</div>        
<div>
<%=Html.ActionLink(Html.ResourceString("Logistics"), "Index", "Services", new { contentUrl = Html.ResourceString("Logistics") }, null)%>
</div>                
</div>
