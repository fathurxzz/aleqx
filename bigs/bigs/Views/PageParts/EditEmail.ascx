<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div id="editEmailContainer">
Email, �� ������� ����� ���������� ������ � ���������<br />
<%using (Html.BeginForm())
  { %>
<%=Html.TextBox("email", ViewData["email"])%>
<input type="submit" value="���������"/>
<%} %>
</div>