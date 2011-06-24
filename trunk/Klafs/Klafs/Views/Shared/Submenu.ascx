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
<td align="center">




<table class="subMenuTable" cellpadding="0" cellspacing="0">
<tr>
    <td class="topLeft"></td>
    <td class="top"></td>
    <td class="topRight"></td>
</tr>
<tr>
    <td class="left"></td>
    <td class="center">
    
    
    <%
        foreach (var subMenuItem in subMenuItems)
        {
           %>
           <div class="submenuItem">
           <%
            if ((string)ViewData["contentName"] != subMenuItem.Name)
            {%>
    <a href="/<%=subMenuItem.Name%>">  <%=subMenuItem.MenuTitle%></a>

    

    <%
        }
            else
            {%>
   
        <%=subMenuItem.MenuTitle%>
        
    
    <%}
            if (Request.IsAuthenticated)
            {%>
    
    <br />
    <div class="adminSubMenuLinksContainer">

    <%=Html.ActionLink(".", "AddPhoto", "Content", new { area = "Admin", id = subMenuItem.Id }, new { @class = "adminLink pictureLink" })%>
    <%=Html.ActionLink(".", "EditContent", "Content", new { area = "Admin", id = subMenuItem.Id }, new { @class = "adminLink pictureLink" })%>
    <%=Html.ActionLink(".", "DeleteContent", "Content", new { area = "Admin", id = subMenuItem.Id }, new { @class = "adminLink pictureLink", onclick = "return confirm('Вы действительно хотите удалить раздел?')" })%>
    </div>
    
    <%}
        %></div><%
        }%>



    </td>
    <td class="right"></td>
</tr>
<tr>
    <td class="bottomLeft"></td>
    <td class="bottom"></td>
    <td class="bottomRight"></td>
</tr>
</table>







</td>
</tr>
</table>
</center>

<%
    }
  %>










