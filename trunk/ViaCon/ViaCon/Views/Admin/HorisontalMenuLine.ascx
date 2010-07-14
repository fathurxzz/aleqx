<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<ViaCon.Models.Content>>" %>
<%@ Import Namespace="System.Web.UI.MobileControls"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
    <%
        var currentContentId = (string)ViewData["currentContentId"];
        var parentContentId = (string)ViewData["parentContentId"];
        var contentId = (string)ViewData["contentId"];
        var parentParentContentId = (string)ViewData["parentParentContentId"];
        var level = (int) ViewData["level"];
        var itemHasChildren = (bool)ViewData["itemHasChildren"];
        var itemHasChildrenParent = (bool)ViewData["itemHasChildrenParent"];
        var lastLevel = (bool) ViewData["lastLevel"];
        
        
        if (Model.Count() != 0 || lastLevel /*&& !lastLevel  || level == 0*/)
        //if(!lastLevel)
            Html.RenderAction<ViaCon.Controllers.AdminController>(a => a.HorisontalMenuLine(parentContentId, level, itemHasChildrenParent, contentId, currentContentId));
        
        
        
        
        if (Model.Count() != 0)
        {
            int menuLevel = 0;
            foreach (var content in Model)
            {
                menuLevel = content.HorisontalLevel;
            }
        %>
        <div class="contentBoxHorisontalMenu mcolor<%=(menuLevel % 3)%>">
        <table style="width:100%; border:none; border-collapse:collapse" cellpadding="0" cellspacing="0">
        <tr>
        <td align="center" >
            <table cellpadding="0" cellspacing="0" style="border-collapse:collapse; border:none;">
            <tr>
            <%
            
            foreach (var item in Model)
            {
                %>
                <td class="horisontalMenuItem">
                <%if (item.ContentId == parentParentContentId)
                {
                    if (item.ContentId != currentContentId)
                    {
                        %><div class="horisontalMenuItemActive color<%=((menuLevel+1) % 3)%>">
                        <a href="/<%=item.ContentId%>"><%=item.Title%></a></div><div class="shadow"></div> <%
                    }
                    else
                    {
                        if(!itemHasChildren)
                        {
                            %><div class="selectedItem"><%=item.Title%></div><%
                        }
                        else
                        {
                            string className =  "horisontalMenuItemActive color" + ((menuLevel + 1) % 3);
                            %><div class="<%=className%>"><%=item.Title%></div><div class="shadow"></div><%
                        }
                    }
                }
                  else
                  { %>
                    <div><a href="/<%=item.ContentId%>"><%=item.Title%></a></div>
                <%} %>
                </td>
            <%}%>
            </tr>
            </table>
        </td>
        </tr>
        </table>
        </div>
        <%
        }
           
            
%>

