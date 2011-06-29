<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Babich.Models.GalleryItem>>" %>

    <table>
        <tr>
            <th></th>
            <th>
                DefaultGalleryImage
            </th>
            <th>
                Id
            </th>
            <th>
                ImageSource
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.Id }) %> |
                <%= Html.ActionLink("Details", "Details", new { id=item.Id })%> |
                <%= Html.ActionLink("Delete", "Delete", new { id=item.Id })%>
            </td>
            <td>
                <%= Html.Encode(item.DefaultGalleryImage) %>
            </td>
            <td>
                <%= Html.Encode(item.Id) %>
            </td>
            <td>
                <%= Html.Encode(item.ImageSource) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>


