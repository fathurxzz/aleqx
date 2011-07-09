<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Che.Models.Content>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<% Html.EnableClientValidation(); %>

    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>
        <%=Html.HiddenFor(x=>x.Id) %>
    <%=Html.HiddenFor(x=>x.ContentType) %>
    <%=Html.HiddenFor(x=>x.ContentLevel) %>


    <%if (Model.ContentLevel == 0)
{%>
   <script type="text/javascript">
       $(function () {
           $(".content0").css("display", "none");
       });
</script>

    <%
}%>

    <%if (Model.ContentType == 1||Model.ContentType == 2||Model.ContentType == 3)
{%>
   <script type="text/javascript">
       $(function () {
           $(".content1").css("display", "none");
       });
</script>

    <%
}%>

       <div class="adminEditContentContainer">
        <table class="adminEditContentTable">
            <tr class="content0">
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
            <tr class="content0">
                <td>
                    Фото
                </td>
                <td>
                    <input type="file" name="logo" />
                </td>
            </tr>
            <tr class="content0">
                <td>
                    <%= Html.LabelFor(model => model.Description) %>
                </td>
                <td>
                    <%= Html.TextAreaFor(model => model.Description, 3, 40, null)%>
                    <%= Html.ValidationMessageFor(model => model.Description) %>
                </td>
            </tr>
            
            <tr class="content0">
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
            <tr class="content1">
                <td colspan="2">
                    <%= Html.LabelFor(model => model.Text) %>
                    <br/>
                
                    <%= Html.TextAreaFor(model => model.Text, new { @class = "ckeditor" })%>
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
Система администрирования - Редактирование содержимого
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="server">
Редактирование содержимого
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="includes" runat="server">
<script type="text/javascript" src="/Scripts/MicrosoftAjax.js"></script>
    <script type="text/javascript" src="/Scripts/MicrosoftMvcValidation.js"></script>
    <script type="text/javascript" src="/Controls/ckeditor/ckeditor.js"></script>
    <script type="text/javascript" src="/Controls/ckeditor/adapters/jquery.js"></script>
    <script type="text/javascript">
        $(function () {
            //CKEDITOR.replace("Text", { toolbar: "Media" });
        })
    </script>
<style type="text/css">
#mainMiddle
{
    padding: 0 !important;
    width: 618px !important;
}
</style>

</asp:Content>