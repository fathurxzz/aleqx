<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AvenueGreen.Models.Content>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%
    var parentId = (int?)ViewData["parentId"];
    var horisontal = (bool?)ViewData["horisontal"];
    var isGalleryItem = (bool) ViewData["isGalleryItem"];
     %>
     <h2>Редактировать контент</h2>


   <% using (Html.BeginForm("UpdateContent","Admin"))
       {%>

        <%=Html.Hidden("parentId",parentId)%>
        <%=Html.Hidden("id",Model.Id)%>
        <%=Html.Hidden("horisontal", horisontal)%>
        <fieldset>
            <legend></legend>
            <p>
                <label for="ContentId">Идентификатор (вводить латиницей):</label>
                <br />
                <%= Html.TextBox("contentId", Model.ContentId,new{style="width:90%"}) %>
            </p>
            <p>
                <label for="Title">Заголовок (он же пункт меню):</label>
                <br />
                <%= Html.TextBox("title", Model.Title, new { style = "width:90%" })%>
            </p>            
            <p>
                <label for="Text">Текст:</label>
                <br />
                <%= Html.TextArea("text", Model.Text)%>
            </p>
            <p>
                <label for="Keywords">Keywords:</label>
                <br />
                <%= Html.TextBox("keywords", Model.Keywords, new { style = "width:90%" })%>
            </p>
            <p>
                <label for="Description">Description:</label>
                <br />
                <%= Html.TextArea("description", Model.Description,5,70,null)%>
            </p>
            <p>
                <label for="SortOrder">Порядок отбражения:</label>
                <br />
                <%= Html.TextBox("sortOrder", Model.SortOrder,new{style="width:90%"}) %>
            </p>
            <p>
                <label for="IsGalleryItem">Галерея:</label>
                <%= Html.CheckBox("isGalleryItem", isGalleryItem)%>
            </p>
            <p>
                <input type="submit" value="Сохранить" />
            </p>
        </fieldset>

    <% } %>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
AvenueGreen - Система администрирования - Редактировать контент
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
<script type="text/javascript" src="/Scripts/jquery.FCKEditor.js"></script>
    <script type="text/javascript">
        /*$(function() {
            $.fck.config = { path: '<%= VirtualPathUtility.ToAbsolute("~/Controls/fckeditor/") %>', config: { DefaultLanguage: "ru", AutoDetectLanguage: false, SkinPath: "/Controls/fckeditor/editor/skins/office2003/"} };
            $("#text").fck({ height: 500, width:600 });
        });*/
    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentTitle" runat="server">
</asp:Content>

