<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="ViaCon.Models" %>
<div id="menuBox">
    
    
    <div id="divMenuBoxTopRight">
        <div id="divMenuBoxTopLeft"></div>
    </div>
    <div id="divMenuBoxMiddle">
    
    
    
   
                <%
                    string contentId = (string)ViewData["contentId"];
                    string parentContentId = (string)ViewData["parentContentId"];
                    string className = string.Empty;

                    using (var context = new ContentStorage())
                    {
                        var parentParentContent = context.Content.Include("Parent").Where(c => c.ContentId == parentContentId && c.Parent != null).FirstOrDefault();
                        if (parentParentContent != null)
                        {
                            contentId = parentContentId;
                            parentContentId = parentParentContent.Parent.ContentId;
                        }
                        var menuItemsList = context.Content.Where(c => !c.Horisontal&&!c.Collapsible).ToList();
                        foreach (var item in menuItemsList)
                        {
                            if (item.Parent == null)
                            {
                                className = item.ContentId == contentId ? "menuItem selected" : "menuItem";
                                %><div class="<%=className%>"><a href="/<%=item.ContentId%>"><%=item.Title%></a></div><%
                                var childrenItems = item.Children.OrderBy(c => c.Id).ToList();
                                foreach (var childItem in childrenItems)
                                {
                                    if (item.ContentId == contentId || childItem.Parent.ContentId == parentContentId)
                                    {
                                        className = childItem.ContentId == contentId ? "childMenuItem selected" : "childMenuItem";
                                        %><div class="<%=className%>"><a href="/<%=childItem.ContentId%>"><%=childItem.Title%></a></div><%
                                    }
                                }
                            }
                        }
                    }   
                %>
                </div>
    <div id="divMenuBoxBottomRight">
        <div id="divMenuBoxBottomLeft"></div>
    </div>
    
    
</div>
