<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Exchange.Models.Signer>>" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Html.ResourceString("Officials")%></h2>

    <table>
        <tr>
            <th>
                <%=Html.ResourceString("Name") %>
            </th>
            <th>
                <%=Html.ResourceString("Post") %>
            </th>
            <th class="empty"></th>
        </tr>

    <% foreach (var item in Model) { %>
        <tr>
            <td>
                <%= Html.Encode(item.Name) %>
            </td>
            <td>
                <%= Html.Encode(item.Post) %>
            </td>
            <td class="empty">
                <%= Html.ResourceActionLink("Edit", "Edit", new { id=item.Id }) %> |
                <%= Html.ResourceActionLink("Delete", "Delete", "Signers", new { id = item.Id }, new { onclick = "return confirm('" + Html.ResourceString("AreYouSure") + "?')" })%>
            </td>
        </tr>
    <% } %>

    </table>

    <div class="buttonsContainer">
        <%= Html.ResourceActionLink("Create", "Create", new[] { SiteRoles.MasterAdmin, SiteRoles.AdminFrontOffice, SiteRoles.AdminFrontOfficeBranch })%>
    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>

