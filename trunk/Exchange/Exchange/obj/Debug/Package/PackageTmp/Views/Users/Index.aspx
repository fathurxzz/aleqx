<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Exchange.Models.UserPresentation>>" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Html.ResourceString("Users") %></h2>

    <table class="filterContainer">
    <tr>
    <td>
    <%Html.RenderPartial("MfoSelector");%>
    </td>
    <td>
    <%Html.RenderPartial("UserNameSelector");%>
    </td>
    </tr>
    </table>

    <table>
        <tr>
            <th>
                <%=Html.ResourceString("Mfo")%>
            </th>
            <th>
                <%=Html.ResourceString("TabNum")%>
            </th>
            <th>
                <%=Html.ResourceString("UserName")%>
            </th>
            <th>
                <%=Html.ResourceString("Office")%>
            </th>
            <th>
                <%=Html.ResourceString("IsAutorized")%>
            </th>
            <th></th>
        </tr>

    <% foreach (var item in Model.OrderBy(u=>u.Podrid)) { %>
    
        <tr>
            <td>
                <%= Html.Encode(item.Podrid) %>
            </td>
            <td>
                <%= Html.Encode(item.TabNum) %>
            </td>
            <td>
                <%= Html.Encode(item.Name) %>
            </td>
            <td>
                <%= Html.Encode(item.OfficeName) %>
            </td>
            <td>
                <%= Html.Encode(Helper.GetResourceString(item.IsAutorized.ToString())) %>
            </td>
            <td>
                <%= Html.ResourceActionLink("EditPermissions", "EditPermissions", new { id = item.Id })%> |
                <%= Html.ResourceActionLink("Details", "Details", new { id = item.Id })%>
            </td>

        </tr>
    
    <% } %>
    </table>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
<%=Html.ResourceString("Users") %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>

