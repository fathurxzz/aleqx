<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Exchange.Models.Account>" %>
<%@ Import Namespace="Exchange.Models" %>
<%@ Import Namespace="Exchange.Models.Enum" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create</h2>
    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%=Html.ResourceString("Operation") %>
            </div>
            <div class="editor-field">
                <%=Html.DropDownList("drOper",new List<SelectListItem>{new SelectListItem{Text = Helper.GetResourceString(EOperation.Buy.ToString()), Value = "1"},new SelectListItem{Text = Helper.GetResourceString(EOperation.Sell.ToString()) ,Value = "2"}}) %>
            </div>
            
            <div class="editor-label">
                <%=Html.ResourceString("Office") %>
            </div>
            <div class="editor-field">
                <%=Html.DropDownList("drOffice", (IEnumerable<SelectListItem>)ViewData["offices"])%>
            </div>
            
            <div class="editor-label">
                <%=Html.ResourceString("Currency") %>
            </div>
            <div class="editor-field">
                <%=Html.DropDownList("drCurrency", (IEnumerable<SelectListItem>)ViewData["currencies"])%>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Nls) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Nls) %>
                <%= Html.ValidationMessageFor(model => model.Nls) %>
            </div>
            
            <div class="editor-label">
                <%=Html.ResourceString("OperSign") %>
            </div>
            <div class="editor-field">
                <%=Html.DropDownList("drOperSign",new List<SelectListItem>{new SelectListItem{Text = Helper.GetResourceString(EOperSign.CurrencyPosition.ToString()), Value = "0"},new SelectListItem{Text = Helper.GetResourceString(EOperSign.ClientCash.ToString()) ,Value = "2"}}) %>
            </div>
            
            <p>
                <input type="submit" value="<%=Html.ResourceString("Create")%>" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%= Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
    <script type="text/javascript" src="/Scripts/MicrosoftAjax.js"></script>
    <script type="text/javascript" src="/Scripts/MicrosoftMvcValidation.js"></script>
</asp:Content>

