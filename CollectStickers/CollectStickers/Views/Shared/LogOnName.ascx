<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="CollectStickers.Helpers" %>

<div class="logOnProperties">
    <div class="logOnName">
        <%=Html.ResourceString("Welcome")%>, <%= Profile.UserName%>!
    </div>
    <%= Html.ResourceActionLink("LogOff", "LogOff", "Account") %>
</div>
