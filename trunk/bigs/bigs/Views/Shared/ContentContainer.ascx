<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="bigs.Models" %>
<%@ Import Namespace="bigs.Controllers" %>

<div id="contentContainer">
<div id="contentContainerTop"></div>
<div id="innerContentContainer">
<%
     string controller = (string)ViewContext.RouteData.Values["controller"];
    string contentName = (string)ViewData["contentName"];

    if (controller.ToLower() == "services" && contentName.ToLower() == "services" && ViewData["teleportObjectImageUrl"] != null)
    {
        if (!string.IsNullOrEmpty(ViewData["teleportObjectImageUrl"].ToString()) && !string.IsNullOrEmpty(ViewData["teleportMessage"].ToString()))
            Html.RenderPartial("TeleportedObject");
    
    } %>

<%= ViewData["text"]%>

<%
   

    switch (controller.ToLower())
    {
        case "requests": Html.RenderPartial("Request");
            break;
        case "services":
            {
                if (contentName.ToLower() == "services")
                {
                    
                    Html.RenderPartial("CargoTeleport");
                }
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