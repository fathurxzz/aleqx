<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>





<%if (ViewData["contentType"] != null && ((int)ViewData["contentType"] == 3 || (int)ViewData["contentType"] == 5))
  {%>

<%
      List<Klafs.Models.Content> subMenuItems = null;
      if (ViewData["subMenuItems"] != null)
          subMenuItems = (List<Klafs.Models.Content>) ViewData["subMenuItems"];
      if (subMenuItems != null && subMenuItems.Count() > 0)
      {

%>

<div class="subMenuContainer">
<table>
<tr>
    <td class="subMenuLeftSide"></td>
    <td class="subMenuCenter">
    <%
          foreach (var subMenuItem in subMenuItems)
          {
              if ((string)ViewData["contentName"] != subMenuItem.Name)
          {%>

        <a href="/<%=subMenuItem.Name%>">
            <%=subMenuItem.MenuTitle%></a> 
            <%
            }
          else
{%>
<span>
<%=subMenuItem.MenuTitle%>
</span>
<%
}
          }
%>
    
    </td>
    <td class="subMenuRightSide"></td>
</tr>
</table>
</div>
<%
      }
  }%>