<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ViaCon.Models.Gallery>>" %>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<%@ Import Namespace="ViaCon.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ManageGalleryPictures
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%
    int id = (int)ViewData["id"];
%>
    <h2>ManageGalleryPictures</h2>

    <table>
        <tr>
            <th>Title</th>
            <th>Image</th>
            <th>Material</th>
            <th>Location</th>
            <th></th>
        </tr>
    <% foreach (var item in Model) 
       { %>
        <tr>
            <td><%= Html.Encode(item.Title) %></td>
            <td><%= Html.Image(GraphicsHelper.GetCachedImage("~/Content/GalleryImages", item.ImageSource, "thumbnail1"))%></td>
            <td><%= Html.Encode(item.Material) %></td>
            <td><%= Html.Encode(item.Location) %></td>
            <td><%= Html.ActionLink("Delete", "DeleteImage", new { contentId = id, id = item.Id }, new { onclick = "return confirm('Вы уверены?')" })%></td>
        </tr>
    <% } %>
    </table>

    
        <%using (Html.BeginForm("AddImageToGallery", "Admin", FormMethod.Post, new { enctype = "multipart/form-data" }))
          { %>
          <%=Html.Hidden("id",id) %>
        <div id="addMore">
            <p>Добавить еще:</p>
            
            <table>
            <tr>
                <td>Title:</td>
                <td><%=Html.TextBox("title") %></td>
            </tr>
            <tr>
                <td>Material:</td>
                <td><%=Html.TextBox("material") %></td>
            </tr>
            <tr>
                <td>Location:</td>
                <td><%=Html.TextBox("location")%></td>
            </tr>
            <tr>
                <td>File:</td>
                <td><input type="file" name="image" />
            </td>
            </tr>
            </table>
            <input type="submit" value="Добавить" />
        </div>
        <%} %>
    
<br /><br />
<%=Html.ActionLink("Назад в галерею","Gallery") %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentContainerTitle" runat="server">
</asp:Content>

