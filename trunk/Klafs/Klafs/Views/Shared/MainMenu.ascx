<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    var contentItems = (List<Klafs.Models.Content>) ViewData["contentItems"];
    if (contentItems.Count() > 0)
    {
%>


<div id="mainMenu">

    <%
        foreach (var item in contentItems)
{
  
            %>
            
            
            <%
}
         %>

    <div class="menuItem">
        <a href="#">Сауна<br />
        <img src="../../Content/img/sauna.gif" alt="Сауна" />
        </a>
        <div class="description">
        Для частных домов. Разные породы дерева, многообразие форм и оснащение удовлетворят самые различные пожелания
        </div>
    </div>
    <div class="menuItem">
        <a href="#">Баня<br />
        <img src="../../Content/img/sauna.gif" alt="Сауна" />
        </a>
        <div class="description">
        Для частных домов. Разные породы дерева, многообразие форм и оснащение удовлетворят самые различные пожелания
        </div>
    </div>
    <div class="menuItem">
        <a href="#">Римская парная<br />
        <img src="../../Content/img/sauna.gif" alt="Сауна" />
        </a>
        <div class="description">
        Для частных домов. Разные породы дерева, многообразие форм и оснащение удовлетворят самые различные пожелания
        </div>
    </div>
    <div class="menuItem">
        <a href="#">Хамам<br />
        <img src="../../Content/img/sauna.gif" alt="Сауна" />
        </a>
        <div class="description">
        Для частных домов. Разные породы дерева, многообразие форм и оснащение удовлетворят самые различные пожелания
        </div>
    </div>
    <div class="menuItem">
        <a href="#">SPA<br />
        <img src="../../Content/img/sauna.gif" alt="Сауна" />
        </a>
        <div class="description">
        Для частных домов. Разные породы дерева, многообразие форм и оснащение удовлетворят самые различные пожелания
        </div>
    </div>
    <div class="clear"></div>
</div>
<%
    }%>
