<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Babich.Models.Content>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>

    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.ContentLevel) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.ContentLevel) %>
                <%= Html.ValidationMessageFor(model => model.ContentLevel) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.ContentTitle) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.ContentTitle) %>
                <%= Html.ValidationMessageFor(model => model.ContentTitle) %>
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
                <%= Html.LabelFor(model => model.Lang) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Lang) %>
                <%= Html.ValidationMessageFor(model => model.Lang) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.MenuTitle) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.MenuTitle) %>
                <%= Html.ValidationMessageFor(model => model.MenuTitle) %>
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

    <div>
        <%= Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="SubMenuContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentTitleContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="GalleryContent" runat="server">
</asp:Content>

