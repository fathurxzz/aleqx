<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<ViaCon.Models.Content>>" %>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<%
    int level = Convert.ToInt32(ViewData["level"]);
    string marginLeft = level * 20 + "px";
%>


<table>
<tr>
    <td align="left">
        <% foreach (var item in Model){
               
               int? parentId=null;
               if(item.Parent!=null)
               parentId=item.Parent.Id;
               
               %>
        
       
        <table class="adminContentTable" style="margin-left:<%= marginLeft %>">
        <%if (level == 0 && ViewData["firstDisplayed"]==null){
              ViewData["firstDisplayed"] = true;
              %>
        <tr>
            <th>
                Идентификатор
            </th>
            <th>
                Заголовок
            </th>
        </tr>
         <%
      }
        %>
        
        <tr>
            <td style="display:none">
                <%= Html.Hidden("itemId_" + item.Id, item.Id)%>
            </td>
            <td><%=item.ContentId%></td>
            <td><%=item.Title%></td>
            
             <%if (level == 0)
               { %>
            <td>
                <%= Html.ActionLink("Добавить подпункт", "AddGalleryItem", new { parentId = item.Id })%>
            </td>
            
            <%} %>
            <td>
                <%=Html.ActionLink("Редактировать", "EditGalleryItem", new { id = item.Id, parentId = parentId})%>
            </td>
            <td>
                <%=Html.ActionLink("Просмотр галереи", "ManageGalleryPictures", new { id = item.Id })%>
            </td>
            <td>
                <%= Html.ActionLink("Удалить", "DeleteGalleryItem", new { id = item.Id }, new { onclick = "return confirm('Удалить этот пункт?')" })%>
            </td>
        </tr>
        </table>
        <%
            if (item.Children.Count > 0)
                Html.RenderAction<ViaCon.Controllers.AdminController>(a => a.GalleryList(item.Id, level + 1));
           } %>
    </td>
</tr>
</table>
    


