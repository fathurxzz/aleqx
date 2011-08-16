<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Login page</h2>
    <% using (Html.BeginForm())
       {
       %>
    <%=Html.DropDownList("drUserList",  (IEnumerable<SelectListItem>) ViewData["userList"])%>
        <div class="buttonsContainer">
            <%=Html.ResourceSubmitButton("Login") %>
        </div>   
       <%    
       } %>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>
