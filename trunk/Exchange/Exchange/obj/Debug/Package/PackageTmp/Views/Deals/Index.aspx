<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Exchange.Models.DealPresentation>>" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=Html.ResourceString("CompletedOperations") %></h2>
    <table>
        <tr>
            <th>
                <%=Html.ResourceString("Office")%>
            </th>
            <th>
                <%=Html.ResourceString("OrderNumber")%>
            </th>
            <th>
                <%=Html.ResourceString("OrderDate")%>
            </th>
            <th>
                <%=Html.ResourceString("OrderSendDate")%>
            </th>
            <th>
                <%=Html.ResourceString("TenderNumber")%>
            </th>
            <th>
                <%=Html.ResourceString("Operation")%>
            </th>
            <th>
                <%=Html.ResourceString("Currency")%>
            </th>
            <th>
                <%=Html.ResourceString("ExchangeMarket")%>
            </th>
            <th>
                <%=Html.ResourceString("Course")%>
            </th>
            <th>
                <%=Html.ResourceString("CourseClient")%>
            </th>
            <th>
                <%=Html.ResourceString("DocSum")%>
            </th>
            <th>
                <%=Html.ResourceString("Equiv")%>
            </th>
            <th>
                <%=Html.ResourceString("Client")%>
            </th>
            <th>
                <%=Html.ResourceString("Info")%>
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <%= Html.Encode(item.OfficeName) %>
            </td>
            <td>
                <%= Html.Encode(item.OrderNumber) %>
            </td>
            <td>
                <%= Html.Encode(item.OrderDate.ToShortDateString()) %>
            </td>
            <td>
                <%= Html.Encode(item.OrderSendDate.ToString())%>
            </td>
            <td>
                <%= Html.Encode(item.TenderId)%>
            </td>
            <td>
                <%= Html.Encode(item.OperName) %>
            </td>
            <td class="currency c<%=item.CurId %>">
                <%= Html.Encode(item.CurId) %>
            </td>
            <td>
                <%= Html.ResourceString(item.ExCode.ToString())%>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.Course)) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.CourseClient)) %>
            </td>
            <td class="sum">
                <%= item.DocSum.ToString("N") %>
            </td>
            <td class="sum">
                <%= item.Equivalent.ToString("N") %>
            </td>
            <td>
                <%= Html.Encode(item.ClientName) %>
            </td>
            <td>
                <%= Html.Encode(item.InfoForBranch) %>
            </td>
        </tr>
        <% } %>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>
