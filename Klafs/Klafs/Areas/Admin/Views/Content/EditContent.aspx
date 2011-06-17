<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Klafs.Models.Content>" %>

<%@ Import Namespace="Dev.Mvc.Ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.EnableClientValidation(); %>
    <h2 class="editContentTitle">
        Редактирование содержимого</h2>
    <% using (Html.BeginForm())
       {%>
    <%=Html.HiddenFor(x=>x.Id) %>
    <%= Html.ValidationSummary(true) %>
    <div class="adminEditContentContainer">
        <table class="adminEditContentTable">
            <tr>
                <td>
                    <%= Html.LabelFor(model => model.Name) %><br />
                    <span style="font-size: 11px;">(только имя, латиницей, без указания расширения)</span>
                </td>
                <td>
                    <%= Html.TextBoxFor(model => model.Name,new{style="width:600px;"} ) %>
                    <%= Html.ValidationMessageFor(model => model.Name) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Html.LabelFor(model => model.MenuTitle) %><br />
                    <span style="font-size: 11px;">(Заголовок Главной превью товарной категории)</span>
                </td>
                <td>
                    <%= Html.TextBoxFor(model => model.MenuTitle, new { style = "width:600px;" })%>
                    <%= Html.ValidationMessageFor(model => model.MenuTitle)%>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Html.LabelFor(model => model.Description)%><br />
                    <span style="font-size: 11px;">(будет отображено под Главной Картинкой создаваемого раздела)</span>
                </td>
                <td>
                    <%= Html.TextBoxFor(model => model.Description, new { style = "width:600px;" })%>
                    <%= Html.ValidationMessageFor(model => model.Description)%>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Html.LabelFor(model => model.PageTitle)%>
                </td>
                <td>
                    <%= Html.TextBoxFor(model => model.PageTitle, new { style = "width:600px;" })%>
                    <%= Html.ValidationMessageFor(model => model.PageTitle)%>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Html.LabelFor(model => model.Title)%>
                </td>
                <td>
                    <%= Html.TextBoxFor(model => model.Title, new { style = "width:600px;" })%>
                    <%= Html.ValidationMessageFor(model => model.Title)%>
                </td>
            </tr>
            <tr>
                <td>
                    <%=Html.LabelFor(model => model.Sign)%>
                </td>
                <td>
                    <%=Html.TextBoxFor(model => model.Sign, new {style = "width:600px;"})%>
                    <%=Html.ValidationMessageFor(model => model.Sign)%>
                </td>
            </tr>
            <tr>
                <td>
                    <%=Html.LabelFor(model => model.Sign2)%>
                </td>
                <td>
                    <%=Html.TextBoxFor(model => model.Sign2, new {style = "width:600px;"})%>
                    <%=Html.ValidationMessageFor(model => model.Sign2)%>
                </td>
            </tr>




            <tr>
                <td>
                    <%= Html.LabelFor(model => model.SortOrder)%><br />
                    <span style="font-size: 11px;">(каким по очереди будет этот раздел, необходимо ввести
                        только цифру)</span>
                </td>
                <td>
                    <%= Html.TextBoxFor(model => model.SortOrder, new { style = "width:20px;" })%>
                    <%= Html.ValidationMessageFor(model => model.SortOrder)%>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Html.LabelFor(model => model.Text)%>
                </td>
                <td>
                    <%= Html.TextAreaFor(model => model.Text)%>
                    <%= Html.ValidationMessageFor(model => model.Text)%>
                </td>
            </tr>
        </table>
    </div>
    <h2 class="editContentTitle">
        Для поисковых систем</h2>
    <div class="adminEditContentContainer">
        <table class="adminEditContentTable">
            <tr>
                <td style="width:500px;">
                    <%= Html.LabelFor(model => model.SeoKeywords)%>
                </td>
                <td>
                    <%= Html.TextBoxFor(model => model.SeoKeywords, new { style = "width:600px;" })%>
                    <%= Html.ValidationMessageFor(model => model.SeoKeywords)%>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Html.LabelFor(model => model.SeoDescription)%>
                </td>
                <td>
                    <%= Html.TextBoxFor(model => model.SeoDescription, new { style = "width:600px;"}) %>
                    <%= Html.ValidationMessageFor(model => model.SeoDescription) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Html.LabelFor(model => model.SeoText)%><br />
                    <span style="font-size: 11px;">(отображается внизу страницы)</span>
                </td>
                <td>
                    <%= Html.TextAreaFor(model => model.SeoText)%>
                    <%= Html.ValidationMessageFor(model => model.SeoText)%>
                </td>
            </tr>
        </table>
    </div>
    <div class="buttonsContainer">
        <input type="submit" value="Сохранить" />&nbsp;
        <%= Html.ActionLink("Назад", "Index","Home",new{area="",id=Model.Name},null) %>
    </div>
    <% } %>
    
        
    
</asp:Content>
<asp:Content ContentPlaceHolderID="PageTitle" runat="server">
    Редактирование содержимого
</asp:Content>
<asp:Content ContentPlaceHolderID="includes" runat="server">
    <script type="text/javascript" src="/Scripts/MicrosoftAjax.js"></script>
    <script type="text/javascript" src="/Scripts/MicrosoftMvcValidation.js"></script>
    <%= Ajax.ScriptInclude("/Controls/ckeditor/ckeditor.js")%>
    <%= Ajax.ScriptInclude("/Controls/ckeditor/adapters/jquery.js")%>
    <script type="text/javascript">
        $(function () {
            CKEDITOR.replace("Text", { toolbar: "Media" });
            CKEDITOR.replace("SeoText", { toolbar: "Media" });
        })
    </script>
</asp:Content>
