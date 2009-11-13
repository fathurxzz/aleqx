<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="CollectStickers.Helpers" %>

<div class="logOnProperties">
    <div class="logOnName">
        <%=Html.ResourceString("Welcome")%>, <font style="color:#df8d1f; font-weight:bold;"><%= Profile.UserName%></font>!
    </div>
    Редактировать профиль&nbsp;
    <%= Html.ResourceActionLink("LogOff", "LogOff", "Account") %>
</div>
