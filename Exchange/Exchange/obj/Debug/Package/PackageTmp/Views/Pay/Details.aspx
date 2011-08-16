<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Exchange.Models.Payment>>" %>

<%@ Import Namespace="Exchange.Models.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Details</h2>
    <% using (Html.BeginForm("CreateOs", "Pay"))
       {
    %>
    <%=Html.Hidden("orderId") %>
    <table>
        <tr>
            <th>
                <%=Html.CheckBox("checkAll") %>
            </th>
            <th>
                Id
            </th>
            <th>
                Podrid
            </th>
            <th>
                OfficeName
            </th>
            <th>
                OrderId
            </th>
            <th>
                CreateDate
            </th>
            <th>
                Paid
            </th>
            <th>
                PayDate
            </th>
            <th>
                <%=Html.ResourceString("PaymentCurId")%>
            </th>
            <th>
                DocSum
            </th>
            <th>
                <%=Html.ResourceString("DealCurId")%>
            </th>
            <th>
                OsFileName
            </th>
            <th>
                UserId
            </th>
            <th>
                Course
            </th>
            <th>
                OfficeId
            </th>
            <th>
                Equivalent
            </th>
            <th>
                Nls
            </th>
            <th>
                Nlsk
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <%if (!item.Paid && !string.IsNullOrEmpty(item.DocNls) && !string.IsNullOrEmpty(item.DocNlsK)){%>
                <%=Html.CheckBox("cb_" + item.Id, new {@class = "item"})%>
                <% } %>
            </td>
            <td>
                <%= Html.Encode(item.Id) %>
            </td>
            <td>
                <%=item.Podrid %>
            </td>
            <td>
                <%=item.OfficeName %>
            </td>
            <td>
                <%= Html.Encode(item.OrderId) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.CreateDate)) %>
            </td>
            <td>
                <%= Html.Encode(item.Paid) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.PayDate)) %>
            </td>
            <td>
                <%= Html.Encode(item.CurId) %>
            </td>
            <td class="sum">
                <%= Html.Encode(String.Format("{0:N}", item.DocSum)) %>
            </td>
            <td>
                <%= Html.Encode(item.DealCurId) %>
            </td>
            <td>
                <%= Html.Encode(item.OsFileName) %>
            </td>
            <td>
                <%= Html.Encode(item.UserId) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.Course)) %>
            </td>
            <td>
                <%= Html.Encode(item.OfficeId) %>
            </td>
            <td class="sum">
                <%= Html.Encode(String.Format("{0:N}", item.Equivalent)) %>
            </td>
            <td>
                <%if (string.IsNullOrEmpty(item.DocNls))
                  { %>
                <span class="warning"><%=Html.ResourceString("NlsAbsent")%></span>
                <% }
                  else
                { %>
                <%=item.DocNls%>
                <% } %>
            </td>
            <td>
                <%if (string.IsNullOrEmpty(item.DocNlsK))
                  { %>
                <span class="warning"><%=Html.ResourceString("NlsAbsent")%></span>
                <% }
                  else
                { %>
                <%=item.DocNlsK%>
                <% } %>
            </td>
        </tr>
        <% }
        %>
    </table>
    <div class="buttonsContainer">
        <%=Html.ResourceSubmitButton("CreateOs")%>
        <span class="UIButton">
            <%= Html.ResourceActionLink("Back", "Index") %>
        </span>
    </div>
    <% 
        } %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#checkAll").click(function () {
                if ($(this).attr('checked'))
                    $("input:checkbox.item").attr("checked", "checked");
                else
                    $("input:checkbox.item").removeAttr("checked");
            });
        });
    </script>
</asp:Content>
