<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= Html.Encode(ViewData["userId"]) %></h2>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="leftMenuContent" runat="server">
</asp:Content>
