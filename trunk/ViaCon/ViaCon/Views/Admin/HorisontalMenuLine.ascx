<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<ViaCon.Models.Content>>" %>
<%@ Import Namespace="System.Web.UI.MobileControls"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
    <%
        var currentContentId = (string)ViewData["currentContentId"];
        var parentContentId = (string)ViewData["parentContentId"];
        var contentId = (string)ViewData["contentId"];
        var parentParentContentId = (string)ViewData["parentParentContentId"];
        var level = (int) ViewData["level"];


        if (Model.Count() != 0 || level == 0)
            Html.RenderAction<ViaCon.Controllers.AdminController>(a => a.HorisontalMenuLine(parentContentId, level + 1, contentId,currentContentId));
        
        if (Model.Count() != 0)
        {
        %>
        <div class="contentBoxHorisontalMenu">
        <table style="width:100%; border:none;">
        <tr>
        <td align="center" >
            <table cellpadding="0" cellspacing="0" style="border-collapse:collapse; border:none;">
            <tr>
            <%foreach (var item in Model)
            {%>
                <td class="horisontalMenuItem">
                <%if (item.ContentId == parentParentContentId)
{
    if (item.ContentId != currentContentId)
    {
        %><%="["+item.Title+"]"%><%
    }
    else
    {
                          
                      
%><%=item.Title%><%}
}
                  else
                  { %>
                <a href="/<%=item.ContentId%>"><%=item.Title%></a>
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

