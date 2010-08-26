<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Rivs.Models.Content>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	�����<% if (Model != null) Response.Write(" - " + Model.Title); %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%if (Model != null)
      {
      
       if (Request.IsAuthenticated)
           {
           %>
           <div>
           <%=Html.ActionLink("[�������������]", "EditContentItem", "Admin", new { id = Model.Id }, new { @class = "adminLink" })%>
           <%=Html.ActionLink("[�������]", "DeleteContentItem", "Admin", new { id = Model.Id }, new { @class = "adminLink", onclick = "return confirm('������� ���� �����?')" })%>
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
