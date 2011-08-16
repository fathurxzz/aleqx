<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Exchange.Models.TenderTotalsSummary>>" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>
<table class="tendersSummary">
    <tr>
        <th>
            <%=Html.ResourceString("Currency")%>
        </th>
        <th>
            <%=Html.ResourceString("Buy")%>
        </th>
        <th>
            <%=Html.ResourceString("Sell")%>
        </th>
    </tr>
    <% foreach (var item in Model)
       { %>
    <tr>
        <td class="currency c<%=item.CurId%>">
            <%= Html.Encode(item.CurId)%>
        </td>
        <td class="amount sum">
            <%= Html.Encode(String.Format("{0:N}", item.AmountBuy))%>
        </td>
        <td class="amount sum">
            <%= Html.Encode(String.Format("{0:N}", item.AmountSell))%>
        </td>
    </tr>
    <% } %>
</table>
