<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Exchange.Models.User>" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    

    
    <table class="details">
    <tr>
        <th colspan="2"><%=Html.ResourceString("CommonInformation")%></th>
    </tr>
    <tr>
        <td class="title"><%=Html.ResourceString("Office")%></td>
        <td><%= Html.Encode(Model.OfficeName) %></td>
    </tr>
    <tr>
        <td class="title"><%=Html.ResourceString("Mfo")%></td>
        <td><%= Html.Encode(Model.Podrid) %></td>
    </tr>
    <tr>
        <td class="title"><%=Html.ResourceString("Role")%></td>
        <td><%
            foreach (int roleId in Model.UserRoleIds)
            {
              %>
              <%=Html.ResourceString(((SiteRoles)roleId).ToString())%><br />
              <%
            }
             %></td>
        </tr>
    <tr>
        <td class="title"><%=Html.ResourceString("TabNum")%></td>
        <td><%= Html.Encode(Model.TabNum) %></td>
    </tr>
    <tr>
        <td class="title"><%=Html.ResourceString("User") %></td>
        <td><%= Html.Encode(Model.Name) %></td>
    </tr>
    <tr>
        <td class="title"><%=Html.ResourceString("Phone") %></td>
        <td><%= Html.Encode(Model.Phone) %></td>
        <td class="empty"><%= Html.ResourceActionLink("Edit", "EditUser", new { id = Model.Id })%></td>
    </tr>
    </table>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
<%=Html.ResourceString("MainPage") %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>

