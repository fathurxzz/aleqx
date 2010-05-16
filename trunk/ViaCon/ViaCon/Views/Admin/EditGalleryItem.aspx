<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ViaCon.Models.Content>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EditGalleryItem
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%
    int? parentId = (int?)ViewData["parentId"];
     %>
    <h2>EditGalleryItem</h2>

   

    <% using (Html.BeginForm("UpdateContent", "Admin"))
       {%>
        <%=Html.Hidden("parentId",parentId)%>
        <%=Html.Hidden("id",Model.Id)%>
        <%=Html.Hidden("isGalleryItem", true)%>
        <%=Html.Hidden("collapsible", false)%>
        <fieldset>
            <legend>Fields</legend>
            <p>
                <label for="ContentId">ContentId:</label>
                <%= Html.TextBox("contentId", Model.ContentId) %>
                <%= Html.ValidationMessage("contentId", "*") %>
            </p>
            <p>
                <label for="Title">Title:</label>
                <%= Html.TextBox("title", Model.Title) %>
                <%= Html.ValidationMessage("title", "*") %>
            </p>
            <p>
                <label for="Description">Description:</label>
                <%= Html.TextBox("description", Model.Description) %>
                <%= Html.ValidationMessage("description", "*") %>
            </p>
            <p>
                <label for="Keywords">Keywords:</label>
                <%= Html.TextBox("keywords", Model.Keywords) %>
                <%= Html.ValidationMessage("keywords", "*") %>
            </p>
            <p>
                <label for="Text">Text:</label>
                <%= Html.TextArea("text", Model.Text)%>
                <%= Html.ValidationMessage("text", "*") %>
            </p>
            <p>
                <input type="submit" value="���������" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("����� � ������", "Gallery")%>
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
