<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Klafs.Models.GalleryItem>>" %>
<%@ Import Namespace="Dev.Mvc.Helpers" %>
<%if (Model.Count() > 0)
  {
%>
<div id="galleryContainer">
    <%  
        foreach (var item in Model)
        {
    %>
    <div class="galleryItem">
        <div class="galleryImage">
            <%=Html.CachedImage("~/Content/Photos/", item.ImageSource, "thumbnail", item.ImageSource, true)%>
        </div>
        <div class="imageSign">
            <%=item.Description %>
        </div>
        <%if (Request.IsAuthenticated)
          { %>
        <%= Html.ActionLink("Удалить", "DeletePhoto", "Content", new { area = "Admin", id = item.Id }, new { onclick = "return confirm('Вы уверены что хотите удалить фото?')", @class = "adminLink" })%>
        <%} %>
    </div>
    <%
        }
    %>
</div>
<div class="clear">
</div>
<div class="separator"></div>
<%
    }%>
