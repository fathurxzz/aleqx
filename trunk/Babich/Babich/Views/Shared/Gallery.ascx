<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Babich.Models.Gallery>>" %>

<%if (Model.Count() > 0 && ViewData["galleryId"]==null)
  {%>

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
    <%
              if (Request.IsAuthenticated)
              {%>
    <%=item.SortOrder%>
    <%
              }%>

         <%=Html.ActionLink("[IMAGE]", "Index", "Home", new { galleryId = item.Id }, null).ToString().Replace("[IMAGE]", "<img src=\"../../Content/img/gallery_preview.gif\"/>")%>
         
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