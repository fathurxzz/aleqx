<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="bigs.Models" %>
<%@ Import Namespace="bigs.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= ViewData["title"]%>
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
        string contentName = string.Empty;
        string contentUrl = (string)ViewData["contentUrl"];
        if (contentUrl != null)
            contentName = (from contentNames in context.SiteContent where contentNames.Url == contentUrl && contentNames.Language == SystemSettings.CurrentLanguage select contentNames.Name).First();
    
    

if ( contentName.ToLower()== "transfers")
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
