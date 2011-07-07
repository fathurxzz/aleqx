<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Che.Models.Content>" %>
<%@ Import Namespace="Dev.Mvc.Helpers" %>

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

      <%if(Model.Id!=1)
{
            %>
                <%=Html.ActionLink("Добавить подраздел","Add","Content",new {area="Admin",id=Model.Id},new{@class="adminLink"}) %>      
            <%
} %>

      
      <%=Html.ActionLink("Редактировать","Edit","Content",new {area="Admin",id=Model.Id},new{@class="adminLink"}) %>
      <%
      
  } %>


  <%
      if (Model.Children.Count > 0)
      {
          %>
          <table id="contentList">
          <tr>
          <%
          int cnt = 0;
          foreach (var item in Model.Children)
          {
              if(cnt%2==0&&cnt!=0)
              {
                  %>
                  </tr>
                  <tr>
                  <%
              }

%>
         <td class="<%=cnt%2==0?"firstItem":"secondItem"%>">
         <a href="/<%=SiteHelper.GetPathByContentType(Model.ContentType) %>/<%=item.Name%>">
         <%=Html.CachedImage("~/Content/Photos/", item.ImageSource, "thumbnail1", item.ImageSource)%>
         </a>
         <div class="contentItemDescription">
         <%=item.Description %></div>
         
         </td>
         <%
              cnt++;
          }%>
          </tr>
          </table>
          <%
      }%>


</asp:Content>
