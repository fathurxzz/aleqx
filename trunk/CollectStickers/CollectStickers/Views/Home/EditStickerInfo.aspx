<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<CollectStickers.Models.StickerPresentation>>" %>
<%@ Import Namespace="CollectStickers.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EditStickerInfo
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    var enables = {};

    function collectCategoryChanges() {
        var enablities = $get("enablities");
        enablities.value = Sys.Serialization.JavaScriptSerializer.serialize(enables);
        return true;
    }


    function changeCellStatus(itemId, id) {
        var item = $get(itemId);
        
        if (item.value == 0) {
            item.value = 2
            item.style.backgroundColor = "#4f7556";
            enables[id] = 2;
        }
        else if (item.value == 2) {
            item.value = 1
            item.style.backgroundColor = "#df8d1f";
            enables[id] = 1;
        }
        else {
            item.value = 0
            item.style.backgroundColor = "#ffffff";
            enables[id] = 0;
        }
    }
    
    
</script>
    <h2>Моя коллекция</h2>


<%
    bool existitem;
     %>
     
 <%using (Html.BeginForm("UpdateStickers", "Home", FormMethod.Post))
   {
     %>     
<table class="stickerTable">
<tr>
<%
    int maxcount = 564;
    for (int i = 1; i <= 570; i++)
    {
        existitem = false;
        if (i <= maxcount)
        {
            if (i % 30 == 1)
            {
             %>
             </tr>
             <tr>
             <%
            }

            if (Model != null)
            {   
                foreach (var item in Model)
                {
                    if (item.Number == i)
                    {
                        
                        if (item.isNeed)
                        {
                            existitem = true;
                            Response.Write("<td value=\"1\" style=\"background-color:#df8d1f\" id=\"item_" + i + "\" onclick=\"changeCellStatus(this.id," + i + ")\">" + i + "</td>");
                        }
                        else if (item.isFree)
                        {
                            existitem = true;
                            Response.Write("<td value=\"2\" style=\"background-color:#4f7556\" id=\"item_" + i + "\" onclick=\"changeCellStatus(this.id," + i + ")\">" + i + "</td>"); 
                        }
                      
                    }
                }
            }

            if (!existitem)
            {
                Response.Write("<td value=\"0\" style=\"background-color:#ffffff\" id=\"item_" + i + "\" onclick=\"changeCellStatus(this.id," + i + ")\">" + i + "</td>"); 
            }
        }
    }
%>
</tr>
</table>
<br />
    <%= Html.Hidden("enablities")%>
    <input type="submit" value="<%= Html.ResourceString("SaveChanges") %>" onclick="return collectCategoryChanges();" />
    
<%} %>

<br />

<div>

<table>
<tr>
    <td style="width:15px; height:13px; background-color:#df8d1f"></td>
    <td align="left"> - Ищу</td>
</tr>
<tr>
    <td style="width:15px; height:13px; background-color:#4f7556"></td>
    <td align="left"> - Готов обменять</td>
</tr>
</table>


<br />

Собрано - <%=ViewData["collected"]%> (<%=ViewData["collectedPercent"]%>%)
<br />
Осталось собрать - <%=ViewData["needed"]%>
<br />
Лишних - <%=ViewData["free"]%>

</div>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="leftMenuContent" runat="server">
</asp:Content>

