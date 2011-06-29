<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


<%
    var subMenuItems = (IEnumerable<Babich.Models.Content>)ViewData["subMenuItems"];

    if (subMenuItems != null)
    {

        foreach (var item in subMenuItems)
        {
            if (item.Name == (string) ViewData["contentName"])
            {
%>
            <span><%=item.MenuTitle%></span>
            <%
            }
            else
            {
%>
    <span>
  <a href="/<%=item.Name%>"><%=item.MenuTitle%></a>
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
  <%=Html.ActionLink("Добавить подраздел","Add","Content",new{id=ViewData["contentId"],area="Admin"},new{@class="adminLink"}) %>
  </div>
<%
  }%>

  <div class="clear"></div>
  


