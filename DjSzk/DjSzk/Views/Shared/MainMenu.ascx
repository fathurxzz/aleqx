<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    var menuItems = (IEnumerable<DjSzk.Models.Content>) ViewData["menuItems"];
    var contentName = (string) ViewData["contentName"];
    foreach (var item in menuItems.OrderBy(c=>c.SortOrder))
    {
        %>
            <div class="<%=item.ClassName%>">
            
            <%if (item.Name == contentName)
              { %>
              <span><%=item.MenuTitle%></span>
            <% }
              else
{ %>
            <a href="/<%=item.Name%>"><%=item.MenuTitle%></a>
            <% } %>
            </div>
        <%
    }
%>
<!--
<div class="read">
    <a href="#">почитать</a>
</div>
<div class="hear">
    <a href="#">послушать</a>
</div>
<div class="connect">
    <a href="#">связаться</a>
</div>
-->