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
                ContentId
            </th>
            <th>
                Title
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
                <%= Html.ActionLink("Добавить подпункт", "AddContentItem", new { parentId = item.Id })%>
            </td>
            
            <%}
               else if(level==1)
               { %>
               
               <td>
                <%=Html.ActionLink("Добавить подпункт", "AddContentItem", new {parentId=item.Id, horisontal=true })%>
               </td>
            
            <%} %>
            <td>
                <%= 
                    Html.ActionLink("Редактировать", "EditContentItem", new { id = item.Id, parentId = parentId, horisontal=level==2 })
                    %>
            </td>
            <td>
                <%= Html.ActionLink("Удалить", "DeleteContentItem", new { id = item.Id }, new { onclick = "return confirm('Удалить этот пункт?')" })%>
            </td>
        </tr>
        </table>
        <%
            if (item.Children.Count > 0)
                Html.RenderAction<ViaCon.Controllers.AdminController>(a => a.ContentList(item.Id, level + 1));
               
               
           } %>
    </td>
</tr>
</table>
    
