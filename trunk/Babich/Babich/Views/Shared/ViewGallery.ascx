<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Babich.Models.Gallery>" %>
<%@ Import Namespace="Dev.Mvc.Helpers" %>
<%if (ViewData["galleryId"]!=null)
  {%>


  <%if (Model.GalleryItems.Count > 0)
{%>

  <script type="text/javascript">

      $(function () {
          $('#galleryPreviews').jcarousel({ 'scroll': 1 });
          $(".fancy").fancybox({ hideOnContentClick: true, showCloseButton: false, cyclic: true, showNavArrows: false, padding: 0, margin: 0, centerOnScroll: true });
      });

      function setImage(path) {
          $("#pictureContainer").attr("src", "/ImageCache/mainView/" + path);
          $("#pictureLink").attr("href", "/Content/Photos/" + path);
      }
      
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
    <ul id="galleryPreviews" class="jcarousel-skin-tango">
    
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

    <%=Html.ActionLink("[IMAGE]", "DeletePhoto", "Gallery", new { area = "Admin", id = item.Id },
                                      new
                                          {
                                              @class = "remove",
                                              title = "Удалить фотографию",
                                              onclick = "return confirm('Вы уверены что хотите удалить фотографию?')"
                                          }).ToString().Replace("[IMAGE]","")%>

        
        
            

            <%=Html.ActionLink("[IMAGE]", "SetPreviewPicture", "Gallery", new { area = "Admin", id = item.Id },
                                      new
                                          {
                                              @class = "setDefault "+(Model.ImageSource==item.ImageSource?"selected":""),
                                              title = "Установить фотографию на превью галереи",
                                              onclick = "return confirm('Вы уверены что хотите установить данную фотографию на превью галереи?')"
                                          }).ToString().Replace("[IMAGE]", "")%>

        
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
    &lt;&lt; 
    
  <%if (ViewData["galleryPage"] != null)
  {%>
    <%=Html.ResourceActionLink("Back", "Index", "Home", new { id = ViewData["contentName"], galleryPage = ViewData["galleryPage"] }, null)%>
    <%
  }
  else
{%>
<%=Html.ResourceActionLink("Back", "Index", "Home", new { id = ViewData["contentName"] }, null)%>
<%
}%>
</div>

<%if(Request.IsAuthenticated){ %>
<div class="adminLinkContainer">
<%=Html.ActionLink("Добавить фото", "AddPhoto", "Gallery", new { area = "Admin", id = ViewData["galleryId"] }, new { @class = "adminLink" })%>
</div>

<%} %>

<%
  }%>