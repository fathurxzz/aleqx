<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Daily.Models" %>
<%
    using (var context = new ContentStorage())
    {
        var newsTitleList = context.News.Select(n => n).ToList();
        foreach (var newsEntity in newsTitleList)
        {
            %>
            <div class="newsTitleListItem">
                <%=Html.ActionLink(newsEntity.Title, "NewsDetails", "Home", new { area = "", id = newsEntity.Id }, null)%>
            </div>
            <%
        }
    }
%>