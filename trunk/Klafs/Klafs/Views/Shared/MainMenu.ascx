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
            <img border="0" src="../../Content/img/<%=item.Id %>.gif" alt="<%=item.Title %>" />
        </a>

        <div class="description">
            Для частных домов. Разные породы дерева, многообразие форм и оснащение удовлетворят
            самые различные пожелания
        </div>
        
    </div>
    <%
        }
    %>
    <div class="clear">
    </div>
</div>
<%
}%>
