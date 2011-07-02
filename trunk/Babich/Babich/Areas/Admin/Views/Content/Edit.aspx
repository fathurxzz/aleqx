<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Babich.Models.Content>" %>
<%@ Import Namespace="Dev.Mvc.Ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="mainContent">
    <% Html.EnableClientValidation(); %>
    <h2 class="editContentTitle">
        Редактирование содержимого</h2>
    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>

    <%=Html.HiddenFor(x=>x.Id) %>
        

        <div class="adminEditContentContainer">

        <table class="adminEditContentTable">
        <tr>
            <td style="width:200px;">
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
                <%= Html.LabelFor(model => model.PageTitle)%>
            </td>
            <td>
                UKR:<%= Html.TextBoxFor(model => model.PageTitle, new { style = "width:600px;" })%>
                <%= Html.ValidationMessageFor(model => model.PageTitle)%><br />
                ENG:<%= Html.TextBoxFor(model => model.PageTitleEng, new { style = "width:600px;" })%>
                <%= Html.ValidationMessageFor(model => model.PageTitleEng)%>
            </td>
        </tr>
         <tr>
            <td>
            <%= Html.LabelFor(model => model.Title)%>
            </td>
            <td>
             UKR:<%= Html.TextBoxFor(model => model.Title, new { style = "width:600px;" })%>
                    <%= Html.ValidationMessageFor(model => model.Title)%><br />
             ENG:<%= Html.TextBoxFor(model => model.TitleEng, new { style = "width:600px;" })%>
                    <%= Html.ValidationMessageFor(model => model.TitleEng)%>

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
                <div id="editTextSwitcher">
                    <a href="#" id="showUkr">UKR</a> <a href="#" id="showEng">ENG</a>
                </div>
                <div id="editTextUkr">
                    UKR:<%= Html.TextAreaFor(model => model.Text)%>
                </div>
                <div id="editTextEng" style="display:none;">
                    ENG:<%= Html.TextAreaFor(model => model.TextEng)%>
                </div>
            </td>
        </tr>
       </table>
       </div>
        <h2 class="editContentTitle">
        Для поисковых систем</h2>
        
    <div class="adminEditContentContainer">
        <table class="adminEditContentTable">
            <tr>
                <td style="width:200px;">
                    <%= Html.LabelFor(model => model.SeoKeywords)%>
                </td>
                <td>
                    UKR:<%= Html.TextBoxFor(model => model.SeoKeywords, new { style = "width:600px;" })%><br />
                    ENG:<%= Html.TextBoxFor(model => model.SeoKeywords, new { style = "width:600px;" })%>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Html.LabelFor(model => model.SeoDescription)%>
                </td>
                <td>
                    UKR:<%= Html.TextBoxFor(model => model.SeoDescription, new { style = "width:600px;"}) %><br />
                    ENG:<%= Html.TextBoxFor(model => model.SeoDescription, new { style = "width:600px;"}) %>
                </td>
            </tr>
        </table>
    </div>
    <div class="buttonsContainer">
        <input type="submit" value="Сохранить" />&nbsp;
        <%= Html.ActionLink("Назад", "Index","Home",new{area="",id=Model.Name},null) %>
    </div>
    <% } %>
        

    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="SubMenuContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentTitleContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="GalleryContent" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="includes" runat="server">
    <script type="text/javascript" src="/Scripts/MicrosoftAjax.js"></script>
    <script type="text/javascript" src="/Scripts/MicrosoftMvcValidation.js"></script>
    <%= Ajax.ScriptInclude("/Controls/ckeditor/ckeditor.js")%>
    <%= Ajax.ScriptInclude("/Controls/ckeditor/adapters/jquery.js")%>
    <script type="text/javascript">
        $(function () {
            CKEDITOR.replace("Text", { toolbar: "Media" });
            CKEDITOR.replace("TextEng", { toolbar: "Media" });


            $("#showUkr").click(function () {
                $("#editTextUkr").css("display", "block");
                $("#editTextEng").css("display", "none");
            });

            $("#showEng").click(function () {
                $("#editTextEng").css("display", "block");
                $("#editTextUkr").css("display", "none");
            });

        })



    </script>
</asp:Content>

