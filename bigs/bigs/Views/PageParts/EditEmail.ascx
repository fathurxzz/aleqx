<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div id="editEmailContainer">
Email, на который будут отправлены письма с запросами<br />
<%using (Html.BeginForm())
  { %>
<%=Html.TextBox("email", ViewData["email"])%>
<input type="submit" value="Сохранить"/>
<%} %>
</div>