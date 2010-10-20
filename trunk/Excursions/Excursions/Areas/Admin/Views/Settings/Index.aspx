<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Settings
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h3>Settings</h3>

    <% using(Html.BeginForm("Update","Settings",FormMethod.Post))
        {
        %>
    <table class="adminEditContentTable">
        <tr>
            <td valign="top" align="right">New comment notification email:</td>
            <td align="left"><%=Html.TextArea("NewCommentNotificationEmail", (string)ViewData["NewCommentNotificationEmail"], 5, 45, null)%> </td>
        </tr>
        <tr>
            <td valign="top" align="right">Feedback notification email:</td>
            <td align="left"><%=Html.TextArea("FeebbackNotificationEmail", (string)ViewData["FeebbackNotificationEmail"], 5, 45, null)%> </td>
        </tr>
        <tr>
            <td valign="top" align="right">Email (in header menu):</td>
            <td align="left"><%=Html.TextBox("Email", ViewData["Email"], new{style="width:300px;"})%> </td>
        </tr>
        <tr>
            <td valign="top" align="right">Text on ContactsPanel:</td>
            <td align="left"><%=Html.TextArea("ContactsPanelText", (string)ViewData["ContactsPanelText"], 5, 45, null)%> </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <input type="submit"  value="Save"/>
            </td>
        </tr>
    </table>
    
    <%
        }%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
    <link href="../../../../Content/Admin.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery.FCKEditor.js"></script>
    <script type="text/javascript">
        $(function () {
            $.fck.config = { path: '<%= VirtualPathUtility.ToAbsolute("~/Controls/fckeditor/") %>', config: { DefaultLanguage: "ru", AutoDetectLanguage: false, SkinPath: "/Controls/fckeditor/editor/skins/office2003/"} };
            $("#ContactsPanelText").fck({ height: 500, width: 500 });
        });
    </script>
</asp:Content>
