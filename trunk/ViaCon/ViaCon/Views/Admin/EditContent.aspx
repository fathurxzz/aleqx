<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EditContent
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>EditContent</h2>

<% using (Html.BeginForm("UpdateContent", "Admin", FormMethod.Post))
   { %>
   <% Html.RenderAction<ViaCon.Controllers.AdminController>(a => a.ContentList(null, 0)); %>
<%} %>

<%= Html.ActionLink("Добавить пункт", "AddContentItem")%>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentContainerTitle" runat="server">
</asp:Content>
