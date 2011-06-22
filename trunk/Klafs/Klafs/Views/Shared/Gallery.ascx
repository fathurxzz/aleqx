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
<div id="galleryContainer">
    <%  
        foreach (var item in Model.OrderBy(c=>c.SortOrder))
        {
    %>
    <div class="galleryItem">
        <%if (Request.IsAuthenticated)
          { %> <span class="sortOrder">
          <%=item.SortOrder %>
          </span>
        <%} %>
        <div class="galleryImage">
            <!--<a rel="group1" href="../../Content/Photos/<%=item.ImageSource%>" class="fancy iframe">-->
                <%=Html.CachedImage("~/Content/Photos/", item.ImageSource, "thumbnail", item.ImageSource, true)%>
            <!--</a>-->
        </div>
        <div class="imageSign">
            <%=item.Description %>
        </div>
        <%if (Request.IsAuthenticated)
          { %>
        <%= Html.ActionLink("Редактировать", "EditPhoto", "Content", new { area = "Admin", id = item.Id, contentName = ViewData["contentName"] }, new { @class = "adminLink" })%>
        <%= Html.ActionLink("Удалить", "DeletePhoto", "Content", new { area = "Admin", id = item.Id }, new { onclick = "return confirm('Вы уверены что хотите удалить фото?')", @class = "adminLink" })%>
        <%} %>
    </div>
    <%
        }
    %>
</div>
<div class="clear">
</div>
<div class="separator">
</div>
<%
    }%>
