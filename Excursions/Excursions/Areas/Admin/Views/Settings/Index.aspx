<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Настройки
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Настройки</h2>

    <% using(Html.BeginForm("Update","Settings",FormMethod.Post))
        {
        %>
    <table class="adminEditContentTable">
        <tr>
            <td>Email для нотификации</td>
            <td><%=Html.TextBox("NotificationEmail", ViewData["NotificationEmail"],new { style = "width:400px;"})%> </td>
        </tr>
    </table>
    <input type="submit"  value="Сохранить"/>
    <%
        }%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>
