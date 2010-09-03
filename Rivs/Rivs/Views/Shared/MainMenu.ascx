<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Rivs.Models" %>
<%
    var contentId = (string)ViewData["ContentId"];
    var parentContentId = (string)ViewData["PatentContentId"];
%>
<div class="menuBox">
    <div class="menuHeader">
        главное меню
    </div>
    <ul class="menu">
        <%
            using (var context = new ContentStorage())
            {
                var menuImemList = context.Content.Include("Parent").Include("Children").Select(m => m).OrderBy(m => m.SortOrder).ToList();

                foreach (var item in menuImemList)
                {
                    if (item.Parent == null)
                    {
                        bool parentItemHasChildren = false;

                        if (item.Children.Count > 0)
                        {
                            foreach (var child in item.Children)
                            {
                                if (child.ContentId == contentId)
                                    parentItemHasChildren = true;
                            }
                        }


%>
                        <li><a href="/<%=item.ContentId%>"><%=item.Title%></a></li>
                        <%
                            if (contentId == item.ContentId || parentItemHasChildren)
                        {
                            if (item.Children.Count > 0)
                            {
                                foreach (var child in item.Children)
                                {
%>
                                <li style="padding-left: 20px;"><a href="/<%=child.ContentId%>">
                                    <%=child.Title%></a></li>
                                <%
                                }
                            }
                        }
                    }
                }
            }
        %>
    </ul>
    <%
        if (Request.IsAuthenticated)
        {%>
    <div>
        <%=Html.ActionLink("[добавить]", "AddContentItem", "Admin", null, new { @class = "adminLink" })%>
    </div>
    <%
        }        
    %>
</div>
