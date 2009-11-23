<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Includes" runat="server">
	
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


<div id="contentContainer">
<div id="contentContainerTop"></div>
<div id="innerContentContainer">
<%= ViewData["text"]%>

<%
string controllerName = ViewContext.RouteData.Values["controller"].ToString().ToUpperInvariant();
if (controllerName == "SERVICES")
{
    %>
    <% Html.RenderPartial("CargoTeleport");%>
    <%
}
%>
</div>
<div id="contentContainerBottom"></div>
</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="LeftContent" runat="server">
    <% Html.RenderPartial("SubMenu");%>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentTitleContent" runat="server">
<%= ViewData["title"]%>
</asp:Content>
