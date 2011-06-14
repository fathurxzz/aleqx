<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%


    List<Klafs.Models.Content> contentItems = null;

    if (ViewData["contentItems"] != null)
        contentItems = (List<Klafs.Models.Content>)ViewData["contentItems"];



    if (contentItems != null && contentItems.Count() > 0)
    {
%>
<div id="mainMenu">
    <%
        foreach (var item in contentItems)
        {
  
    %>
    <div class="menuItem">

        <a href="#"><%=item.Title %><br />
            <img src="../../Content/img/<%=item.ImageSource %>" alt="<%=item.Title %>" />
        </a>
        <div class="description">
            Для частных домов. Разные породы дерева, многообразие форм и оснащение удовлетворят
            самые различные пожелания
        </div>
        <%if (Request.IsAuthenticated)
          {%>
        <%=Html.ActionLink("редактировать","EditContent","Content",new{area="Admin", id=item.Id },new{ @class="adminLink"})%>
        <%}%>
    </div>
    <%
        }
    %>
    <div class="clear">
    </div>
</div>
<%
}%>
