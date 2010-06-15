<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="AvenueGreen.Models" %>
    
    
    
    <%
        using(var context = new ContentStorage())
        {
            var newsItem = context.Article.Select(c => c).FirstOrDefault();
            if (newsItem != null)
            {
             %>
             <div id="byTheWayLabel">Кстати,</div>
             <div id="dateLabel"><%=newsItem.Date.ToShortDateString()%></div>
             <div id="newsTitleLabel"><a href="/News#<%=newsItem.Name %>"><%=newsItem.Title%></a></div>
             <div id="allNewsLink"><a href="/News">Все новости</a></div>
             <%    
        }
            else
            {
                if (Request.IsAuthenticated)
                {
                    %>
                    <a class="adminLink" href="/News">[К списку новостей]</a>
                    <% 
                } 
            }
    %>
    
    <%} %>
            
            
           

