<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Excursions.Models.LogOnModel>" %>

<asp:Content ID="loginTitle" ContentPlaceHolderID="TitleContent" runat="server">
     Вход в систему администрирования
</asp:Content>

<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">
   <h3>Вход в систему администрирования</h3>
    <p>
        Пожалуйста введите логин и пароль.
    </p>
    <%= Html.ValidationSummary("Аутентификация пользователя не удалась. Пожалуйста проверте данные и повторите попытку.") %>

    <% using (Html.BeginForm()) { %>
        <div style="padding:10px;">
        
            <table class="logOnTable">
            <tr>
                <td align="right"><label for="username">Логин:</label></td>
                <td>
                    <%= Html.TextBox("username","",new{style="width:200px;"})%>
                    <%= Html.ValidationMessage("username") %>
                </td>
            </tr>
            <tr>
                <td align="right"><label for="password">Пароль:</label></td>
                <td><%= Html.Password("password", "", new { style = "width:200px;" })%><%= Html.ValidationMessage("password") %></td>
            </tr>
            <tr>
                <td></td>
                <td><%= Html.CheckBox("rememberMe") %> <label class="inline" for="rememberMe">запомнить?</label></td>
            </tr>
            <tr>
                <td colspan="2" align="center"><input type="submit" value="Вход" /></td>
            </tr>
            </table>
            
        </div>
    <% } %>
</asp:Content>
