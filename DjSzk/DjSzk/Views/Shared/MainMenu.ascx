<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    var menuItems = (IEnumerable<DjSzk.Models.Content>) ViewData["menuItems"];
    var contentName = (string) ViewData["contentName"];
    foreach (var item in menuItems.OrderBy(c=>c.SortOrder))
    {
        %>
           
            
            <%if (item.Name == contentName)
              { %>
              <span class="<%=item.ClassName%>" ><%=item.MenuTitle%></span>
            <% }
              else
{ %>
            <a class="<%=item.ClassName%>" href="/<%=item.Name%>"><%=item.MenuTitle%></a>
            <% } %>
            
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