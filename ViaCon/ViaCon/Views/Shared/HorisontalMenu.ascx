<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="ViaCon.Models" %>
<%
    string contentId = (string)ViewData["contentId"];
    string parentContentId = (string)ViewData["parentContentId"];

    using (var context = new ContentStorage())
    {

        string childContentId = null;
        var parentParentContent = context.Content.Include("Parent").Where(c => c.ContentId == parentContentId && c.Parent != null).FirstOrDefault();
        if (parentParentContent != null)
        {
            childContentId = contentId;
            contentId = parentContentId;
            parentContentId = parentParentContent.Parent.ContentId;
            
        }
        
        var menuItemsList = context.Content.Include("Parent").Where(c => c.Horisontal&& c.Parent!=null).ToList();
        menuItemsList = menuItemsList.Where(c => c.Parent.ContentId == contentId).OrderBy(c=>c.SortOrder).ToList();
        if (menuItemsList.Count > 0)
        {
            %><div id="contentBoxHorisontalMenu">
            
            <table style="width:100%; border:none;">
            <tr>
            <td align="center" >
                <table cellpadding="0" cellspacing="0" style="border-collapse:collapse; border:none;">
                <tr>
                    <%
            foreach (var item in menuItemsList)
            {
                if (item.Parent.ContentId == contentId)
                {
                    %>
                    <td class="horisontalMenuItem">
                    <%
                    if (item.ContentId == childContentId)
                    {
                        %><%=item.Title%><%
                    }
                    else
                    {
                        %><a href="/<%=item.ContentId%>"><%=item.Title%></a><% 
                    }
                    %>
                    </td>
                    <%
                }
            }
            %>                
                </tr>
                </table>
            
                
            </td>
            </tr>
            </table>
     </div><%
        }
        
    }
    
%>

    


