<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Babich.Models.LogOnModel>" %>

<asp:Content ID="loginTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Log On
</asp:Content>

<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">
<div id="mainContent">
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
    </div>
</asp:Content>
