<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="AvenueGreen.Models" %>
<%
    var contentId = (string)ViewData["contentId"];
    var parentContentId = (string)ViewData["parentContentId"];
    var contentLevel = (int)ViewData["contentLevel"];
    using (var context = new ContentStorage())
    {
        var menuItemsList = context.Content.Include("Parent").Where(c => c.ContentLevel == 2).Where(c => c.Parent.ContentId == parentContentId || c.Parent.ContentId == contentId).OrderBy(c => c.SortOrder).ToList();
        foreach (var item in menuItemsList)
        {
            if (item.ContentId == contentId)
            {
                %>
                <span class="selectedhorisontalMenuItem"><%=item.Title%></span>
                <%
            }
            else
            {
                string className = item.ContentId == contentId ? "horisontalMenuItem selected" : "horisontalMenuItem";
            %>
             <span class="<%=className%>"><a href="/<%=item.ContentId%>"><%=item.Title%></a></span>
             <%
            }
        }
    }
%>