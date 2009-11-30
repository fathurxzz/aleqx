<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="bigs.Models" %>
<%@ Import Namespace="bigs.Controllers" %>

<div id="contentContainer">
<div id="contentContainerTop"></div>
<div id="innerContentContainer">
<%= ViewData["text"]%>

<%
    string controller = (string)ViewContext.RouteData.Values["controller"];
    
    using (DataStorage context = new DataStorage())
    {
        string contentName = string.Empty;
        string contentUrl = (string)ViewData["contentUrl"];
        if (contentUrl != null)
            contentName = (from contentNames in context.SiteContent where contentNames.Url == contentUrl /*&& contentNames.Language == SystemSettings.CurrentLanguage*/ select contentNames.Name).First();
    
    

    if ( contentName.ToLower()== "transfers")
    {
    %>
    <% Html.RenderPartial("CargoTeleport");%>
    <%
        }
    }

    if (controller.ToLower() == "requests")
        Html.RenderPartial("Request");
        
%>
</div>
<div id="contentContainerBottom"></div>
</div>