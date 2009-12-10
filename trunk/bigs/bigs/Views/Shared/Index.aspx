<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="bigs.Models" %>
<%@ Import Namespace="bigs.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= ViewData["title"]%>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Includes" runat="server">
	
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ContentContainer");%>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="LeftContent" runat="server">
    <% //Html.RenderPartial("SubMenu");%>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentTitleContent" runat="server">
<%= ViewData["title"]%>
</asp:Content>
