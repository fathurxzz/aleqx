<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<AvenueGreen.Models.Widgets>>" %>
<%@ Import Namespace="AvenueGreen.Helpers"%>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


<% var widgetType = (int)ViewData["type"]; %>
    <h2>Index</h2>

    <table>
        <tr>
            <th></th>
            <th>
                Id
            </th>
            <th>
                ImageSource
            </th>
            <th>
                Title
            </th>
            <th>
                Type
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.Id }) %> |
                <%= Html.ActionLink("Details", "Details", new { id=item.Id })%>
            </td>
            <td>
                <%= Html.Encode(item.Id) %>
            </td>
            <td>
                <%= Html.Image(GraphicsHelper.GetCachedImage("~/Content/WidgetImages", item.ImageSource, "thumbnail1"))%>
            </td>
            <td>
                <%= Html.Encode(item.Title) %>
            </td>
            <td>
                <%= Html.Encode(item.Type) %>
            </td>
        </tr>
    
    <% } %>

 </table>

    <%
        if (Request.IsAuthenticated)
        {
            using (Html.BeginForm("AddWidgetItem", "Widget", FormMethod.Post, new { enctype = "multipart/form-data" }))
            {
                
                 %>
    <%=Html.Hidden("widgetType", widgetType)%>
    <div id="addMore">
        <p>
            Добавить еще:</p>
        <table>
            <tr>
                <td>
                    Title:
                </td>
                <td>
                    <%=Html.TextBox("title")%>
                </td>
            </tr>
            <tr>
                <td>
                    Файл:
                </td>
                <td>
                    <input type="file" name="image" />
                </td>
            </tr>
        </table>
        <input type="submit" value="Добавить" />
    </div>
    <%}
        }%>


   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentTitle" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="GalleryContent" runat="server">
</asp:Content>

