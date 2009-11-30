<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/ContentAdmin.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EditText
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% using(Html.BeginForm()){ %>  
        <%= Html.Hidden("controllerName") %>
        <%= Html.Hidden("contentUrl") %>
        
        <table>
        <tr>
            <td valign="middle" align="right">Title:</td>
            <td align="left"><%= Html.TextArea("editTitle", new { cols="50", rows="2" })%></td>
        </tr>
        <tr>
            <td valign="middle" align="right">Keywords:</td>
            <td align="left"><%= Html.TextArea("keywords", new { cols = "50", rows = "5" })%></td>
        </tr>
        <tr>
            <td valign="middle" align="right">Description:</td>
            <td align="left"><%= Html.TextArea("description", new { cols = "50", rows = "5" })%></td>
        </tr>
        <tr>
            <td colspan="2"><%= Html.TextArea("text")%></td>
        </tr>
        </table>
        <input type="submit" value="Сохранить" />
    <%} %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentTitle" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Includes" runat="server">
    <script type="text/javascript" src="/Scripts/jquery.FCKEditor.js"></script>
    <script type="text/javascript">
        $(function() {
            $.fck.config = { path: '<%= VirtualPathUtility.ToAbsolute("~/Controls/fckeditor/") %>', config: { DefaultLanguage: "ru", AutoDetectLanguage: false, SkinPath: "/Controls/fckeditor/editor/skins/office2003/"} };
            $("#text").fck({ height: 500, width:600 });
        });
    </script>
</asp:Content>
