<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Excursions.Models.Excursion>>" %>
<%@ Import Namespace="Excursions.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">

    <%
        var excursionType = (ExcursionType) ViewData["ExcursionType"];
    %>

    Kyiv Tours - <%=excursionType == ExcursionType.Both ? "Excursions" : excursionType + "s"%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <%
        using(Html.BeginForm("Index","Search",FormMethod.Post))
        {
          %>
          <div class="search">
          <span>Site Search</span><%=Html.TextBox("q")%>
          </div>
          <%
        }
         %>


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
