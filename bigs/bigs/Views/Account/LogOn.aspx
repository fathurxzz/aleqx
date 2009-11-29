<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="loginTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Вход в систему администрирования
</asp:Content>

<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    $(function() {
        $("#username").focus();
    });
</script>
    <h2>Вход в систему администрирования</h2>
    <p>
        Пожалуйста введите Ваш логин и пароль.
    </p>
    <%= Html.ValidationSummary("Вход не был выполнен. Пожалуйста введите правильный логин и пароль.") %>

    <% using (Html.BeginForm()) { %>
            <table>
            <tr>
                <td align="right"><label for="username">Username:</label></td>
                <td align="left">
                <%= Html.TextBox("username") %>
                <%= Html.ValidationMessage("username") %>
                </td>
            </tr>
            <tr>
                <td align="right"><label for="password">Password:</label></td>
                <td align="left">
                    <%= Html.Password("password") %>
                    <%= Html.ValidationMessage("password") %>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <input type="submit" value="Log On" />
                </td>
            </tr>
            </table>
    <% } %>
</asp:Content>
