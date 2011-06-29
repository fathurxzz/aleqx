<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Babich.Models.Gallery>>" %>
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
            if (cnt > (currentPage * 8-8) && cnt <= currentPage * 8)
            {

%>
    
    <div class="galleryItem">
    <%
                if (Request.IsAuthenticated)
                {%>
    <%=item.SortOrder%>
    <%
                }%>

                
        <a href="#">
        <img src="../../Content/img/gallery_preview.gif" alt="" />
        </a>
        <div class="gallerySign">
            <%=item.Description%>
            Житловий комплекс по вул. Лук'янівській, 77
        </div>
    </div>
  
  <%
        }
        }%>


  
    


    <%if (Request.IsAuthenticated)
      {%>
      <div class="clear"></div>
      <div id="addGalleryAdminLinkContainer">
      <%=Html.ActionLink("Добавить галерею","Add","Gallery",new{area="Admin",id=ViewData["contentId"]},new{@class="adminLink"})%>
      </div>

    <%
      }%>
    <div class="clear"></div>
</div>
