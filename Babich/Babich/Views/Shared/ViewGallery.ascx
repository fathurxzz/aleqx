<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Babich.Models.Gallery>" %>
<%@ Import Namespace="Dev.Mvc.Helpers" %>
<%if (ViewData["galleryId"]!=null)
  {%>




  <script type="text/javascript">
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
</script>









<div id="galleryViewerContainer">
    <div id="galleryViewer">
        <a href="#" id="pictureLink">
           <img src="" alt="" id="pictureContainer" />
        </a>
    </div>
    <ul id="galleryPreviews">
    
    <%
      var cnt = 0;
      foreach (var item in Model.GalleryItems)
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