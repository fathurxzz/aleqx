<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="ViaCon.Models" %>
<div id="menuBox">
    
    
    <div id="divMenuBoxTopRight">
        <div id="divMenuBoxTopLeft"></div>
    </div>
    <div id="divMenuBoxMiddle">
    
    
    
   
                <%
                    var contentId = (string)ViewData["contentId"];
                    var parentContentId = (string)ViewData["parentContentId"];

                    using (var context = new ContentStorage())
                    {
                        var parentParentContent = context.Content.Include("Parent").Where(c => c.ContentId == parentContentId && c.Parent != null).FirstOrDefault();
                        if (parentParentContent != null)
                        {
                            contentId = parentContentId;
                            parentContentId = parentParentContent.Parent.ContentId;
                        }
                        var menuItemsList = context.Content.Where(c => !c.Horisontal&&!c.Collapsible).OrderBy(c=>c.SortOrder).ToList();
                        foreach (var item in menuItemsList)
                        {
                            if (item.Parent == null)
                            {
                                string className = item.ContentId == contentId ? "menuItem selected" : "menuItem";
                                %><div class="<%=className%>">
                                
                                <%if (item.ContentId == contentId) { Response.Write(item.Title); }
                                  else if (item.ContentId==parentContentId)
                                  {
                                  %>
                                <a href="/<%=item.ContentId%>" class="parentSelected"><%=item.Title%></a>
                                <%    
                                  }
                                  else
                                  
                                {  %>
                                <a href="/<%=item.ContentId%>"><%=item.Title%></a>
                                <%} %>
                                
                                <%if (Request.IsAuthenticated)
                                  {
                                      string adminClassName = item.ContentId == contentId ? "adminLinkContainer selectedAdmin" : "adminLinkContainer";
                                       %>
                                <div class="<%=adminClassName%>">
                                    <%=Html.ActionLink("добавить подпункт", "AddContentItem", "Admin", new { @class = "adminLink" })%>
                                </div>
                                <div class="<%=adminClassName%>">
                                    <%=Html.ActionLink("редактировать", "EditContentItem", "Admin", new { id = item.Id, collapsible = item.Collapsible }, new { @class = "adminLink" })%>
                                </div>
                                <div class="<%=adminClassName%>">
                                    <%=Html.ActionLink("добавить ***", "AddContentItem", "Admin", new { parentId = item.Id, collapsible = true }, new { @class = "adminLink" })%>
                                </div>
                                <%} %>
                                
                                </div><%
                                var childrenItems = item.Children.OrderBy(c => c.Id).OrderBy(c=>c.SortOrder).ToList();
                                foreach (var childItem in childrenItems)
                                {
                                    if (item.ContentId == contentId || childItem.Parent.ContentId == parentContentId)
                                    {
                                        className = childItem.ContentId == contentId ? "childMenuItem selectedChild" : "childMenuItem";
                                        
                                        %><div class="<%=className%>">
                                        
                                        <%if (childItem.ContentId == contentId)
                                          { %>
                                          <%=childItem.Title%>
                                        <%}
                                          else
                                          { %>
                                        <a href="/<%=childItem.ContentId%>"><%=childItem.Title%></a>
                                        <%} %>
                                        
                                        
                                        <%if (Request.IsAuthenticated)
                                          {
                                              string adminClassName = childItem.ContentId == contentId ? "adminLinkContainer selectedAdmin" : "adminLinkContainer";
                                               %>
                                        <div class="<%=adminClassName%>">
                                            <%=Html.ActionLink("редактировать", "EditContentItem", "Admin", new { id = childItem.Id, parentId = item.Id, collapsible = item.Collapsible }, new { @class = "adminLink" })%>
                                        </div>
                                        <div class="<%=adminClassName%>">
                                            <%=Html.ActionLink("добавить ***", "AddContentItem", "Admin", new { parentId = item.Id, collapsible = true }, new { @class = "adminLink" })%>
                                        </div>
                                        <%} %>
                                
                                        </div><%
                                    }
                                }
                            }
                        }
                        
                        if(Request.IsAuthenticated)
                        {
                            %>
                            <div class="adminLinkContainer">
                            <%=Html.ActionLink("добавить пункт", "AddContentItem", "Admin", new { @class = "adminLink" })%>
                            </div>
                            <%
                        }
                    }   
                %>
                </div>
    <div id="divMenuBoxBottomRight">
        <div id="divMenuBoxBottomLeft"></div>
    </div>
    
    
</div>
