<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div id="editEmailContainer">
Email, �� ������� ����� ���������� ������ � ���������<br />
<%using (Html.BeginForm("UpdateEmail", "Admin", FormMethod.Post))
  { %>
<%= Html.Hidden("redirectUrl", Request.Url.AbsolutePath) %>
<%=Html.TextBox("email", ViewData["email"])%>
<input type="submit" value="���������"/>
<%} %>
</div>