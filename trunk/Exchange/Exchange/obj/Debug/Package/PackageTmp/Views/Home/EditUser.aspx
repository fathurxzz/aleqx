<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Exchange.Models.User>" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>EditUser</h2>
    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>
        <div class="container">
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Phone) %>
            </div>
            <div class="field-validation">
                <%= Html.ValidationMessageFor(model => model.Phone) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Phone) %>
            </div>
            </div>
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

