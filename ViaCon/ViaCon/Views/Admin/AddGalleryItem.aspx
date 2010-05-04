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
        <fieldset>
            <legend>Fields</legend>
        
            <p>
                <label for="ContentId">ContentId:</label>
                <%= Html.TextBox("ContentId") %>
            </p>
            <p>
                <label for="Title">Title:</label>
                <%= Html.TextBox("Title") %>
            </p>
            <p>
                <label for="Description">Description:</label>
                <%= Html.TextBox("Description") %>
            </p>
            <p>
                <label for="Keywords">Keywords:</label>
                <%= Html.TextBox("Keywords") %>
            </p>
            <p>
                <label for="Text">Text:</label>
                <%= Html.TextBox("Text") %>
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

