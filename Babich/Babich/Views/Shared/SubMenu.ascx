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
<!--
<span><a href="#">Житлові будівлі</a> </span>
<span><a href="#">Отелі</a> </span>
<span><a href="#">Котеджі</a> </span>
<span><a href="#">Торговельно-розважальні комплекси</a> </span>
<span><a href="#">Ресторанні комплекси</a></span>
<span><a href="#">Офісні споруди</a> </span>
<span><a href="#">Конкурсні проекти</a> </span>
<span><a href="#">Громадські споруди</a> </span>
<span><a href="#">Сакральна архітектура</a> </span>
<span><a href="#">Приватні будинки</a></span>
<span><a href="#">Ландшафтна архітектура</a></span>
-->

<%if (Request.IsAuthenticated)
  {%>
  <div class="clear"></div>
  <div id="subMenuAdminLinkContainer">
  <%=Html.ActionLink("Редактировать", "Edit", "Content", new { id = ViewData["contentId"], area = "Admin" }, new { @class = "adminLink" })%>
  <%=Html.ActionLink("Добавить подраздел","Add","Content",new{id=ViewData["contentId"],area="Admin"},new{@class="adminLink"}) %>
  </div>
<%
  }%>

  <div class="clear"></div>
  


