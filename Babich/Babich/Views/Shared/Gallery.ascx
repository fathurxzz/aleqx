<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Babich.Models.Gallery>>" %>
<%@ Import Namespace="Dev.Mvc.Helpers" %>

<%if (Model.Count() > 0 && ViewData["galleryId"]==null)
  {%>


  <script type="text/javascript">

      $(function () {

          $(".galleryItem").mousemove(function () {
              $(this).children('div').addClass("show");
          });
          $(".galleryItem").mouseleave(function () {
              $(this).children('div').removeClass("show");
          });
      });

  </script>


<div id="gallery">

    <%

      int currentPage = 1;

      if (ViewData["galleryPage"] != null)
      {
          currentPage = (int) ViewData["galleryPage"];
      }

      int cnt = 0;

      foreach (var item in Model.OrderBy(g => g.SortOrder))
      {
          cnt++;
          if (cnt > (currentPage*8 - 8) && cnt <= currentPage*8)
          {

%>
    
    <div class="galleryItem">
            <%if (Request.IsAuthenticated)
        {%>
        <div class="adminLinksPouUpContainer">
            <div class="adminLinksPouUpContainerRight">
                <div class="adminLinksPouUpContainerInner">
                    <%= Html.ActionLink("[IMAGE]", "Delete", "Gallery", new { area = "Admin", id = item.Id }, new { @class = "pictureLink delete", title = "Удалить галерею", onclick = "return confirm('Вы уверены что хотите удалить галерею?')" }).ToString().Replace("[IMAGE]", "")%>
                    <%= Html.ActionLink("[IMAGE]", "Edit", "Gallery", new { area = "Admin", id = item.Id }, new { @class = "pictureLink edit", title = "Редактировать галерею" }).ToString().Replace("[IMAGE]", "")%>
                </div>
            </div>
        </div>
        <div style="position:absolute">
            <%=item.SortOrder%>
        </div>
        <%
              }%>

              <%if(!string.IsNullOrEmpty( item.ImageSource))
{%>
         <%=Html.ActionLink("[IMAGE]", "Index", "Home", new {galleryId = item.Id}, null).ToString().Replace("[IMAGE]", "<img src=\"" + GraphicsHelper.GetCachedImage("~/Content/Photos", item.ImageSource,"galleryThumbnail") +"\">")%>
                  <%
}else
{
                    %>
                    <%=Html.ActionLink("[IMAGE]", "Index", "Home", new { galleryId = item.Id }, null).ToString().Replace("[IMAGE]", "<img src=\"" + GraphicsHelper.GetCachedImage("~/Content/Photos", "nophoto.gif", "galleryThumbnail") + "\">")%>
                    <%
}%>
         
        <div class="gallerySign">
            <%
                if (!string.IsNullOrEmpty(item.Description))
                {
            %>
            <%=Html.ActionLink(item.Description, "Index", "Home", new { galleryId = item.Id }, null)%>
            <%
                }
            %>
        </div>
    </div>
  
  <%
          }
      }%>


  
    


    
    <div class="clear"></div>
</div>
<%
  }%>

  <%
      if (Request.IsAuthenticated && ViewData["contentLevel"] != null && (int)ViewData["contentLevel"] == 1 && ViewData["galleryId"] == null)
      {%>
      <div class="clear"></div>
      <div id="addGalleryAdminLinkContainer">
      <%=Html.ActionLink("Добавить галерею", "Add", "Gallery", new {area = "Admin", id = ViewData["contentId"]}, new {@class = "adminLink"})%>
      </div>
    <%
      }%>