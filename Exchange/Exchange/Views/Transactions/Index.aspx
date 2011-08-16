<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Exchange.Models.Transaction>>" %>

<%@ Import Namespace="Exchange.Models.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=Html.ResourceString("Transactions") %></h2>
    <div class="filterContainer">
        <div>
            <%Html.RenderPartial("DateSelector"); %>
            <%Html.RenderPartial("CurrencySelector", ViewData["CurrencyList"]);%>
            <%Html.RenderPartial("StatusSelector", ViewData["StatusList"]);%>
            <%Html.RenderPartial("MfoSelector"); %>
            <%=Html.ResourceActionLink("Matching", "Index", "Transactions", new { matching = true },null)%>
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="clear">
    </div>
    <table>
        <tr>
            <th>
                <%=Html.ResourceString("Number")%>
            </th>
            <th>
                <%=Html.ResourceString("Date")%>
            </th>
            <th>
                <%=Html.ResourceString("Currency")%>
            </th>
            <th>
                <%=Html.ResourceString("DocSum")%>
            </th>
            <th>
                <%=Html.ResourceString("Podrid")%>
            </th>
            <th>
                <%=Html.ResourceString("PaymentDetail")%>
            </th>
            <th>
                <%=Html.ResourceString("KvSum")%>
            </th>
            <th>
                <%=Html.ResourceString("KvDate")%>
            </th>
            <th>
                <%=Html.ResourceString("Tender")%>
            </th>
            <th>
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <%= item.TrId %>
            </td>
            <td>
                <%= item.TrDate.ToShortDateString() %>
            </td>
            <td class="currency c<%=item.CurId%>">
                <%= Html.Encode(item.CurId) %>
            </td>
            <td class="sum">
                <%= item.TrSum.ToString("N") %>
            </td>
            <td>
                <%= item.Podrid %>
            </td>
            <td>
                <%= Html.Encode(item.Detail) %>
            </td>
            <td class="sum">
                <%= item.KvSum.ToString("N") %>
            </td>
            <td>
                <%= item.KvDate %>
            </td>
            <td>
                <%if (item.TenderId.HasValue)
                  {%>
                <%=Html.ActionLink(item.TenderId.ToString(), "Edit", "ExchangeTenders", new {id = item.TenderId},null)%>
                <%}%>
            </td>
            <td class="nowrap">
                <%= Html.ResourceActionLink("Edit", "Edit", new { id=item.Id }) %>
                |
                <%= Html.ResourceActionLink("Delete", "Delete", new { id = item.Id }, new { onclick = "return confirm('" + Html.ResourceString("AreYouSureToDeleteSelectedItems") + "?')" })%>
            </td>
        </tr>
        <% } %>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
    <%=Html.RegisterCss("Tenders.css") %>
</asp:Content>
