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

    switch (controller.ToLower())
    {
        case "requests": Html.RenderPartial("Request");
            break;
        case "services":
            {
                if (contentName.ToLower() == "services")
                Html.RenderPartial("CargoTeleport");
            }
            break;
        case "teleportsession":
            Html.RenderPartial("Teleport");
            break;
    }
%>
</div>
<div id="contentContainerBottom"></div>
</div>