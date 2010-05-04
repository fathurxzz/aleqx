<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="ViaCon.Models" %>
<div id="menuBox">
    <table id="tableMenu" cellpadding="0" cellspacing="0">
        <tr>
            <td id="tmenuBoxTopLeft">
            </td>
            <td id="tmenuBoxTopMiddle">
                &nbsp;
            </td>
            <td id="tmenuBoxTopRight">
            </td>
        </tr>
        <tr>
            <td colspan="3" class="tmenuMiddleBox">
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
            </td>
        </tr>
        <tr>
            <td id="tmenuBoxBottomLeft">
            </td>
            <td id="tmenuBoxBottomMiddle">
                &nbsp;
            </td>
            <td id="tmenuBoxBottomRight">
            </td>
        </tr>
    </table>
    <!--
<div id="menuBoxTop">
    <div id="menuBoxTopLeft"></div>
    <div id="menuBoxTopRight"></div>
</div>
<div id="menuBoxMiddle">



</div>
<div id="menuBoxBottom">
    <div id="menuBoxBottomLeft"></div>
    <div id="menuBoxBottomRight"></div>
</div>
-->
</div>
