<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Excursions.Models.Excursion>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Kyiv Tours - Excursions
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.RenderPartial("Search"); %>
    <% if (Model != null)
           foreach (var item in Model)
           {
               Html.RenderPartial("Excursion", item);
           } %>
    <%
        if (Request.IsAuthenticated)
        {
    %>
    <div class="addExcursionLink">
    <%= Html.ActionLink("Add item", "AddEdit", "Excursions", new { area = "Admin" }, new { @class = "adminLink" })%>
    </div>
    <%
        } %>
</asp:Content>
