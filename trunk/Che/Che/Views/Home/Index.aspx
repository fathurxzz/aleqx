<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Che.Models.Content>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    <%=Model.PageTitle %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="server">
    <%=Model.Title %>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<%if(Request.IsAuthenticated)
  {
      %>
      <%=Html.ActionLink("Добавить подраздел","Add","Content",new {area="Admin",id=Model.Id},new{@class="adminLink"}) %>
      <%=Html.ActionLink("Редактировать","Edit","Content",new {area="Admin",id=Model.Id},new{@class="adminLink"}) %>
      <%
      
  } %>

</asp:Content>
