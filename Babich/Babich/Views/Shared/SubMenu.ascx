<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Babich" %>


<%
    var subMenuItems = (IEnumerable<Babich.Models.Content>)ViewData["subMenuItems"];

    if (subMenuItems != null)
    {

        foreach (var item in subMenuItems)
        {
            if (item.Name == (string) ViewData["contentName"])
            {
%>
            <span>
            <%if (SiteSettings.Language == Language.En)
              {%>
            <%=item.TitleEng%>
            <%
                }
              else
{%>
           <%=item.Title%>
<%
}%>
            </span>
            <%
            }
            else
            {
%>
    <span>
  

  <%if (SiteSettings.Language == Language.En)
              {%>
            <a href="/<%=item.Name%>"><%=item.TitleEng%></a>
            <%
                }
              else
{%>
           <a href="/<%=item.Name%>"><%=item.Title%></a>
<%
}%>


  </span>
  <%
            }
        }
    }
%>

<%if (Request.IsAuthenticated)
  {%>
  <div class="clear"></div>
  <div id="subMenuAdminLinkContainer">
  <%=Html.ActionLink("Редактировать", "Edit", "Content", new { id = ViewData["contentId"], area = "Admin" }, new { @class = "adminLink" })%>
  
  <%if (ViewData["contentId"].ToString() != "1" && ViewData["contentLevel"].ToString()!="1")
{%>
  <%=Html.ActionLink("Добавить подраздел", "Add", "Content",
                                      new {id = ViewData["contentId"], area = "Admin"}, new {@class = "adminLink"})%>
  <%
}%>
  </div>
<%
  }%>

  <div class="clear"></div>
  


