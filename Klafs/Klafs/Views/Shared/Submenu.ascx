<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    List<Klafs.Models.Content> subMenuItems = null;
    if (ViewData["subMenuItems"] != null)
        subMenuItems = (List<Klafs.Models.Content>)ViewData["subMenuItems"];
    if (subMenuItems != null && subMenuItems.Count() > 0)
    {
%>
<center>
<table style="width:925px;">
<tr>
<td>
<table class="subMenuTable">
<tr>
<td class="subMenuLeftSide"></td>
<td class="subMenuCenter">
    
    <%
        foreach (var subMenuItem in subMenuItems)
        {
            if (Request.IsAuthenticated)
            {
            %>
            <div><span>
            <%}
            else
            { %>
            <span>
            <%
        }
            if ((string)ViewData["contentName"] != subMenuItem.Name)
            {%>
    <a href="/<%=subMenuItem.Name%>">
        <%=subMenuItem.MenuTitle%></a>
    <%
        }
            else
            {%>
    <span class="sMenuItem">
        <%=subMenuItem.MenuTitle%>
        </span>
    
    <%}
            if (Request.IsAuthenticated)
            {%>
    <br />
    <div class="subMenuAdminLinkContainer">
     <%=Html.ActionLink("Добавить фото", "AddPhoto", "Content", new { area = "Admin", id = subMenuItem.Id }, new { @class = "adminLink" })%><br />
    <%=Html.ActionLink("Редактировать", "EditContent", "Content", new { area = "Admin", id = subMenuItem.Id }, new { @class = "adminLink" })%><br />
    <%=Html.ActionLink("Удалить", "DeleteContent", "Content", new { area = "Admin", id = subMenuItem.Id }, new { @class = "adminLink", onclick = "return confirm('Вы действительно хотите удалить раздел?')" })%>
    </div>
    <%}
            if (Request.IsAuthenticated)
            {%></span></div><%}
            else
            { %>
            
    
    </span>
    <%}
        }%>

    </td>
<td class="subMenuRightSide"></td>
</tr>
</table></td>
</tr>
</table>
</center>

<%
    }
  %>










