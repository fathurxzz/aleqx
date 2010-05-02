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
            <td colspan="3" class="tmenuMiddleBox">
                <%
                    string contentId = (string)ViewData["id"];

                    using (var context = new ViaCon.Models.ContentStorage())
                    {
                        var menuList = context.Content.Where(c => !c.Horisontal).ToList();

                        //var menuListSecondLevel = context.Content.Where(c => !c.Horisontal && c.Parent != null).ToList();


                        if (menuList != null)
                        {
                            foreach (var item in menuList)
                            {
                                if (item.Parent != null) continue;
                                //if (item.Parent.Count > 0) continue;
                                //string cls = item.ContentId == contentId ? "menuLink selected" : "menuLink";
                                if (item.ContentId == contentId){ %><div class="menuItem selected"><%=item.Title%></div><%
                                
                                    if(item.Children!=null)
                                    {
                                        foreach (var child in item.Children)
                                        {
                                            %><div class="childMenuItem"><a href="/<%=child.ContentId%>"><%=child.Title%></a></div><%
                                        }
                                    }
                                
                                }else{ %><div class="menuItem"><a href="/<%=item.ContentId%>"><%=item.Title%></a></div><%}
                                
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
