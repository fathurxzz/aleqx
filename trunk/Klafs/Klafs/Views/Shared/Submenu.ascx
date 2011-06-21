<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    List<Klafs.Models.Content> subMenuItems = null;
    if (ViewData["subMenuItems"] != null)
        subMenuItems = (List<Klafs.Models.Content>)ViewData["subMenuItems"];
    if (subMenuItems != null && subMenuItems.Count() > 0)
    {
%>
<center>
<table class="subMenuTable">
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
    <%}
    if (Request.IsAuthenticated)
      {%>
    <br />
    <%=Html.ActionLink("Редактировать", "EditContent", "Content", new { area = "Admin", id = subMenuItem.Id }, new { @class = "adminLink" })%>
    <%=Html.ActionLink("Удалить", "DeleteContent", "Content", new { area = "Admin", id = subMenuItem.Id }, new { @class = "adminLink", onclick = "return confirm('Вы действительно хотите удалить раздел?')" })%>|
    <%}}if (Request.IsAuthenticated)
      {%>
    <%=Html.ActionLink("Добавить", "AddContent", "Content", new { area = "Admin" }, new { @class = "adminLink" })%>
    <%} %>

    </td>
<td class="subMenuRightSide"></td>
</tr>
</table>
</center>

<%
    }
  %>










