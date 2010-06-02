<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="AvenueGreen.Models" %>


<%
    var contentId = (string)ViewData["contentId"];
    var parentContentId = (string)ViewData["parentContentId"];
    var contentLevel = (int?)ViewData["contentLevel"];
    var parentParentContentId = (string)ViewData["parentParentContentId"];

    using (var context = new ContentStorage())
    {
    
         var menuItemsList = context.Content.Where(c => c.ContentLevel==0).OrderBy(c=>c.SortOrder).ToList();
         foreach (var item in menuItemsList)
         {
             if (item.ContentId == contentId)
             {
                 %>
                 <span class="selectedMainMenuItem"><%=item.Title%></span>
                 <%
             }
             else
             {
                 string className = (item.ContentId == parentContentId||item.ContentId==parentParentContentId) ? "mainMenuItem selected" : "mainMenuItem";
             %>
             <span class="<%=className%>"><a href="/<%=item.ContentId%>"><%=item.Title%></a></span>
             <%
             }
         }
    }   
%>
