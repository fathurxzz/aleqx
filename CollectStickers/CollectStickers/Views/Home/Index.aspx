<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= Html.Encode(ViewData["Message"]) %></h2>
    
    <div style="padding-left:100px;text-align:left;">
    
    <h3>Как работает сервис:</h3>
    <ul>            
        <li>Вы регистрируетесь и указываете номера наклеек, которые вам нужны (и какие есть на обмен).</li>
        <li>Система подбирает оптимальных пользователей сервиса, с кем можно обменяться в вашем городе или по почте. </li>
        <li>Вы связываетесь с пользователем через личные сообщения и производите обмен.</li>
    </ul>
    
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="leftMenuContent" runat="server">

</asp:Content>
    