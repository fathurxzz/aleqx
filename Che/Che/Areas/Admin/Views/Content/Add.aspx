<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Che.Models.Content>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm("Add", "Content", FormMethod.Post, new { enctype = "multipart/form-data" }))
       {%>



    <%= Html.ValidationSummary(true) %>

    <%=Html.HiddenFor(x=>x.Id) %>
    <%=Html.HiddenFor(x=>x.ContentType) %>
    <%=Html.HiddenFor(x=>x.ContentLevel) %>


    <div class="adminEditContentContainer">
        <table class="adminEditContentTable">
            <tr>
                <td>
                    <%= Html.LabelFor(model => model.Name) %><br />
                    <span style="font-size: 11px;">(только имя, латиницей, без указания расширения)</span>
                </td>
                <td>
                    <%= Html.TextBoxFor(model => model.Name) %>
                    <%= Html.ValidationMessageFor(model => model.Name) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Html.LabelFor(model => model.PageTitle) %>
                </td>
                <td>
                    <%= Html.TextBoxFor(model => model.PageTitle) %>
                    <%= Html.ValidationMessageFor(model => model.PageTitle) %>
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
                    Фото
                </td>
                <td>
                    <input type="file" name="logo" />
                </td>
            </tr>
            <tr>
                <td>
                    <%= Html.LabelFor(model => model.Description) %>
                </td>
                <td>
                    <%= Html.TextAreaFor(model => model.Description, 3, 40, null)%>
                    <%= Html.ValidationMessageFor(model => model.Description) %>
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
            <tr style="display: none;">
                <td>
                    <%= Html.LabelFor(model => model.Text) %>
                </td>
                <td>
                    <%= Html.TextBoxFor(model => model.Text) %>
                    <%= Html.ValidationMessageFor(model => model.Text) %>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    Для поисковых систем
                </td>
            </tr>
            <tr>
                <td>
                    <%= Html.LabelFor(model => model.SeoDescription) %>
                </td>
                <td>
                    <%= Html.TextAreaFor(model => model.SeoDescription, 3, 40, null)%>
                    <%= Html.ValidationMessageFor(model => model.SeoDescription) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Html.LabelFor(model => model.SeoKeywords) %>
                </td>
                <td>
                    <%= Html.TextAreaFor(model => model.SeoKeywords,3,40,null) %>
                    <%= Html.ValidationMessageFor(model => model.SeoKeywords) %>
                </td>
            </tr>
        </table>
        <div class="buttonsContainer">
            <input type="submit" value="Сохранить" />
            <a href="#" onclick="javascript:history.back();">Отмена</a>
        </div>
    </div>
    <% } %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    Система администрирования - Добавление содержимого
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="server">
    Добавление содержимого
</asp:Content>


<asp:Content ID="Content6" ContentPlaceHolderID="includes" runat="server">
    <script type="text/javascript" src="/Scripts/MicrosoftAjax.js"></script>
    <script type="text/javascript" src="/Scripts/MicrosoftMvcValidation.js"></script>
    <style type="text/css">
#mainMiddle
{
    padding: 0 !important;
    width: 618px !important;
}
</style>
</asp:Content>
