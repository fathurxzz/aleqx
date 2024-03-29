<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="CollectStickers.Helpers" %>
<%
    bool authenticated = (bool)ViewData["authenticated"];
    bool IsAdmin = (bool)ViewData["isAdmin"];
%>
<%=Html.ResourceActionLink("MainPage","Index","Home").ToLower() %>
<%if(authenticated){ %>
<span style="color: #ffffff"><b>|</b></span>
<%=Html.ResourceActionLink("EditStickerInfo", "EditStickerInfo", "Home").ToLower()%>
<span style="color: #ffffff"><b>|</b></span>
<%=Html.ResourceActionLink("StickersSummary", "StickersSummary", "Home").ToLower()%>
<span style="color: #ffffff"><b>|</b></span>
<%=Html.ResourceActionLink("Coincidences", "Coincidences", "Home").ToLower()%>
<%} %>
<%if(IsAdmin){ %>
<span style="color: #ffffff"><b>|</b></span>
<%=Html.ResourceActionLink("Users", "Users", "Home").ToLower()%>
<%} %>

