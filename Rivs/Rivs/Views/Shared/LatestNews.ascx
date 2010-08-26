<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Rivs.Models" %>
<div class="menuBox">
    <div class="menuHeader">
        последние публикации
    </div>
    <ul class="latestnews">
        <%
            using (var context = new ContentStorage())
            {
                var newsItems = context.Article.Select(c => c).OrderBy(c => c.Date).Take(3).ToList();
                foreach (var newsItem in newsItems)
                {
        %>
        <li class="latestnews"><a class="latestnews" href="/News#<%=newsItem.Name %>">
            <%=newsItem.Title%></a></li>
        <%
            }
        }
        %>
    </ul>
    <div class="allNewsLink">
        <a href="/News">Все новости</a></div>
</div>
