<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Honda.Models.Content>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	СП «Биомедика-Сервис»<% if (Model != null) Response.Write(" - " + Model.Title); %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentBoxTitle" runat="server">
<% if (Model != null) Response.Write(Model.Title); %>
</asp:Content>
