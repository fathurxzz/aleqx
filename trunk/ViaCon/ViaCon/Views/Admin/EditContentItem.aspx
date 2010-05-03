<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ViaCon.Models.Content>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EditContentItem
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%
    int? parentId = (int?)ViewData["parentId"];
    bool? horisontal = (bool?)ViewData["horisontal"];
     %>
    <h2>EditContentItem</h2>


    <% using (Html.BeginForm("UpdateContent","Admin"))
       {%>

        <%=Html.Hidden("parentId",parentId)%>
        <%=Html.Hidden("id",Model.Id)%>
        <%=Html.Hidden("horisontal", horisontal)%>

        <fieldset>
            <legend>Fields</legend>

            <p>
                <label for="ContentId">ContentId:</label>
                <%= Html.TextBox("contentId", Model.ContentId) %>
            </p>
            <p>
                <label for="Title">Title:</label>
                <%= Html.TextBox("title", Model.Title) %>
            </p>            
            <p>
                <label for="Description">Description:</label>
                <%= Html.TextArea("description", Model.Description)%>
            </p>

            <p>
                <label for="Keywords">Keywords:</label>
                <%= Html.TextArea("keywords", Model.Keywords)%>
            </p>
            <p>
                <label for="Text">Text:</label>
                <%= Html.TextArea("text", Model.Text)%>
            </p>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to List", "EditContent") %>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentContainerTitle" runat="server">
</asp:Content>

