<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Rivs.Models" %>

<%
    var contentId = (string) ViewData["ContentId"];
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
            var menuImemList = context.Content.Include("Parent").Select(m => m).OrderBy(m=>m.SortOrder).ToList();
            
            
            
            foreach (var item in menuImemList)
            {
                %>
                <li><a href="/<%=item.ContentId%>"><%=item.Title%></a></li>
                <%
            }
        }
         %>
    </ul>
    
    <%
        if(Request.IsAuthenticated)
        {%>
        <div>
        <%=Html.ActionLink("[добавить]", "AddContentItem", "Admin", null, new { @class = "adminLink" })%>
        </div>
        <%
        }        
    %>

</div>