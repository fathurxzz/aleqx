<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="loginTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Log On
</asp:Content>

<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    $(function() {
        $("#username").focus();
    });
</script>
    <h2>Log On</h2>
    <p>
        Please enter your username and password.
    </p>
    <%= Html.ValidationSummary("Login was unsuccessful. Please correct the errors and try again.") %>

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
