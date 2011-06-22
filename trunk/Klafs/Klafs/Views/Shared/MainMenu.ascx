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

    <%
        foreach (var item in contentItems)
        {
    %>
    <div class="menuItem">
        <a href="/<%=item.Name%>" >
            
            <span>
            <%=item.MenuTitle%>
            </span>
            <%if (Request.IsAuthenticated)
          { %> <span class="sortOrder">
          <%=item.SortOrder %>
          </span>
        <%} %>
            <img border="0" src="../../Content/img/<%=item.Id %>.gif" alt="<%=item.Title %>" />
        </a>

        <div class="description">
           <%=item.Description%>
        </div>


        <%if (Request.IsAuthenticated)
{%>
<div class="adminLinksContainder">
                <%=Html.ActionLink("Редактировать", "EditContent", "Content",
                                      new {area = "Admin", id = item.Id}, new {@class = "adminLink"})%>

                                      <br />
                                       <%=Html.ActionLink("Добавить фото", "AddPhoto", "Content",
                                                new {area = "Admin", id = item.Id}, new {@class = "adminLink"})%>
                                                </div>
                                            <%
}%>
    </div>
    <%
        }
    %>
    <div class="clear">
    </div>
</div>
<%
}%>
