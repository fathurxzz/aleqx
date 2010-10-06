<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Excursions.Models.Content>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Kyiv Tours - <%=Model.Title %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%if (Request.IsAuthenticated)
      {
    %>
    <%=Html.ActionLink("Edit page", "AddEdit", "Content", new { area = "Admin",id=Model.Id }, new { @class = "adminLink" })%> | 
    <%=Html.ActionLink("Delete page", "Delete", "Content", new { area = "Admin", id = Model.Id }, new { @class = "adminLink", onclick = "return confirm('Are you sure?')" })%>
    <%
        } %>
    <%=Model.Text %>
</asp:Content>
