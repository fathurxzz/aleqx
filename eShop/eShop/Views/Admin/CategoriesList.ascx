<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<eShop.Models.Category>>" %>
<%@ Import Namespace="eShop.Helpers" %>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<%
    int level = Convert.ToInt32(ViewData["level"]);
    string marginLeft = level * 20 + "px";
%>

<table>
<tr>
    <td align="left">

<% foreach (var item in Model) { %>
    <table class="adminTable" style="margin-left:<%= marginLeft %>">
    <%if (level == 0 && ViewData["firstDisplayed"]==null){
              ViewData["firstDisplayed"] = true;
              %>
        <tr>
            <th>
                Name
            </th>
            <th>
                Enabled
            </th>
        </tr>

     <%
      }
        %>
        <tr>
            <td style="display:none">
                <%= Html.Hidden("itemId_" + item.Id, item.Id)%>
            </td>
            <td style="width:200px">
                <%= Html.Encode(item.Name) %>
            </td>
            <td align="center" style="width:40px;">
                <%= Html.CheckBox("Enabled_" + item.Id, item.Enabled, new { onblur = "updateEnables(this, " + item.Id + ")" })%>
            </td>
            <td>
                <%= Html.ActionLink(Html.ResourceString("Delete"), "DeleteCategory", new { id = item.Id }, new { onclick = "return confirm('" + Html.ResourceString("DeleteCategoryConfirmation") + "')" })%>
            </td>
        </tr>
    </table>
    <% 
       if (item.Categories.Count > 0)
           Html.RenderAction<eShop.Controllers.AdminController>(a => a.CategoriesList(item.Id, level + 1));
       } %>
    </td>
</tr>
</table>
    



