<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Excursions" %>
<div id="meta">
    <a href="~/Excursions" runat="server" class="marr">Home</a> | <a href="mailto:<%=ApplicationData.Email %>" class="marl marr">Email</a><!-- | <a href="#" class="marl">Search</a>-->
</div>