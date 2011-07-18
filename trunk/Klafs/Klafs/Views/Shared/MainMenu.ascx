<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    List<Klafs.Models.Content> contentItems = null;
    if (ViewData["contentItems"] != null)
        contentItems = (List<Klafs.Models.Content>)ViewData["contentItems"];

    if (contentItems != null && contentItems.Count() > 0)
    {
%>
<div id="mainMenu">

<script type="text/javascript">

    $(function () {

        $(".menuItem a").mousemove(function () {
            //$(this).children('span').css("text-decoration", "none");
            $(this).children('img').addClass("fade");
        });
        $(".menuItem a").mouseleave(function () {
            //$(this).children('span').css("text-decoration", "underline");
            $(this).children('img').removeClass("fade");
        });
    });

</script>
<div>
    <%
        int i = 0;
        foreach (var item in contentItems)
        {
            i++;
            
            
    %>

    

    <div class="menuItem m<%=item.Id%>">
        <%if (Request.IsAuthenticated)
{%>
<div class="adminLinksContainder">
                <%=Html.ActionLink("[IMAGE]", "EditContent", "Content", new { area = "Admin", id = item.Id }, new { @class = "pictureLink edit white marg", title = "Редактировать" }).ToString().Replace("[IMAGE]","")%>
                <%=Html.ActionLink("[IMAGE]", "AddPhoto", "Content", new { area = "Admin", id = item.Id }, new { @class = "pictureLink add white marg", title = "Добавить фото" }).ToString().Replace("[IMAGE]", "")%>
                <%=Html.ActionLink("[IMAGE]", "AddContent", "Content", new { area = "Admin", id = item.Id }, new { @class = "pictureLinkSubItem addSubItem white", title = "Добавить подраздел" }).ToString().Replace("[IMAGE]", "")%>
 </div>
 <%}%>








 <%if ((string)ViewData["contentName"] != item.Name)
   { %>
        <a href="/<%=item.Name%>#titleContainer" >
        <%} %>
            <%if (Request.IsAuthenticated)
          { %> <span style="float:left" class="sortOrder">
          <%=item.SortOrder %>
          </span>
        <%} %>
           
            
            <%
                if ((string)ViewData["contentName"] == item.Name || (string)ViewData["parentContentName"] == item.Name)
                {
                  %>
                  <span class="mainMenuItemSelectedTitle">
            <%=item.Title%>
            </span>
                  <img class="mainMenuItemSelected" border="0" src="../../Content/img/<%=item.Id %>.gif" alt="<%=item.Title %>" />
                  <%}
                else
                { %>
                <span>
            <%=item.Title%>
            </span>
            <img class="" border="0" src="../../Content/img/<%=item.Id %>.gif" alt="<%=item.Title %>" />
            <%} %>
        
        <%if ((string)ViewData["contentName"] != item.Name)
   { %>
        </a>
        <%} %>






        <div class="description">
           <%=item.Description%>
        </div>


    </div>


   

    <%
            
        if (i % 5 == 0)
        {
            %>
          <div class="clear"></div>
    </div>
    <div>  
            <% 
        }
            
        }
    %>
    <div class="clear">
    </div>
     </div>
</div>
<%
}%>
