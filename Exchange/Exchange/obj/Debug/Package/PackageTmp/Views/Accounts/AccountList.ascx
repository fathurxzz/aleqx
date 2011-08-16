<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Exchange.Models.Account>>" %>
<%@ Import Namespace="Exchange.Models" %>
<%@ Import Namespace="Exchange.Models.Enum" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>

<table>
    <tr>
        <th>
            <%=Html.ResourceString("Operation") %>
        </th>
        <th>
            <%=Html.ResourceString("Mfo") %>
        </th>
        <th>
            <%=Html.ResourceString("Office") %>
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
        <th></th>
    </tr>

<% foreach (var item in Model) { %>
    <tr>
        <td>
            <%=Html.ResourceString(((EOperation)item.OperId).ToString()) %>
        </td>
        <td>
            <%= Html.Encode(item.Podrid) %>
        </td>
        <td>
            <%= Html.Encode(item.OfficeName) %>
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
        <td>
            <%= Html.ResourceActionLink("Edit", "Edit", new { id=item.Id  }) %> |
            <%= Html.ResourceActionLink("Delete", "Delete", "Accounts", new { id = item.Id }, new { onclick = "return confirm('" + Html.ResourceString("AreYouSure") + "?')" })%>
        </td>
    </tr>
<% } %>
</table>

  


