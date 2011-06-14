<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    List<Klafs.Models.Content> headerMenuItems = null;
    if (ViewData["headerMenuItems"] != null)
        headerMenuItems = (List<Klafs.Models.Content>)ViewData["headerMenuItems"];

    if (headerMenuItems != null && headerMenuItems.Count() > 0)
    {
%>



 <div id="headerMenu">




            :::::: 
            
            <%
        foreach (var item in headerMenuItems)
{
  %>
  
  <a href="/<%=item.Name %>"><%=item.MenuTitle%></a> ::::::
  <%
}
         %>
            <!--<a href="#">Ссылки на ресурсы</a> :::::: <a href="#">Партнерская информация</a>
            :::::: <a href="#">Контактная информация</a>-->
            
            
            
            


 </div>

 <%
    }%>