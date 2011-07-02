<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    var menuItems = (IEnumerable<Babich.Models.Content>) ViewData["menuItems"];

    if (menuItems != null)
    {

        foreach (var item in menuItems)
        {
            if (item.Name == (string)ViewData["contentName"])
            {
%>
            <span id="menuItem<%=item.Id%>"></span>
            <%
            }
            else if(item.Name == (string)ViewData["parentContentName"])
            {
                %>
                <a id="menuItem<%=item.Id%>" class="subMenuSelected" href="/<%=item.Name%>"></a>
                <%
            }
            else
            {
%>
  <a id="menuItem<%=item.Id%>" href="/<%=item.Name%>"></a>
  <%
            }
        }
    }
%>
