<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="eShop.Helpers" %>

<div class="logOnProperties">
    <div class="logOnName">
        <%= Html.ResourceString("Welcome") %>,<br />
        <%= Profile.FirstName %> <%= Profile.LastName %>!
    </div>
    <%= Html.ResourceActionLink("LogOff", "LogOff", "Account") %>
</div>
