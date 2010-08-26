<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Rivs.Models.Content>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	«Ривс»<% if (Model != null) Response.Write(" - " + Model.Title); %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%if (Model != null)
      {
      
       if (Request.IsAuthenticated)
           {
           %>
           <div>
           <%=Html.ActionLink("[редактировать]", "EditContentItem", "Admin", new { id = Model.Id }, new { @class = "adminLink" })%>
           <%=Html.ActionLink("[удалить]", "DeleteContentItem", "Admin", new { id = Model.Id }, new { @class = "adminLink", onclick = "return confirm('Удалить этот пункт?')" })%>
           </div>
           <%
           }
        Response.Write(Model.Text);
      } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentContainerTitle" runat="server">
<% if (Model != null) Response.Write(Model.Title); %>
</asp:Content>
