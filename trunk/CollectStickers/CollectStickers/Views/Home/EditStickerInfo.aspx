<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<CollectStickers.Models.StickerPresentation>>" %>
<%@ Import Namespace="CollectStickers.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EditStickerInfo
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    var enablesNeed = {};
    var enablesFree = {};

    function updateEnablesNeed(check, id) {
        if (check.checked) {
            enablesNeed[id] = true;
        }
        else {
            enablesNeed[id] = false;
        }
    }

    function updateEnablesFree(check, id) {
        if (check.checked) {
            enablesFree[id] = true;
        }
        else {
            enablesFree[id] = false;
        }
    }

    function collectCategoryChanges() {

        var enablitiesNeed = $get("enablitiesNeed");
        enablitiesNeed.value = Sys.Serialization.JavaScriptSerializer.serialize(enablesNeed);

        var enablitiesFree = $get("enablitiesFree");
        enablitiesFree.value = Sys.Serialization.JavaScriptSerializer.serialize(enablesFree);

        return true;
    }
    
</script>
    <h2>UserPage</h2>


 <%using (Html.BeginForm("UpdateStickers", "Home", FormMethod.Post))
   {
     %>

    <table class="stickerTable">
        <tr>
            <th>
                Номер стикера
            </th>
            <th>
                Мне необходим данный стикер
            </th>
            <th>
                У меня есть такой лишний
            </th>
        </tr>



    <% 
     bool existitem = false;
     for (int i = 1; i <= 564; i++)
     {
         existitem = false;
         if (Model != null)
         {
             foreach (var item in Model)
             {
                 if (item.Number == i)
                 {
                     existitem = true;
                %>
                <tr>
                    <td>
                        <%= Html.Encode(item.Number)%>
                    </td>
                    <td>
                        <%= Html.CheckBox("chNeed_" + i, item.isNeed, new { onblur = "updateEnablesNeed(this, " + i + ")" })%>
                    </td>
                    <td>
                        <%= Html.CheckBox("chFree_" + i, item.isFree, new { onblur = "updateEnablesFree(this, " + i + ")" })%>
                    </td>
                </tr>
                <%
     }

             }
         }

         if (!existitem)
         {
                %>
                <tr>
                    <td>
                        <%= Html.Encode(i)%>
                    </td>
                    <td>
                        <%= Html.CheckBox("chNeed_" + i, false, new { onblur = "updateEnablesNeed(this, " + i + ")" })%>
                    </td>
                    <td>
                        <%= Html.CheckBox("chFree_" + i, false, new { onblur = "updateEnablesFree(this, " + i + ")" })%>
                    </td>
                </tr>
                <%
     }
     }
        %>
    </table>
    
    <%= Html.Hidden("enablitiesNeed")%>
    <%= Html.Hidden("enablitiesFree")%>
    <input type="submit" value="<%= Html.ResourceString("SaveChanges") %>" onclick="return collectCategoryChanges();" />
    
<%} %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="leftMenuContent" runat="server">
</asp:Content>

