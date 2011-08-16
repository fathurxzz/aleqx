<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Exchange.Models.Deal>>" %>

<%@ Import Namespace="Exchange.Models" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 style="float: left">
        <%=Html.ResourceString("Deals")%></h2>
    <div style="float: right">
        ---</div>
    <div style="clear: both">
    </div>
    <div class="line">
    </div>
    <%using (Html.BeginForm())
      {
    %>

    <table class="filterContainer">
    <tr>
        <td><%Html.RenderPartial("CurrencySelector", ViewData["CurrencyList"], new ViewDataDictionary { new KeyValuePair<string, object>("controllerName", "CentreDeals") });%></td>
        <td><%Html.RenderPartial("OperationSelector", ViewData["OperList"]); %></td>
        <td><%Html.RenderPartial("SingleDateSelector");%></td>
        <td><%Html.RenderPartial("OperSignSelector", ViewData["OperSignList"]);%></td>
    </tr>
    <tr>
        <td><%=Html.ResourceString("courseMor")%>
            <%=Html.TextBox("courseMor") %>
            <%=Html.ResourceSubmitButton("Update", FormAction.Update)%></td>
        <td></td>
    </tr>
    </table>

    <table>
        <tr>
            <th><%=Html.CheckBox("checkAll") %></th>
            <th>
                <%=Html.ResourceString("Operation")%>
            </th>
            <th>
                <%=Html.ResourceString("Podrid")%>
            </th>
            <th>
                <%=Html.ResourceString("Office")%>
            </th>
            <th>
                <%=Html.ResourceString("TenderNumber")%>
            </th>
            <th>
                <%=Html.ResourceString("Currency")%>
            </th>
            <th>
                <%=Html.ResourceString("DocSum")%>
            </th>
            <th>
                <%=Html.ResourceString("TenderCourse")%>
            </th>
            <th>
                <%=Html.ResourceString("TenderCourseOrient")%>
            </th>
            <th>
                <%=Html.ResourceString("DealCourse")%>
            </th>
            <th>
                <%=Html.ResourceString("CourseClient")%>
            </th>
            <th>
                <%=Html.ResourceString("ExtraCommissionSum")%>
            </th>
            <th>
                <%=Html.ResourceString("CourseMor")%>
            </th>
            <th>
                <%=Html.ResourceString("Commission")%>
            </th>
            <th>
                <%=Html.ResourceString("Client")%>
            </th>
            <th>
                <%=Html.ResourceString("ClientCode")%>
            </th>
            <th class="empty">
            </th>
        </tr>
        <% foreach (var item in Model.OrderBy(d => d.Operation))
           { %>
        <tr>
            <td>
                <%=Html.CheckBox("cb_" + item.Id, new{@class="item"})%>
            </td>
            <td>
                <%= Html.ResourceString(item.Operation.ToString()) %>
            </td>
            <td>
                <%= Html.Encode(item.Podrid) %>
            </td>
            <td>
                <%= Html.Encode(item.OfficeName) %>
            </td>
            <td>
                <%= Html.Encode(item.TenderId) %>
            </td>
            <td class="currency c<%=item.CurId%>">
                <%= Html.Encode(item.CurId) %>
            </td>
            <td class="sum">
                <%= item.DocSum.ToString("N") %>
            </td>
            <td>
                <%= Html.Encode(item.TenderCourse) %>
            </td>
            <td>
                <%= Html.Encode(item.TenderCourseOrient) %>
            </td>
            <td>
                <%= Html.Encode(item.Course) %>
            </td>
            <td>
                <%= Html.Encode(item.CourseClient) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.ExtraCommissionSum)) %>
            </td>
            <td>
                <%= Html.Encode(item.CourseMor) %>
            </td>
            <td>
                <%= Html.Encode(item.Commission) %>
            </td>
            <td>
                <%= Html.Encode(item.ClientName) %>
            </td>
            <td>
                <%= Html.Encode(item.ClientCode) %>
            </td>
            <td class="empty">
                <%= Html.ResourceActionLink("Edit", "Edit", new { id=item.Id  }) %>
            </td>
        </tr>
        <% } %>
    </table>
      <%
        } %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
       
</asp:Content>
