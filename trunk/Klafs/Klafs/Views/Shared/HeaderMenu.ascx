<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    List<Klafs.Models.Content> headerMenuItems = null;
    if (ViewData["headerMenuItems"] != null)
        headerMenuItems = (List<Klafs.Models.Content>)ViewData["headerMenuItems"];

    if (headerMenuItems != null && headerMenuItems.Count() > 0)
    {
%>
<div id="headerMenu">
    &nbsp;:&nbsp;:&nbsp;:&nbsp;:&nbsp;:&nbsp;:&nbsp;
    <%
        foreach (var item in headerMenuItems.Where(c=>c.ContentType==0))
        {
    %>
    <a href="/<%=item.Name %>">
        <%=item.MenuTitle%></a> &nbsp;:&nbsp;:&nbsp;:&nbsp;:&nbsp;:&nbsp;:&nbsp;
    <%
        }
    %>

<div id="rightHeaderMenuItem">
    <%
        foreach (var item in headerMenuItems.Where(c=>c.ContentType==3))
        {
    %>

    <a class="hobbyLink" href="/<%=item.Name %>"><img src="../../Content/img/heart.gif" alt="" /><%=item.MenuTitle%></a>
    
        <%
        }
    %>

</div>
</div>

<%
}%>