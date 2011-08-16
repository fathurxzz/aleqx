<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Exchange.Models.Helpers.OfficeSelectListItem>>" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>EditModeratorsOffices</h2>

    <% 
        using(Html.BeginForm()){
        %>

        <%=Html.Hidden("userId",ViewData["userId"]) %>
    <table>
        <tr>
            <th></th>
            <th>
                OfficeName
            </th>
        </tr>

    <%
            foreach (var item in Model)
            {%>
    
        <tr class="officeLevel<%=item.OfficeLevel%>">
            <td>
                <%=Html.CheckBox("cb_" + item.Value, item.Selected)%>
            </td>
            <td style="text-align:left">
                <%=Html.Encode(item.Text)%>
            </td>
        </tr>
    
    <%
            }%>

    </table>

    <div class="buttonsContainer">
        <%=Html.ResourceSubmitButton("Save") %>
        <span class="UIButton">
            <%= Html.ResourceActionLink("Back", "Moderators") %>
        </span>
    </div>

    <%
        }%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>

