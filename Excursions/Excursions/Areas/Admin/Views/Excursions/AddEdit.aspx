<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Excursions.Models.Excursion>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	AddEdit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>AddEdit</h2>

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
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Date) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Date) %>
                <%= Html.ValidationMessageFor(model => model.Date) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.SortOrder) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.SortOrder) %>
                <%= Html.ValidationMessageFor(model => model.SortOrder) %>
            </div>
            
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%= Html.ActionLink("Назад к списку экскурсий", "Index", "Excursions", new { area=""},null)%>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="LeftContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="RightContent" runat="server">
</asp:Content>

