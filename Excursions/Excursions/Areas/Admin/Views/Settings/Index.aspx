<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Настройки
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h3>Настройки</h3>

    <% using(Html.BeginForm("Update","Settings",FormMethod.Post))
        {
        %>
    <table class="adminEditContentTable">
        <tr>
            <td valign="top" align="right">Email адреса для нотификации:</td>
            <td align="left"><%=Html.TextArea("NotificationEmail", (string)ViewData["NotificationEmail"],5,45,null)%> </td>
        </tr>
        <tr>
            <td valign="top" align="right">Email (ссылка вверху):</td>
            <td align="left"><%=Html.TextBox("Email", ViewData["Email"], new{style="width:300px;"})%> </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <input type="submit"  value="Сохранить"/>
            </td>
        </tr>
    </table>
    
    <%
        }%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>
