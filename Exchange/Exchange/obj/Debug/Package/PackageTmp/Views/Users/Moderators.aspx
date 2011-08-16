<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Exchange.Models.Moderator>>" %>

<%@ Import Namespace="Exchange.Models.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=Html.ResourceString("Moderators")%></h2>
    <table>
        <tr>
            <th>
                <%=Html.ResourceString("Name")%>
            </th>
            <th>
                <%=Html.ResourceString("Podrid")%>
            </th>
            <th>
                <%=Html.ResourceString("Phone")%>
            </th>
            <th>
                <%=Html.ResourceString("Office")%>
            </th>
            <th>
                <%=Html.ResourceString("ModeratorOfficeCount")%>
            </th>
            <th class="empty">
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <%= Html.Encode(item.Name) %>
            </td>
            <td>
                <%= Html.Encode(item.Podrid) %>
            </td>
            <td>
                <%= Html.Encode(item.Phone) %>
            </td>
            <td>
                <%= Html.Encode(item.OfficeName) %>
                <%if (item.OfficeId != item.ParentOfficeId)
                  {%>
                (<%= Html.Encode(item.ParentOfficeName) %>)
                <%
                    }%>
            </td>
            <td>
                <%= Html.Encode(item.ModeratorOfficeCount) %>
            </td>
            <td class="empty">
                <%= Html.ResourceActionLink("EditOffices", "EditModeratorsOffices", new {  id=item.Id  })%>
                |
                <%= Html.ResourceActionLink("ViewOffices", "ViewModeratorsOffices", new { id = item.Id })%>
                |
                <%= Html.ResourceActionLink("ClearOfficeList", "DeleteModeratorsOffices", "Users", new { id = item.Id }, new { onclick = "return confirm('" + Html.ResourceString("AreYouSure") + "?')" })%>
            </td>
        </tr>
        <% } %>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>
