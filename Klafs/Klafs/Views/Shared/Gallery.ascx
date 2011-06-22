<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Klafs.Models.GalleryItem>>" %>
<%@ Import Namespace="Dev.Mvc.Helpers" %>
<%if (Model.Count() > 0)
  {
%>
<script type="text/javascript">
    /*$(function () {
        $(".fancy").fancybox({ showCloseButton: true, cyclic: true, showNavArrows: true, padding: 0, margin: 0, centerOnScroll: true });
    });*/
</script>






<table id="galleryContainer">
<tr>
    <%
      int cnt = 0;
        foreach (var item in Model.OrderBy(c=>c.SortOrder))
        {
            cnt++;
    %>
    <td class="galleryItem">
        <%if (Request.IsAuthenticated)
          { %> <span class="sortOrder">
          <%=item.SortOrder %>
          </span>
        <%} %>
        <div class="galleryImage">
        <center>
            <div">
            <!--<a rel="group1" href="../../Content/Photos/<%=item.ImageSource%>" class="fancy iframe">-->
                <%=Html.CachedImage("~/Content/Photos/", item.ImageSource, "thumbnail", item.ImageSource, true)%>
            <!--</a>-->
             <div class="imageSign">
            <%=item.Description %>
            </div>
            </div>
            
            </center>
        </div>
       
        <%if (Request.IsAuthenticated)
          { %>

          <table style="width:100%">
          <tr>
            <td align="left" style="padding-left:10px;"><%= Html.ActionLink("Редактировать", "EditPhoto", "Content", new { area = "Admin", id = item.Id, contentName = ViewData["contentName"] }, new { @class = "adminLink" })%></td>
            <td align="right" style="padding-right:10px;"><%= Html.ActionLink("Удалить", "DeletePhoto", "Content", new { area = "Admin", id = item.Id }, new { onclick = "return confirm('Вы уверены что хотите удалить фото?')", @class = "adminLink" })%></td>
          </tr>
          </table>
        
        
        
        <%} %>
    </td>
    <%if (cnt % 2 == 0)
{%>

    </tr><tr>
    
    <%
}
        }
    %>
    </tr>
</table>
<div class="separator">
</div>
<%
    }%>
