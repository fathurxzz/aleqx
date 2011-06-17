<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    List<Klafs.Models.Content> headerMenuItems = null;
    if (ViewData["headerMenuItems"] != null)
        headerMenuItems = (List<Klafs.Models.Content>)ViewData["headerMenuItems"];

    if (headerMenuItems != null && headerMenuItems.Count() > 0)
    {
%>
<div id="headerMenu">
    <div>
        &nbsp;:&nbsp;:&nbsp;:&nbsp;:&nbsp;:&nbsp;:&nbsp;
        <%
            foreach (var item in headerMenuItems.Where(c => c.ContentType == 0))
            {
        %>

        <%if ((string)ViewData["contentName"] != item.Name)
          {%>

        <a href="/<%=item.Name%>">
            <%=item.MenuTitle%></a> 
            <%
            }
          else
{%>
<span>
<%=item.MenuTitle%>
</span>
<%
}%>
            
            
            &nbsp;:&nbsp;:&nbsp;:&nbsp;:&nbsp;:&nbsp;:&nbsp;
        <%
            }
        %>
    </div>
    <div id="rightHeaderMenuItem">
        <%
            foreach (var item in headerMenuItems.Where(c => c.ContentType == 3))
            {
        %>

        
        <%if ((string)ViewData["contentName"] != item.Name)
          {%>
        <a class="hobbyLink" href="/<%=item.Name %>">
            <%=item.MenuTitle%>
        </a>
        <%
            }
          else
{%>
<span class="hobbyLink">
<%=item.MenuTitle%>
</span>
<%
}%>
        <%
            }
        %>
    </div>
</div>
<%
}%>
