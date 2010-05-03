<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="ViaCon.Models" %>
<%
    string contentId = (string)ViewData["contentId"];
    string parentContentId = (string)ViewData["parentContentId"];

    using (var context = new ContentStorage())
    {

        var parentParentContent = context.Content.Include("Parent").Where(c => c.ContentId == parentContentId && c.Parent != null).FirstOrDefault();
        if (parentParentContent != null)
        {
            contentId = parentContentId;
            parentContentId = parentParentContent.Parent.ContentId;
        }
        
        var menuItemsList = context.Content.Include("Parent").Where(c => c.Horisontal&& c.Parent!=null).ToList();
        menuItemsList = menuItemsList.Where(c => c.Parent.ContentId == contentId).ToList();
        if (menuItemsList.Count > 0)
        {
            %><div id="contentBoxHorisontalMenu"><%
            foreach (var item in menuItemsList)
            {
                if (item.Parent.ContentId == contentId)
                {
                %>
                    <a href="/<%=item.ContentId%>"><%=item.Title%></a>
                <% 
                }
            }
            %></div><%
        }
        
    }
    
%>

    


