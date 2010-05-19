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
                    var currentContentId = contentId;
                    

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
                                string classNameAdmin = item.ContentId == contentId ? "adminLinkContainer selectedAdmin" : "adminLinkContainer";
                                %><div class="<%=className%>">
                                
                                <%if (item.ContentId == contentId)
                                    {
                                        Response.Write(item.Title);
                                      
                                      
                                      if (Request.IsAuthenticated&&item.Children.Count==0)
                                        { %>
                                        <div class="<%=classNameAdmin%>">
                                        <%=Html.ActionLink("[добавить]", "AddContentItem", "Admin", new { parentId = item.Id, isGalleryItem = item.IsGalleryItem }, new { @class = "adminLink" })%>
                                        </div>
                                        <%} 
                                      
                                      
                                    }
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
                                
                                
                                
                                </div><%
                                
                                var childrenItems = item.Children.OrderBy(c => c.Id).OrderBy(c=>c.SortOrder).ToList();
                                int childItemsCount = 0;
                                foreach (var childItem in childrenItems)
                             {
                                    if (item.ContentId == contentId || childItem.Parent.ContentId == parentContentId)
                                    {
                                        childItemsCount++;
                                        className = childItem.ContentId == contentId ? "childMenuItem selectedChild" : "childMenuItem";
                                        
                                        %><div class="<%=className%>">
                                        
                                        <%if (childItem.ContentId == contentId)
                                          {
                                              if (childItem.ContentId != currentContentId)
                                              {
                                                  %>
                                                  <a href="/<%=childItem.ContentId%>"><%=childItem.Title%></a>
                                                  <%
                                              }
                                              else
                                              {
%>
                                          <%=childItem.Title%>
                                        <%}
                                          }
                                          else
                                          { %>
                                        <a href="/<%=childItem.ContentId%>"><%=childItem.Title%></a>
                                        <%} %>
                                        </div><%
                                    }
                                }
                                if(childItemsCount>0&&Request.IsAuthenticated)
                                {%>
                                <div class="childMenuItem">
                                <%=Html.ActionLink("[добавить]", "AddContentItem", "Admin", new { parentId = item.Id, isGalleryItem = item.IsGalleryItem }, new { @class = "adminLink" })%>
                                </div>
                                <%
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
