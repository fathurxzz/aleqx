<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="menuBox">

    <div class="menuHeader">
    главное меню
    </div>
    
    <ul class="menu">
        <li><a href="#">Главная</a></li>
        <li><a href="#">Новости</a></li>
        <li><a href="#">Специальные предложения</a></li>
        <li><a href="#">Обратная связь</a></li>
    </ul>
    
    <%
        if(Request.IsAuthenticated)
        {%>
        <div>
        <%=Html.ActionLink("[добавить]", "AddContentItem", "Admin", null, new { @class = "adminLink" })%>
        </div>
        <%
        }        
    %>

</div>