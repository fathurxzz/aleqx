<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
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
            <td class="tmenuMiddleBox">
            </td>
            <td class="tmenuMiddleBox">
                <%
                    string contentId = (string)ViewData["id"];

                    using (var context = new ViaCon.Models.ContentStorage())
                    {
                        var contentList = context.Content.Where(c => !c.Horisontal).ToList();

                        if (contentList != null)
                        {
                            foreach (var item in contentList)
                            {
                                string cls = item.ContentId == contentId ? "menuItem selected" : "menuItem";
                                
                                %>
                                <div>
                                <%=Html.ActionLink(item.Title, "Index", new { id = item.ContentId }, new { @class = cls })%>
                                </div>
                                <%
                            }
                        }
                    }   
    
                %>
            </td>
            <td class="tmenuMiddleBox">
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
