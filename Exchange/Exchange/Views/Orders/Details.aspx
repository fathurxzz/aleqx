<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Exchange.Models.Order>" %>

<%@ Import Namespace="Exchange.Models.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%using (Html.BeginForm())
      {
    %>

    <%=Html.HiddenFor(model=>model.Id) %>

    Розпорядження №<%=Model.Number%>
    за операціями купівлі філіями
    <table>
        <tr>
            <th>
            </th>
            <th>
                <%=Html.ResourceString("Office")%>
            </th>
            <th>
                <%=Html.ResourceString("Nls")%>
            </th>
            <th>
                <%=Html.ResourceString("Podrid")%>
            </th>
            <th>
                <%=Html.ResourceString("DocSum")%>
            </th>
            <th>
                <%=Html.ResourceString("Course")%>
            </th>
            <th>
                <%=Html.ResourceString("Equiv")%>
            </th>
            <th></th>
        </tr>
        <%
            foreach (var item in Model.Deals)
            {
        %>
        <tr>
            <td>
                <%=Html.CheckBox("cb_"+item.Id)%>
            </td>
            <td>
                <%=Html.Encode(item.OfficeName)%>
            </td>
            <td>
                <%=Html.Encode(item.Nls)%>
            </td>
            <td>
                <%=item.Podrid%>
            </td>
            <td>
                <%=Html.Encode(String.Format("{0:N}", item.DocSum))%>
            </td>
            <td>
                <%=item.Course%>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:N}", item.Equivalent))%>
            </td>
            <td>
                <%=Html.ResourceActionLink("SplitDealSum", "SplitDeal", "Orders", new { id = item.Id, orderId = Model.Id }, null)%>
            </td>
        </tr>
        <%
            }
        %>
    </table>
    <div class="buttonsContainer">
        <%=Html.ResourceSubmitButton("Delete",FormAction.Delete) %>
        <span class="UIButton">
            <%= Html.ResourceActionLink("Back", "Index") %>
        </span>
    </div>
    <%} %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>
