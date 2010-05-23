<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="AvenueGreen.Models" %>
<div id="leftMenuBox">
<%
    
    var contentId = (string)ViewData["contentId"];
    var parentContentId = (string)ViewData["parentContentId"];
    var parentParentContentId = (string)ViewData["parentParentContentId"];
    var contentLevel = (int?)ViewData["contentLevel"];
    var parentId = (int?)ViewData["parentId"];

    using (var context = new ContentStorage())
    {
        var menuItemsList = context.Content.Include("Parent").Where(c => c.ContentLevel == 1).Where(c => c.Parent.ContentId == parentContentId || c.Parent.ContentId == contentId || c.Parent.ContentId == parentParentContentId).OrderBy(c => c.SortOrder).ToList();
         foreach (var item in menuItemsList)
         {
             
             if (item.ContentId == contentId)
             {
                 %>
                 <div class="selectedLeftMenuItem"><%=item.Title%></div>
                 <%
             }
             else
             {
                 string className = (item.ContentId == contentId || item.ContentId==parentContentId) ? "leftMenuItem selected" : "leftMenuItem";
             %>
             <div class="<%=className%>"><a href="/<%=item.ContentId%>"><%=item.Title%></a></div>
             <%
             }
         }
    }

    if (Request.IsAuthenticated&&ViewData["typeData"]!="news")
    {
        %>
        <%=Html.ActionLink("[добавить]", "AddContentItem", "Admin", new { parentId = parentId, contentLevel=1}, new { @class = "adminLink", style="margin:40px;" })%>
        <% 
    }                 
                 
%>

</div>

 