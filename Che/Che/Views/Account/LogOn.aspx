<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Che.Models.LogOnModel>" %>

<asp:Content ID="loginTitle" ContentPlaceHolderID="PageTitle" runat="server">
    Вход в систему администрирования
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Дизайн для чернигова. Вход в систему администрирования
</asp:Content>

<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">
    <% using (Html.BeginForm())
       { %>
    <%= Html.ValidationSummary(true, "Неправильный логин или пароль. Попробуйте ещё раз.") %>
    <div class="logOnForm">
        <table>
            <tr>
                <td>
                    Логин:
                </td>
                <td>
                    <%= Html.TextBoxFor(m => m.UserName,new{style="width:120px;"}) %>
                    <%= Html.ValidationMessageFor(m => m.UserName) %>
                </td>
            </tr>
            <tr>
                <td>
                    Пароль:
                </td>
                <td>
                    <%= Html.PasswordFor(m => m.Password, new { style = "width:120px;" })%>
                    <%= Html.ValidationMessageFor(m => m.Password) %>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    Запомнить меня <%= Html.CheckBoxFor(m => m.RememberMe) %>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <input type="submit" value="Войти" />
                </td>
            </tr>
        </table>
    </div>
    <% } %>
</asp:Content>
