<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Babich" %>
<%
    var subMenuItems = (IEnumerable<Babich.Models.Content>)ViewData["subMenuItems"];

    if (subMenuItems != null)
    {
        foreach (var item in subMenuItems)
        {
%>
<span>
    <%
        if (item.Name == (string)ViewData["contentName"] && ViewData["galleryId"] == null)
        {
            if (SiteSettings.Language == Language.En)
            {%>
    <%=item.TitleEng%>
    <%
}
                    else
                    {%>
    <%=item.Title%>
    <%
        }
                }
                else
                {
    %>
    <%if (SiteSettings.Language == Language.En)
      {%>
    <a href="/<%=item.Name%>">
        <%=item.TitleEng%></a>
    <%
        }
      else
      {%>
    <a href="/<%=item.Name%>">
        <%=item.Title%></a>
    <%
}
                    }

                if (Request.IsAuthenticated)
                {
    %>
    <%=Html.ActionLink("[IMAGE]", "Delete", "Content", new { area = "Admin", id = item.Id },
                                      new
                                          {
                                              @class = "removeContent",
                                              title = "Удалить раздел",
                                              onclick = "return confirm('Вы уверены что хотите удалить раздел?')"
                                          }).ToString().Replace("[IMAGE]","")%>
    <%
        }

    %>
</span>
<%
}
    }
%>
<%if (Request.IsAuthenticated)
  {%>
<div class="clear">
</div>
<div id="subMenuAdminLinkContainer">
    <%=Html.ActionLink("Редактировать", "Edit", "Content", new { id = ViewData["contentId"], area = "Admin" }, new { @class = "adminLink" })%>
    <%if (ViewData["contentId"].ToString() != "1" && ViewData["contentLevel"].ToString() != "1")
      {%>
    <%=Html.ActionLink("Добавить подраздел", "Add", "Content",
                                      new {id = ViewData["contentId"], area = "Admin"}, new {@class = "adminLink"})%>
    <%
        }%>
</div>
<%
    }%>
<!--<div class="clear">
</div>-->
