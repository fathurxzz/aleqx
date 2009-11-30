<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="bigs.Models" %>
<%@ Import Namespace="bigs.Controllers" %>

<div id="contentContainer">
<div id="contentContainerTop"></div>
<div id="innerContentContainer">
<%= ViewData["text"]%>

<%
    string controller = (string)ViewContext.RouteData.Values["controller"];
    
    string contentName = (string)ViewData["contentName"];
    if ( contentName.ToLower()== "transfers")
    {
    %>
    <% Html.RenderPartial("CargoTeleport");%>
    <%
        }
    if (controller.ToLower() == "requests")
        Html.RenderPartial("Request");
        
%>
</div>
<div id="contentContainerBottom"></div>
</div>