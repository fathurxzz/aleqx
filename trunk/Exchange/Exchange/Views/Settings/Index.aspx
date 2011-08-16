<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Exchange.Models.ExchangeSettings>" %>

<%@ Import Namespace="Exchange.Models.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=Html.ResourceString("Settings") %></h2>
    <% using (Html.BeginForm())
       {%>
    <%= Html.ValidationSummary(true) %>
    <table>
        <tr>
            <th>
                <%=Html.ResourceString("Key")%>
            </th>
            <th>
                <%=Html.ResourceString("Value")%>
            </th>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("TransactionMatchingInterval")%>
            </td>
            <td>
                <%= Html.TextBoxFor(model => model.TransactionMatchingInterval) %>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("CheckingCourseMor")%>
            </td>
            <td>
                <%= Html.CheckBoxFor(model => model.CheckingCourseMor) %>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("CheckingCourseMorTime")%>
            </td>
            <td>
                <%= Html.TextBox("CheckingCourseMorTime",Model.CheckingCourseMorTime.ToShortTimeString())%>
            </td>
        </tr>
    </table>
    <div class="buttonsContainer">
        <%=Html.ResourceSubmitButton("Save") %>
    </div>
    <% } %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>
