<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<eShop.Models.CategoryProperties>>" %>
<%@ Import Namespace="eShop.Helpers" %>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
    <table>
        <tr>
            <td align="left">

    <% foreach (var item in Model)
       { %>
    <table class="adminTable">
        <%if (ViewData["firstDisplayed"] == null)
          {
              ViewData["firstDisplayed"] = true;
              %>
        <tr>
            <th>
                <%=Html.ResourceString("Name")%>
            </th>
            <th>
                Unit
            </th>
            <th>
                IsMainProperty
            </th>
            <th>
                
            </th>
        </tr>

     <%
        }
        %>

    
        <tr>
            <td style="display:none">
                <%= Html.Hidden("itemId_" + item.Id, item.Id)%>
            </td>
            <td>
                <%= Html.TextBox("item_" + item.Id, item.Name, new { onblur = "updateNames(this.value, " + item.Id + ")", style = "width:200px;" })%>
            </td>
            <td>
                <%= Html.TextBox("unit_" + item.Id, item.Unit)%>
            </td>
            <td>
                <%= Html.CheckBox("Enabled_" + item.Id, item.IsMainProperty, new { onblur = "updateEnables(this, " + item.Id + ")" })%>
            </td>
            <td>
                <%= Html.ActionLink(Html.ResourceString("Delete"), "DeleteCategoryProperty", new { id = item.Id }, new { onclick = "return confirm('" + Html.ResourceString("DeleteCategoryConfirmation") + "')" })%>
            </td>
        </tr>
    </table>
    <% } %>

       </td>
</tr>
</table>

