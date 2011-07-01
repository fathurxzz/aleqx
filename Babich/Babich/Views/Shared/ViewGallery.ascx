<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Babich.Models.Gallery>" %>
<%@ Import Namespace="Dev.Mvc.Helpers" %>
<%if (ViewData["galleryId"]!=null)
  {%>


  <%if (Model.GalleryItems.Count > 0)
{%>

  <script type="text/javascript">

      $(function () {
          $(".fancy").fancybox({ hideOnContentClick: true, showCloseButton: false, cyclic: true, showNavArrows: false, padding: 0, margin: 0, centerOnScroll: true });
      });

      $(function () {
      /*
          jQuery('#mycarousel').jcarousel({ 'scroll': 1 });
          $("a#pictureLink").fancybox({
              'titleShow': false,
              'transitionIn': 'none',
              'transitionOut': 'none',
              'hideOnOverlayClick': true,
              'hideOnContentClick': true,
              'enableEscapeButton': true,
              'showCloseButton': false
          });*/
      });

      function setImage(path) {
          $("#pictureContainer").attr("src", "/ImageCache/mainView/" + path);
          $("#pictureLink").attr("href", "/Content/Photos/" + path);
      }

      $(function () {
      
          $(".carouselItem").mousemove(function () {
              $(this).children('div').addClass("show");
          });
          $(".carouselItem").mouseleave(function () {
              $(this).children('div').removeClass("show");
          });
      });

</script>

<%
}%>








<div id="galleryViewerContainer">
    <div id="galleryViewer">
     <%if (Model.GalleryItems.Count > 0)
{%>

        <a href="#" id="pictureLink" class="fancy">
           <img src="" alt="" id="pictureContainer" />
        </a>
        <%
}%>
    </div>
    <ul id="galleryPreviews">
    
    <%
      var cnt = 0;
      foreach (var item in Model.GalleryItems.OrderBy(gi=>gi.Id))
{
    if (cnt == 0)
    {
        Response.Write(
            "<script type=\"text/javascript\">" +
            "$(\"#pictureContainer\").attr(\"src\", \"" + GraphicsHelper.GetCachedImage("~/Content/Photos", item.ImageSource, "mainView") + "\");" +
            "$(\"#pictureLink\").attr(\"href\", \"/Content/Photos/" + item.ImageSource + "\");" +
            "</script>");
    }
    cnt++;
  %>
  
  <li>
    <div class="carouselItem" style="cursor:pointer" onclick="setImage('<%=item.ImageSource%>')">
        
            <%if (Request.IsAuthenticated)
    {%>
        <div class="adminLinksPouUpContainer">
        
            <%=Html.ActionLink("[IMAGE]", "DeletePhoto", "Gallery", new { area = "Admin", id = item.Id },
                                      new
                                          {
                                              @class = "pictureLink delete",
                                              title = "Удалить фотографию",
                                              onclick = "return confirm('Вы уверены что хотите удалить фотографию?')"
                                          }).ToString().Replace("[IMAGE]","")%>

            <%=Html.ActionLink("[IMAGE]", "SetPreviewPicture", "Gallery", new { area = "Admin", id = item.Id },
                                      new
                                          {
                                              @class = "pictureLink delete",
                                              title = "Установить фотографию на превью галереи",
                                              onclick = "return confirm('Вы уверены что хотите установить данную фотографию на превью галереи?')"
                                          }).ToString().Replace("[IMAGE]", "")%>

        </div>
        <%
}%>
    <%=Html.CachedImage("~/Content/Photos/", item.ImageSource, "thumbnail", item.ImageSource)%>
    </div>
  </li>

  <%
}
       %>

    </ul>
</div>





<div class="clear">
</div>
<div id="backToCatalogue">
    &lt;&lt; <a href="#">До каталогу</a>
</div>



<%if(Request.IsAuthenticated){ %>

<div class="adminLinkContainer">
<%=Html.ActionLink("Добавить фото", "AddPhoto", "Gallery", new { area = "Admin", id = ViewData["galleryId"] }, new { @class = "adminLink" })%>
</div>

<%} %>

<%
  }%>