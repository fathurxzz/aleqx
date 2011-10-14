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
        &nbsp;:&nbsp;:&nbsp;:&nbsp;:&nbsp;
        <%
            foreach (var item in headerMenuItems.Where(c => c.ContentType == 2))
            {
        %>
        <%if ((string)ViewData["contentName"] == item.Name || (string)ViewData["parentContentName"] == item.Name)
          {%>
        <span>
            <%=item.Title%>
        </span>
        <%
            }
          else
          {%>
        <a href="/<%=item.Name%>">
            <%=item.Title%></a>
        <%
            }%>

            <%if (Request.IsAuthenticated)
              { %>
              <span class="sortOrder"><%=item.SortOrder %></span>
            <%} %>

        &nbsp;:&nbsp;:&nbsp;:&nbsp;:&nbsp;
        <%
            }
        %>
    </div>
    <div id="rightHeaderMenuItem">
        <%
            foreach (var item in headerMenuItems.Where(c => c.ContentType == 3))
            {
        %>
        <%if ((string)ViewData["contentName"] == item.Name/* || (string)ViewData["parentContentName"] == item.Name*/)
          {%>
        <span class="hobbyLink">
            <%=item.Title%>
        </span>
        <%
            }
          else
          {%>
        <a class="hobbyLink" href="/<%=item.Name%>">
            <%=item.Title%>
        </a>
        <%
            }%>
        <%
            }
        %>
    </div>
</div>
<%
}%>
