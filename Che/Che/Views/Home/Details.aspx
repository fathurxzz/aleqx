﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Che.Models.Content>" %>
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
    
    <% foreach (var item in Model.Children.OrderBy(c=>c.SortOrder))
{
    %>
    <tr> 
    
    <td class="description"><%=item.Description %>
            <%if(Request.IsAuthenticated)
{
    %>
    <br/>
    <%=Html.ActionLink("Редактировать", "EditDetailsItem", "Content", new { id = item.Id, area = "Admin" }, new { @class = "adminLink"})%>
    <br/>
    <%=Html.ActionLink("Удалить", "Delete", "Content", new { id = item.Id, area = "Admin" }, new { @class = "adminLink", onclick = "return confirm('Вы действительно хотите удалить запись?')" })%>
    
    <%
} %>
    </td>   
    <td>
        <a href="/Content/Photos/<%=item.ImageSource %>" class="fancy" >
        <%=Html.CachedImage("~/Content/Photos/", item.ImageSource, "thumbnail2", item.ImageSource)%>
        </a>

    </td>   
    </tr>
        <%
} %>
    
    </table>
    <%
      
        } %>
        <div id="backLink">
        &laquo; <!--<a href="javascript:history.back();">В каталог</a>-->
         <a href="/<%=SiteHelper.GetPathByContentType(Model.Parent.ContentType) %>">В каталог</a>
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


<asp:Content ContentPlaceHolderID="includes" runat="server">
<script type="text/javascript">
$(function () {
    $(".fancy").fancybox({ hideOnContentClick: true, showCloseButton: false, cyclic: true, showNavArrows: false, padding: 0, margin: 0, centerOnScroll: false,titleShow:false, autoScale:false });
});
</script>
</asp:Content>



<asp:Content ID="Content5" ContentPlaceHolderID="RootLink" runat="server">
 <%
               
                if (Model.Id!=1)
              {%>
            <a href="/" class="rootLink"><img src="../../Content/img/pixel.gif" alt="" /></a>
            <%
              }%>
</asp:Content>