<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Exchange.Models.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Details</h2>

    <fieldset>
        <legend>Fields</legend>
        
        <div class="display-label">Id</div>
        <div class="display-field"><%= Html.Encode(Model.Id) %></div>
        
        <div class="display-label">Name</div>
        <div class="display-field"><%= Html.Encode(Model.Name) %></div>
        
        <div class="display-label">OfficeId</div>
        <div class="display-field"><%= Html.Encode(Model.OfficeId) %></div>
        
        <div class="display-label">LastActivityDate</div>
        <div class="display-field"><%= Html.Encode(String.Format("{0:g}", Model.LastActivityDate)) %></div>
        
        <div class="display-label">TabNum</div>
        <div class="display-field"><%= Html.Encode(Model.TabNum) %></div>
        
        <div class="display-label">IsAutorized</div>
        <div class="display-field"><%= Html.Encode(Model.IsAutorized) %></div>
        
        <div class="display-label">Podrid</div>
        <div class="display-field"><%= Html.Encode(Model.Podrid) %></div>
        
        <div class="display-label">Phone</div>
        <div class="display-field"><%= Html.Encode(Model.Phone) %></div>
        
        <div class="display-label">OfficeName</div>
        <div class="display-field"><%= Html.Encode(Model.OfficeName) %></div>
        
        <div class="display-label">UserCode</div>
        <div class="display-field"><%= Html.Encode(Model.UserCode) %></div>
        
        <div class="display-label">DateClose</div>
        <div class="display-field"><%= Html.Encode(String.Format("{0:g}", Model.DateClose)) %></div>
        
        <div class="display-label">UserIdOdb</div>
        <div class="display-field"><%= Html.Encode(Model.UserIdOdb) %></div>
        
        <div class="display-label">ParentOfficeId</div>
        <div class="display-field"><%= Html.Encode(Model.ParentOfficeId) %></div>
        
        <div class="display-label">Language</div>
        <div class="display-field"><%= Html.Encode(Model.Language) %></div>
        
    </fieldset>
    <p>
        <%= Html.ActionLink("Back to List", "Index") %>
    </p>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>

