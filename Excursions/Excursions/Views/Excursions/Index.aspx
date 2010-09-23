<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Excursions.Models.Excursion>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

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
                Name
            </th>
            <th>
                Text
            </th>
            <th>
                Title
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "EditExcursion", "Admin", new { id = item.Id },null)%> |
                <%= Html.ActionLink("Delete", "DeleteExcursion", "Admin", new { id = item.Id },null)%>
            </td>
            <td>
                <%= Html.Encode(item.Id) %>
            </td>
            <td>
                <%= Html.Encode(item.ImageSource) %>
            </td>
            <td>
                <%= Html.Encode(item.Name) %>
            </td>
            <td>
                <%= Html.Encode(item.Text) %>
            </td>
            <td>
                <%= Html.Encode(item.Title) %>
            </td>
        </tr>
    
    <% } %>

    </table>
    <%
    if (Request.IsAuthenticated)
    {
    %>
    <p>
       <%= Html.ActionLink("Создать", "AddEdit", "Excursions", new { area="Admin"}, null)%>
    </p>
    <%
    } %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="LeftContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="RightContent" runat="server">
</asp:Content>

