<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/Admin.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<eShop.Models.CategoryProperties>>" %>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	CategoryPropertiesList
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>CategoryPropertiesList</h2>
    <% Html.RenderAction<eShop.Controllers.PagePartsController>(ppc => ppc.CategoriesList()); %>
    <table>
        <tr>
            <th></th>
            <th>
                Id
            </th>
            <th>
                Name
            </th>
        </tr>
<%if (Model != null)
  { %>
    <% foreach (var item in Model)
       { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id = item.Id })%> |
                <%= Html.ActionLink("Details", "Details", new { id = item.Id })%>
            </td>
            <td>
                <%= Html.Encode(item.Id)%>
            </td>
            <td>
                <%= Html.Encode(item.Name)%>
            </td>
        </tr>
    
    <% }
  }%>

    </table>

    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>
    
    
</asp:Content>

