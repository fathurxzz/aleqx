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
        <fieldset>
            <legend>Fields</legend>
            <p>
                <label for="ContentId">ContentId:</label>
                <%= Html.TextBox("ContentId", Model.ContentId) %>
                <%= Html.ValidationMessage("ContentId", "*") %>
            </p>
            <p>
                <label for="Title">Title:</label>
                <%= Html.TextBox("Title", Model.Title) %>
                <%= Html.ValidationMessage("Title", "*") %>
            </p>
            <p>
                <label for="Description">Description:</label>
                <%= Html.TextBox("Description", Model.Description) %>
                <%= Html.ValidationMessage("Description", "*") %>
            </p>
            <p>
                <label for="Keywords">Keywords:</label>
                <%= Html.TextBox("Keywords", Model.Keywords) %>
                <%= Html.ValidationMessage("Keywords", "*") %>
            </p>
            <p>
                <label for="Text">Text:</label>
                <%= Html.TextBox("Text", Model.Text) %>
                <%= Html.ValidationMessage("Text", "*") %>
            </p>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to List", "Gallery") %>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentContainerTitle" runat="server">
</asp:Content>

