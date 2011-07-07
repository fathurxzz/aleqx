<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Che.Models.Content>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>

    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.ContentType) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.ContentType) %>
                <%= Html.ValidationMessageFor(model => model.ContentType) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Description) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Description) %>
                <%= Html.ValidationMessageFor(model => model.Description) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Id) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Id) %>
                <%= Html.ValidationMessageFor(model => model.Id) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.ImageSource) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.ImageSource) %>
                <%= Html.ValidationMessageFor(model => model.ImageSource) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Name) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Name) %>
                <%= Html.ValidationMessageFor(model => model.Name) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.PageTitle) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.PageTitle) %>
                <%= Html.ValidationMessageFor(model => model.PageTitle) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.SeoDescription) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.SeoDescription) %>
                <%= Html.ValidationMessageFor(model => model.SeoDescription) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.SeoKeywords) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.SeoKeywords) %>
                <%= Html.ValidationMessageFor(model => model.SeoKeywords) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.SortOrder) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.SortOrder) %>
                <%= Html.ValidationMessageFor(model => model.SortOrder) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Text) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Text) %>
                <%= Html.ValidationMessageFor(model => model.Text) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Title) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Title) %>
                <%= Html.ValidationMessageFor(model => model.Title) %>
            </div>
            
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="server">
</asp:Content>

