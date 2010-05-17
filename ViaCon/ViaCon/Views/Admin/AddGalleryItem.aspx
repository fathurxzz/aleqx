<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ViaCon.Models.Content>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	AddGalleryItem
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%
    int? parentId = (int?)ViewData["parentId"];
     %>
    <h2>AddGalleryItem</h2>

    <% using (Html.BeginForm("UpdateContent","Admin")) {%>
        <%=Html.Hidden("parentId",parentId)%>
        <%=Html.Hidden("id",int.MinValue)%>
        <%=Html.Hidden("isGalleryItem", true)%>
        <%=Html.Hidden("collapsible", false)%>
        <fieldset>
            <legend></legend>
        
            <p>
                <label for="ContentId">Идентификатор:</label>
                <%= Html.TextBox("contentId") %>
            </p>
            <p>
                <label for="Title">Заголовок (он же пункт меню):</label>
                <%= Html.TextBox("title") %>
            </p>
            <p>
                <label for="Text">Текст:</label>
                <%= Html.TextArea("text")%>
            </p>
            <p>
                <label for="Keywords">Keywords:</label>
                <%= Html.TextBox("keywords") %>
            </p>
            <p>
                <label for="Description">Description:</label>
                <%= Html.TextArea("description")%>
            </p>
            <p>
                <input type="submit" value="Создать" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Назад к списку", "Gallery")%>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentContainerTitle" runat="server">
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
