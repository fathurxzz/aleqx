<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AvenueGreen.Models.Content>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%  if (Model != null)
    {

        if (Request.IsAuthenticated)
        {
         %>   
            <%=Html.ActionLink("[редактировать]", "EditContentItem", "Admin", new { id = Model.Id, contentLevel=Model.ContentLevel /*parentId = mparentId, isGalleryItem = Model.IsGalleryItem*/ }, new { @class = "adminLink" })%>
           <% 
        }
       %><br/><%
        Response.Write(Model.Text);
       } %>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
 AvenueGreen<% if (Model != null) Response.Write(" - " + Model.Title); %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentTitle" runat="server">
<% if (Model != null) Response.Write(Model.Title); %>
</asp:Content>