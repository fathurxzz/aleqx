<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Babich.Models.Gallery>>" %>

<%if(Model.Count()>8)
  {%>
<div id="paging">
    <%
      int currentPage = 1;
      if (ViewData["galleryPage"] != null)
      {
          currentPage = (int) ViewData["galleryPage"];
      }

      int page = 0;
      int cnt = 0;
      int pageCount = (int) (Model.Count()/8) + 1;
      foreach (var item in Model)
      {
          cnt++;
          if (cnt%8 == 0)
          {
              page++;
              if (page == currentPage)
              {
%>
    <%=page%>
    <span>..</span>
    <%
              }
              else
              {
%>
    <%=Html.ActionLink(page.ToString(), "Index", "Home",
                                                    new {id = ViewData["contentName"], galleryPage = page}, null)%>
    <span>..</span>
    <%
              }
          }
      }

      if (pageCount != currentPage)
      {%>
    <%=Html.ActionLink(pageCount.ToString(), "Index", "Home",
                                            new {id = ViewData["contentName"], galleryPage = pageCount}, null)%>
    <%
      }
      else
      {%>
    <%=pageCount%>
    <%
      }%>
</div>
<%
  }%>