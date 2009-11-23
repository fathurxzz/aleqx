<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="bigs.Models" %>
<%@ Import Namespace="bigs.Controllers" %>

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
    using (DataStorage context = new DataStorage())
    {
        string contentUrl = (string)ViewData["contentUrl"];
        string contentName = (from contentNames in context.SiteContent where contentNames.Url == contentUrl && contentNames.Language == SystemSettings.CurrentLanguage select contentNames.Name).First();
    
    
//string controllerName = ViewContext.RouteData.Values["controller"].ToString().ToUpperInvariant();
if ( contentName.ToLower()== "transfers" /*controllerName == "SERVICES"*/)
{
    %>
    <% Html.RenderPartial("CargoTeleport");%>
    <%
}
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
