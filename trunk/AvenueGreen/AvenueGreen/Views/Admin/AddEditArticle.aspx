<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AvenueGreen.Models.Article>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Добавить новость</h2>
    <% using (Html.BeginForm("AddEditArticle", "Admin", FormMethod.Post, new { id = "AddEditArticle" }))
       {%>
        <%= Html.Hidden("isNew") %>
        <fieldset>
            <legend></legend>
            <p>
                Адрессная строка:<span id="idErrorHolder"></span> <br />
                <%if((bool)ViewData["isNew"]){ %>
                    <%= Html.TextBox("id") %>
                <%} %>
                <%else{ %>
                    <strong><%= ViewData["id"] %></strong>
                <%} %>
            </p>

        <p>
            Заглавие:<span id="titleErrorHolder"></span> <br />
            <%= Html.TextBox("title") %>
        </p>
        <p>
            Дата: <span id="dateErrorHolder"></span> <br />
            <%= Html.TextBox("date") %>
        </p>
        <p>
            Текст: <span id="textErrorHolder"></span> <br />
            <%= Html.TextArea("text") %>
        </p>
        <p>
            Description:<br />
            <%= Html.TextBox("description") %>
        </p>
        <p>
            Keywords:<br />
            <%= Html.TextBox("keywords") %>
        </p>
        <input type="submit" value="Сохранить" />
        </fieldset>

    <% } %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
AvenueGreen - Система администрирования - <%=ViewData["cTitle"] %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
<script type="text/javascript" src="/Scripts/jquery.FCKEditor.js"></script>
 <script type="text/javascript">
        $(function() {
            $.fck.config = { path: '<%= VirtualPathUtility.ToAbsolute("~/Controls/fckeditor/") %>', config: { DefaultLanguage: "ru", AutoDetectLanguage: false, SkinPath: "/Controls/fckeditor/editor/skins/office2003/"} };
            $("#text").fck({ height: 500, width:450 });
        });
</script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentTitle" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="GalleryContent" runat="server">
</asp:Content>

