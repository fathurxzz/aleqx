<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DjSzk.Models.MusicContent>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Редактировать музыкальный контент
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm("EditMusicContent", "Content", FormMethod.Post, new { enctype = "multipart/form-data" }))
       {%>
    <%= Html.ValidationSummary(true) %>
    <div class="adminEditContentContainer">
        <table class="adminEditContentTable">
            <tr>
                <td>
                    <%= Html.LabelFor(model => model.FileSource) %>
                </td>
                <td>
                    <input type="file" name="logo" />
                </td>
            </tr>
            <tr>
                <td>
                    <%= Html.LabelFor(model => model.Title) %>
                </td>
                <td>
                    <%= Html.TextBoxFor(model => model.Title) %>
                    <%= Html.ValidationMessageFor(model => model.Title) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Html.LabelFor(model => model.SortOrder) %><br />
                    <span style="font-size: 11px;">(каким по очереди будет этот раздел, необходимо ввести
                        только цифру)</span>
                </td>
                <td>
                    <%= Html.TextBoxFor(model => model.SortOrder, new { style = "width:20px;" })%>
                    <%= Html.ValidationMessageFor(model => model.SortOrder) %>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <%= Html.LabelFor(model => model.Description)%>
                    <br />
                    <%= Html.TextAreaFor(model => model.Description, new { @class = "ckeditor" })%>
                    <%= Html.ValidationMessageFor(model => model.Description)%>
                </td>
            </tr>
        </table>
        <div class="buttonsContainer">
            <input type="submit" value="Сохранить" />
           <a href="javascript:history.back();">Отмена</a>
        </div>
    </div>
    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
 <script type="text/javascript" src="/Scripts/MicrosoftAjax.js"></script>
    <script type="text/javascript" src="/Scripts/MicrosoftMvcValidation.js"></script>
    <script type="text/javascript" src="/Controls/ckeditor/ckeditor.js"></script>
    <script type="text/javascript" src="/Controls/ckeditor/adapters/jquery.js"></script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainMenu" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentTitle" runat="server">
Редактировать музыкальный контент
</asp:Content>
