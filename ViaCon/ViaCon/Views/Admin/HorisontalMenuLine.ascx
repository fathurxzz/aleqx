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
        //var lastLevel = (bool) ViewData["lastLevel"];
        
        
        if (Model.Count() != 0 /*&& !lastLevel */ || level == 0)
            Html.RenderAction<ViaCon.Controllers.AdminController>(a => a.HorisontalMenuLine(parentContentId, level+1, itemHasChildrenParent, contentId, currentContentId));
        
        if (Model.Count() != 0)
        {
        %>
        <div class="contentBoxHorisontalMenu mcolor<%=(level % 2)%>">
        <table style="width:100%; border:none;">
        <tr>
        <td align="center" >
            <table cellpadding="0" cellspacing="0" style="border-collapse:collapse; border:none;">
            <tr>
            <%
            
            foreach (var item in Model)
            {
                %>
                
                <%if (item.ContentId == parentParentContentId)
                {
                if (item.ContentId != currentContentId)
                {
                    %><td class="horisontalMenuItem horisontalMenuItemActive color<%=((level+1) % 2)%>">
                    <a href="/<%=item.ContentId%>"><%=item.Title+level%></a><%
                }
                else
                {

                    string className = (!itemHasChildren) ? "" : "horisontalMenuItemActive color" + ((level + 1) % 2);
                                  
                    %><td class="horisontalMenuItem <%=className%>"><%=item.Title + level%><%}
                }
                  else
                  { %>
                    <td class="horisontalMenuItem"><a href="/<%=item.ContentId%>"><%=item.Title + level%></a>
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

