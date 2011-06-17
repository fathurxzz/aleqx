<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Klafs.Models.LogOnModel>" %>

<asp:Content ID="loginTitle" ContentPlaceHolderID="PageTitle" runat="server">
    Вход в систему администрирования
</asp:Content>

<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Вход в систему администрирования</h2>
    

    <% using (Html.BeginForm()) { %>
        <%= Html.ValidationSummary(true, "Неправильный логин или пароль. Попробуйте ещё раз.") %>
        
        
        
        <table>
        <tr>
            <td>Логин:</td>
            <td><%= Html.TextBoxFor(m => m.UserName) %>
                    <%= Html.ValidationMessageFor(m => m.UserName) %></td>
        </tr>
        <tr>
            <td>Пароль:</td>
            <td> <%= Html.PasswordFor(m => m.Password) %>
                    <%= Html.ValidationMessageFor(m => m.Password) %></td>
        </tr>
        <tr>
            <td colspan="2" align="center">
            Запомнить меня <%= Html.CheckBoxFor(m => m.RememberMe) %><br />
            <input type="submit" value="Войти" /></td>
            
        </tr>
        </table>
    <% } %>
</asp:Content>
