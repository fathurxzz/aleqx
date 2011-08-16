<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Exchange.Models.Office>>" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Html.ResourceString("Offices") %></h2>

    <table>
        <tr>
            <th>
                <%=Html.ResourceString("OfficeId")%>
            </th>
            <th>
                <%=Html.ResourceString("Podrid")%>
            </th>
            <th>
                <%=Html.ResourceString("OfficeName")%>
            </th>
            <th>
                <%=Html.ResourceString("OfficeLevel")%>
            </th>
            <th>
                <%=Html.ResourceString("DateClose")%>
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr class="officeLevel<%=item.OfficeLevel%>">
            <td>
                <%= Html.Encode(item.OfficeId) %>
            </td>
            <td>
                <%= Html.Encode(item.Podrid) %>
            </td>
            <td>
                <%= Html.Encode(item.OfficeName) %>
            </td>
            <td>
                <%= Html.Encode(item.OfficeLevel) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.DateClose)) %>
            </td>
        </tr>
    
    <% } %>

    </table>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
<%=Html.ResourceString("Offices") %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>

