<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Exchange.Models.Office>>" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>ViewModeratorsOffices</h2>
    <table>
        <tr>
            <th>
                Podrid
            </th>
            <th>
                OfficeName
            </th>
        </tr>

    <% foreach (var item in Model) { %>
        <tr>
            <td>
                <%= Html.Encode(item.Podrid) %>
            </td>
            <td>
                <%= Html.Encode(item.OfficeName) %>
            </td>
        </tr>
    <% } %>
    </table>

        <div class="buttonsContainer">
        <span class="UIButton">
            <%= Html.ResourceActionLink("Back", "Moderators") %>
        </span>
    </div>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>

