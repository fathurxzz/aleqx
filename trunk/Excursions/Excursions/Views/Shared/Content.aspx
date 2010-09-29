<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Excursions.Models.Content>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Model.Title %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%if (Request.IsAuthenticated)
      {
    %>
    <%=Html.ActionLink("редактировать станицу", "AddEdit", "Content", new { area = "Admin",id=Model.Id }, new { @class = "adminLink" })%>
    <%
        } %>
    <%=Model.Text %>
</asp:Content>
