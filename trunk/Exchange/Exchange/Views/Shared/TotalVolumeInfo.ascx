<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Exchange.Models.TotalVolumeInfoEntity>>" %>
<%@ Import Namespace="Exchange.Models" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>
<table class="tendersSummary">
<tr>
    <th colspan="3"><%=Html.ResourceString("TotalSum")%></th>
</tr>
<% foreach (var item in Model.Where(i=>i.CurId==WebSession.SelectedCurrency||WebSession.SelectedCurrency==0 )) { %>
    <tr>
        <td class="currency c<%=item.CurId%>"><%= Html.Encode(item.CurId)%></td>
        <td class="course sum"><%=Html.Encode(String.Format("{0:N8}", item.AverageCourse))%></td>
        <td class="amount sum"><%= Html.Encode(String.Format("{0:N}", item.Amount)) %></td>
    </tr>
<% } %>
</table>
