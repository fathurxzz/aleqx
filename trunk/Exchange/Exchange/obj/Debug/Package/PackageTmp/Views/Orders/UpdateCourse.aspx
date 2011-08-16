<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Exchange.Models.Order>" %>

<%@ Import Namespace="Exchange.Models.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        UpdateCourse</h2>
    <%using (Html.BeginForm())
      {
  
    %>
    <%=Html.HiddenFor(model=>model.Id)%>
    <%=Html.TextBoxFor(model=>model.Course)%>
    <div class="buttonsContainer">
        <%=Html.ResourceSubmitButton("Save") %>
        <span class="UIButton">
            <%= Html.ResourceActionLink("Back", "Index") %>
        </span>
    </div>
    <%} %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>
