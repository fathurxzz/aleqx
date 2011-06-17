<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Klafs.Models.LogOnModel>" %>

<asp:Content ID="loginTitle" ContentPlaceHolderID="PageTitle" runat="server">
    Админка
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
<asp:Content ContentPlaceHolderID="HeaderTitleContent" runat="server">
    Админка
</asp:Content>
