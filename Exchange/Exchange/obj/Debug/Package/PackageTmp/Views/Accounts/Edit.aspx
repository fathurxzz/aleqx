<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Exchange.Models.Account>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>

    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Id) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Id) %>
                <%= Html.ValidationMessageFor(model => model.Id) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.OperId) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.OperId) %>
                <%= Html.ValidationMessageFor(model => model.OperId) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.OfficeId) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.OfficeId) %>
                <%= Html.ValidationMessageFor(model => model.OfficeId) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.OfficeName) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.OfficeName) %>
                <%= Html.ValidationMessageFor(model => model.OfficeName) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.CurId) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.CurId) %>
                <%= Html.ValidationMessageFor(model => model.CurId) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Nls) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Nls) %>
                <%= Html.ValidationMessageFor(model => model.Nls) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.OperSign) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.OperSign) %>
                <%= Html.ValidationMessageFor(model => model.OperSign) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Podrid) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Podrid) %>
                <%= Html.ValidationMessageFor(model => model.Podrid) %>
            </div>
            
            <p>
                <input type="submit" value="Save" />
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
</asp:Content>

