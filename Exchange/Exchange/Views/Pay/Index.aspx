<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Exchange.Models.Order>>" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>

    <table class="filterContainer">
        <tr>
        <td>
        <%Html.RenderPartial("CurrencySelector", ViewData["CurrencyList"]);%>
        </td>
        <td>
        <%Html.RenderPartial("OperationSelector", ViewData["OperList"]); %>
        </td>
        </tr>
    </table>

    <table>
        <tr>
            <th>
                <%=Html.ResourceString("Number")%>
            </th>
            <th>
                <%=Html.ResourceString("ExchangeMarket")%>
            </th>
            <th>
                <%=Html.ResourceString("Currency")%>
            </th>
            <th>
                <%=Html.ResourceString("Operation")%>
            </th>
            <th>
                <%=Html.ResourceString("OperSign")%>
            </th>
            <th>
                <%=Html.ResourceString("DocSum")%>
            </th>
            <th>
                <%=Html.ResourceString("Equiv")%>
            </th>
            <th>
                <%=Html.ResourceString("Course")%>
            </th>
            <th>
                <%=Html.ResourceString("DealsCount")%>
            </th>
            <th>
                <%=Html.ResourceString("PaymentsCount")%> (<%=Html.ResourceString("PaymentsPaidCount")%>)
            </th>
            <th>
                <%=Html.ResourceString("Info")%>
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%=Html.ActionLink(item.Number,"Details","Pay",new{id=item.Id},null) %>
            </td>
            <td>
                <%=Html.ResourceString(item.ExchangeMarket.ToString())%>
            </td>

            <td class="currency c<%=item.CurId%>">
                <%= Html.Encode(item.CurId) %>
            </td>
            <td>
                <%= Html.Encode(item.OperName) %>
            </td>
            <td>
                <%=Html.ResourceString(item.OperSign.ToString()) %>
            </td>
            <td class="sum">
                <%= Html.Encode(String.Format("{0:N}", item.DocSum)) %>
            </td>
            <td class="sum">
                <%= Html.Encode(String.Format("{0:N}", item.Equivalent)) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.Course)) %>
            </td>
            <td>
                <%= item.DealsCount %>
            </td>
            <td>
                <%= item.PaymentsCount %>
                (<span class="paimentsPaid"><%= item.PaymentsPaidCount %></span> / <span class="paimentsNotPaid"><%=item.PaymentsCount - item.PaymentsPaidCount%></span>)
            </td>
            <td>
                <%= Html.Encode(item.InfoForPay) %>
            </td>
        </tr>
    
    <% } %>

    </table>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">

</asp:Content>

