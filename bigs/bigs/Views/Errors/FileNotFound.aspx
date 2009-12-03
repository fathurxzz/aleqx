<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Не там свернули!
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


<div id="contentContainer">
<div id="contentContainerTop"></div>
<div id="innerContentContainer">
<% Html.RenderPartial("NotFound");%>
</div>
<div id="contentContainerBottom"></div>
</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="LeftContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentTitleContent" runat="server">
Не там свернули!
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>
