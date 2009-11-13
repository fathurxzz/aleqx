<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= Html.Encode(ViewData["Message"]) %></h2>
    
    Как работает сервис:
    <ul>            
        <li></li>
    </ul>
</asp:Content>

<asp:Content ContentPlaceHolderID="leftMenuContent" runat="server">

</asp:Content>
    