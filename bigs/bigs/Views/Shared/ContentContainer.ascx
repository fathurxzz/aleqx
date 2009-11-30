<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div id="contentContainer">
<div id="contentContainerTop"></div>
<div id="innerContentContainer">
<%= ViewData["text"]%>

<%
//    using (DataStorage context = new DataStorage())
//    {
//        string contentName = string.Empty;
//        string contentUrl = (string)ViewData["contentUrl"];
//        if (contentUrl != null)
//            contentName = (from contentNames in context.SiteContent where contentNames.Url == contentUrl && contentNames.Language == SystemSettings.CurrentLanguage select contentNames.Name).First();
    
    

//if ( contentName.ToLower()== "transfers")
//{
    %>
    <% //Html.RenderPartial("CargoTeleport");%>
    <%
//}
//        }

        Html.RenderPartial("Request");
        
%>
</div>
<div id="contentContainerBottom"></div>
</div>