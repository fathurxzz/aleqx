<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="loginTitle" ContentPlaceHolderID="TitleContent" runat="server">
    ViaCon - Вход в систему администрирования
</asp:Content>

<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Вход в систему администрирования</h2>
    <p>
        Пожалуйста введите логин и пароль.
    </p>
    <%= Html.ValidationSummary("Аутентификация пользователя не удалась. Пожалуйста проверте данные и повторите попытку.") %>

    <% using (Html.BeginForm()) { %>
        <div>
        
            <table>
            <tr>
                <td><label for="username">Логин:</label></td>
                <td>
                    <%= Html.TextBox("username") %>
                    <%= Html.ValidationMessage("username") %>
                </td>
            </tr>
            <tr>
                <td><label for="password">Пароль:</label></td>
                <td><%= Html.Password("password") %><%= Html.ValidationMessage("password") %></td>
            </tr>
            <tr>
                <td colspan="2"><%= Html.CheckBox("rememberMe") %> <label class="inline" for="rememberMe">запомнить?</label></td>
            </tr>
            </table>
            <input type="submit" value="Вход" />
        </div>
    <% } %>
</asp:Content>
