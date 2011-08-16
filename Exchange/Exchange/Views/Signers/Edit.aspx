<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Exchange.Models.Signer>" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>
    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>
        <%=Html.HiddenFor(model => model.Id)%>
        <%=Html.HiddenFor(model => model.OfficeId)%>
        <table>
        <tr>
            <td><%= Html.LabelFor(model => model.Name) %></td>
            <td><%= Html.TextBoxFor(model => model.Name) %>
            <div class="field-validation">
                <%= Html.ValidationMessageFor(model => model.Name) %>
                </div>
            </td>
        </tr>
        <tr>
            <td><%= Html.LabelFor(model => model.Post) %></td>
            <td><%= Html.TextBoxFor(model => model.Post) %>
            <div class="field-validation">
                <%= Html.ValidationMessageFor(model => model.Post) %>
                </div>
                </td>
        </tr>
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
    <script type="text/javascript" src="/Scripts/MicrosoftAjax.js"></script>
    <script type="text/javascript" src="/Scripts/MicrosoftMvcValidation.js"></script>
</asp:Content>

