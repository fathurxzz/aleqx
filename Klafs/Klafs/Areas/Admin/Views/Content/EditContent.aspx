<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Klafs.Models.Content>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>EditContent</h2>

    <% using (Html.BeginForm()) {%>

    <%=Html.HiddenFor(x=>x.Id) %>
        <%= Html.ValidationSummary(true) %>
        
       
            
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
                <%= Html.LabelFor(model => model.MenuTitle) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.MenuTitle)%>
                <%= Html.ValidationMessageFor(model => model.MenuTitle)%>
            </div>

            <div class="editor-label">
                <%= Html.LabelFor(model => model.Title) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Title) %>
                <%= Html.ValidationMessageFor(model => model.Title) %>
            </div>


            <div class="editor-label">
                <%= Html.LabelFor(model => model.Sign) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Sign)%>
                <%= Html.ValidationMessageFor(model => model.Sign)%>
            </div>

            <div class="editor-label">
                <%= Html.LabelFor(model => model.Sign2)%>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Sign2)%>
                <%= Html.ValidationMessageFor(model => model.Sign2)%>
            </div>



            <div class="editor-label">
                <%= Html.LabelFor(model => model.Description) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Description) %>
                <%= Html.ValidationMessageFor(model => model.Description) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.SortOrder) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.SortOrder) %>
                <%= Html.ValidationMessageFor(model => model.SortOrder) %>
            </div>
            
             <div class="editor-label">
                <%= Html.LabelFor(model => model.ImageSource) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.ImageSource) %>
                <%= Html.ValidationMessageFor(model => model.ImageSource) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Text) %>
            </div>
            <div class="editor-field">
                <%= Html.TextAreaFor(model => model.Text) %>
                <%= Html.ValidationMessageFor(model => model.Text) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.SeoDescription)%>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.SeoDescription) %>
                <%= Html.ValidationMessageFor(model => model.SeoDescription) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.SeoKeywords) %>
            </div>
            <div class="editor-field">
                <%= Html.TextAreaFor(model => model.SeoKeywords)%>
                <%= Html.ValidationMessageFor(model => model.SeoKeywords) %>
            </div>
            
            
           <div class="editor-label">
                <%= Html.LabelFor(model => model.SeoText)%>
            </div>
            <div class="editor-field">
                <%= Html.TextAreaFor(model => model.SeoText)%>
                <%= Html.ValidationMessageFor(model => model.SeoText)%>
            </div>
            
           
            
            <p>
                <input type="submit" value="Save" />
            </p>
        

    <% } %>

    <div>
        <%= Html.ActionLink("Back to List", "Index","Home",new{area="",id=Model.Name},null) %>
    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderTitleContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="HeaderTitleSignContent" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="SeoContent" runat="server">
</asp:Content>

