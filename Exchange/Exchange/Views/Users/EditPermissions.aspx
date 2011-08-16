<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Exchange.Models.User>" %>

<%@ Import Namespace="Exchange.Models" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
    </h2>
    <% using (Html.BeginForm())
       {%>
    <%= Html.ValidationSummary(true) %>
    <%= Html.HiddenFor(model => model.Id) %>
    <%
        var userRoles = (IEnumerable<UserRole>)ViewData["userRolesToAssign"];
    %>
    <div style="border: none; width: 300px; margin: 10px auto;">
        <%
            foreach (UserRole role in userRoles)
            {
        %>
        <div style="white-space: nowrap; margin: 3px;">
            <%=Html.CheckBox("cbur_"+role.Id,Model.UserRoleIds.Contains(role.Id)) %>
            <%=Html.ResourceString(role.Name)%>
        </div>
        <%
            }
        %>
    </div>
    <table>
        <tr>
            <td class="title">
                <%=Html.ResourceString("IsAutorized")%>
            </td>
            <td>
                <%= Html.CheckBoxFor(model => model.IsAutorized) %>
            </td>
        </tr>
        <tr>
            <td class="title">
                <%= Html.LabelFor(model => model.Name) %>
            </td>
            <td>
                <%= Html.DisplayTextFor(model => model.Name) %>
            </td>
        </tr>
        <tr>
            <td class="title">
                <%= Html.LabelFor(model => model.LastActivityDate) %>
            </td>
            <td>
                <%=Html.Encode(Model.LastActivityDate.ToShortDateString()) %>
            </td>
        </tr>
        <tr>
            <td class="title">
                <%= Html.LabelFor(model => model.TabNum) %>
            </td>
            <td>
                <%= Html.DisplayTextFor(model => model.TabNum) %>
            </td>
        </tr>
        <tr>
            <td class="title">
                <%= Html.LabelFor(model => model.Podrid) %>
            </td>
            <td>
                <%= Html.DisplayTextFor(model => model.Podrid) %>
            </td>
        </tr>
        <tr>
            <td class="title">
                <%=Html.ResourceString("Office")%>
            </td>
            <td>
                <%= Html.DisplayTextFor(model => model.OfficeName) %>
            </td>
        </tr>
        <tr>
            <td class="title">
                <%= Html.LabelFor(model => model.Phone) %>
            </td>
            <td>
                <%= Html.ValidationMessageFor(model => model.Phone) %>
                <%= Html.TextBoxFor(model => model.Phone) %>
            </td>
        </tr>
        <tr>
            <td class="title">
                <%= Html.LabelFor(model => model.UserIdOdb) %>
            </td>
            <td>
                <%= Html.TextBoxFor(model => model.UserIdOdb) %>
                <%= Html.ValidationMessageFor(model => model.UserIdOdb) %>
            </td>
        </tr>
        <tr>
            <td class="title">
                <%=Html.ResourceString("ChangeOffice")%>
            </td>
            <td>
                <%=Html.DropDownList("drOffices", (IEnumerable<SelectListItem>)ViewData["changeOffices"])%>
            </td>
        </tr>
    </table>
    <table style="margin-top: 20px;">
        <tr>
            <th>
                <%=Html.ResourceString("Currency")%>
            </th>
            <th>
                <%=Html.ResourceString("View")%>
            </th>
            <th>
                <%=Html.ResourceString("Processing")%>
            </th>
        </tr>
        <%

            var allCurrencies = (IList<Currency>)ViewData["allCurrencies"];

            foreach (Currency currency in allCurrencies)
            {
                
                
        %>
        <tr>
            <td>
                <%=Html.Encode(currency.CurName)%>
            </td>
            <td>
                <%=Html.CheckBox("cbcv_" + currency.CurId, UserCurrency.CheckForView(Model.CurrencyList,currency.CurId) )%>
            </td>
            <td>
                <%=Html.CheckBox("cbcp_" + currency.CurId, UserCurrency.CheckForProcessing(Model.CurrencyList, currency.CurId))%>
            </td>
        </tr>
        <%
}
           
        %>
    </table>
    <div class="buttonsContainer">
        <%=Html.ResourceSubmitButton("Save") %>
        <span class="UIButton">
            <%= Html.ResourceActionLink("Back", "Index") %>
        </span>
    </div>
    <% } %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>
