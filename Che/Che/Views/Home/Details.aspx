<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Che.Models.Content>" %>
<%@ Import Namespace="Dev.Mvc.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <%if(Request.IsAuthenticated)
      {
          %>
          <%=Html.ActionLink("Добавить запись","AddDetailsItem","Content",new{area="Admin",id=Model.Id},new{@class="adminLink"}) %>
          <%
      } %>
    
    <%if (Model.Children.Count > 0)
      {
    %>
    <table id="contentItemDetails">
    <tr>
    <% foreach (var item in Model.Children)
{
    
} %>
    </tr>
    </table>
    <%
      
        } %>
        <div id="backLink">
        << <a href="#" onclick="javascript:history.back();">В каталог</a>
        </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%=Model.PageTitle %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="server">
    <a href="/<%=SiteHelper.GetPathByContentType(Model.Parent.ContentType) %>">
        <%=Model.Parent.Title %></a>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="SubTitle" runat="server">
    <div id="subTitle">
        /
        <%=Model.Title %>
    </div>
</asp:Content>
