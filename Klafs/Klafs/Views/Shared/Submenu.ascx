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
    
    <ul>
    <%
        foreach (var subMenuItem in subMenuItems)
        {
           %>


           <li class="submenuItem">
           <%
            if ((string)ViewData["contentName"] != subMenuItem.Name)
            {%>
    <a href="/<%=subMenuItem.Name%>"><%=subMenuItem.MenuTitle%></a>

    

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
    <%=Html.ActionLink("[IMAGE]", "AddPhoto", "Content", new { area = "Admin", id = subMenuItem.Id }, new { @class = "pictureLink add gray", title = "Добавить фотографию" }).ToString().Replace("[IMAGE]", "")%>
    <%=Html.ActionLink("[IMAGE]", "EditContent", "Content", new { area = "Admin", id = subMenuItem.Id }, new { @class = "pictureLink edit gray", title = "Редактировать" }).ToString().Replace("[IMAGE]", "")%>
    <%=Html.ActionLink("[IMAGE]", "DeleteContent", "Content", new { area = "Admin", id = subMenuItem.Id }, new { @class = "pictureLink delete gray", title = "Удалить", onclick = "return confirm('Вы действительно хотите удалить раздел?')" }).ToString().Replace("[IMAGE]", "")%>
    </div>
    
    <%}
        %></li><%
        }%>


        </ul>
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










