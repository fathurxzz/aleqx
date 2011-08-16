<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Exchange.Models.Payment>>" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Payments</h2>

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
    <% using (Html.BeginForm())
       {

%>
    <table>
        <tr>
            <th><%=Html.CheckBox("checkAll")%></th>
            <th>
                Id
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
                <%=Html.ResourceString("Course")%>
            </th>
            <th>
                <%=Html.ResourceString("Equiv")%>
            </th>
            <th>
                Podrid
            </th>
            <th>
                DocNls
            </th>
            <th>
                DocNlsK
            </th>
            <th>
                Edrpou
            </th>
            <th>
                OfficeName
            </th>
        </tr>

    <% foreach (var item in Model)
{ %>
    
        <tr>
            <td>
                <%=Html.CheckBox("cb_" + item.Id, new {@class = "item"})%>
            </td>
            <td>
                <%=Html.Encode(item.Id)%>
            </td>
            <td>
                <%=Html.Encode(item.OrderId)%>
            </td>
            <td>
                <%=Html.Encode(String.Format("{0:g}", item.CreateDate))%>
            </td>
            <td>
                <%=Html.Encode(item.Paid)%>
            </td>
            <td>
                <%=Html.Encode(String.Format("{0:g}", item.PayDate))%>
            </td>
            <td>
                <%=Html.Encode(item.CurId)%>
            </td>
            <td class="sum">
                <%=Html.Encode(String.Format("{0:N}", item.DocSum))%>
            </td>
            <td>
                <%=Html.Encode(item.DealCurId)%>
            </td>
            <td>
                <%=Html.Encode(item.OsFileName)%>
            </td>
            <td>
                <%=Html.Encode(String.Format("{0:F}", item.Course))%>
            </td>
            <td class="sum">
                <%=Html.Encode(String.Format("{0:N}", item.Equivalent))%>
            </td>
            <td>
                <%=Html.Encode(item.Podrid)%>
            </td>
            <td>
                 <% if (string.IsNullOrEmpty(item.DocNls))
{ %>
                <span class="warning"><%=Html.ResourceString("NlsAbsent")%></span>
                <% }
else
{ %>
                <%=item.DocNls%>
                <% } %>
            </td>
            <td>
                <% if (string.IsNullOrEmpty(item.DocNlsK))
{ %>
                <span class="warning"><%=Html.ResourceString("NlsAbsent")%></span>
                <% }
else
{ %>
                <%=item.DocNlsK%>
                <% } %>
            </td>
            <td>
                <%=Html.Encode(item.Edrpou)%>
            </td>
            <td>
                <%=Html.Encode(item.OfficeName)%>
            </td>
        </tr>
    
    <% } %>
    </table>
     <div class="buttonsContainer">
     <%=Html.ResourceSubmitButton("Delete", FormAction.Delete, "AreYouSureToDeleteSelectedItems")%>
     </div>
     <% } %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
 
</asp:Content>

