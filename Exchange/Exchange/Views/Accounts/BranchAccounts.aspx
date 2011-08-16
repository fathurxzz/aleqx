<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Exchange.Models.Account>>" %>
<%@ Import Namespace="Exchange.Models" %>
<%@ Import Namespace="Exchange.Models.Enum" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2> <%=Html.ResourceString("Accounts") %></h2>

    <table>
        <tr>
            <th>
                <%=Html.ResourceString("Operation") %>
            </th>
            <th>
                <%=Html.ResourceString("Currency") %>
            </th>
            <th>
                <%=Html.ResourceString("Nls") %>
            </th>
            <th>
                <%=Html.ResourceString("OperSign") %>
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%=Html.ResourceString(((EOperation)item.OperId).ToString()) %>
            </td>
            <td>
                <%= Html.Encode(item.CurId) %>
            </td>
            <td>
                <%= Html.Encode(item.Nls) %>
            </td>
            <td>
                <%= Html.ResourceString(((EOperSign)item.OperSign).ToString())%>
            </td>
        </tr>
    
    <% } %>

    </table>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
<%=Html.ResourceString("Accounts") %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>

