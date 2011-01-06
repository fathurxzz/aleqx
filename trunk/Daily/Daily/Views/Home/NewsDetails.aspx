<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Daily.Models.News>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	NewsDetails
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<%=Model.Content %>        
 
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="LeftContent" runat="server">
    <%
        Html.RenderPartial("NewsTitleList");
    %>
</asp:Content>

