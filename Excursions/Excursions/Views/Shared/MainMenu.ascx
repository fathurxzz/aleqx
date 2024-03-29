﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Excursions.Models" %>
<ul id="menu">
	<li><a runat="server" href="~/Excursions">Home</a></li>
    <li><a runat="server" href="~/Sights">Sights</a></li>
	

    <%
        using (var context = new ContentStorage())
        {
            var menuImemList = context.Content.Select(m => m).Where(m=>!m.IsContactsPage).OrderBy(m => m.SortOrder).ToList();
            foreach (var item in menuImemList)
            {
                %>
                <li><a href="/<%=item.ContentId%>"><%=item.Title%></a></li>
                <%
            }
        }
     %>
     <li><a id="A1" runat="server" href="~/Contacts">Contacts</a></li>
    <%
        if (Request.IsAuthenticated)
        {%>
    
        <li class="admin"><%=Html.ActionLink("[Add item]", "AddEdit", "Content", new { area = "Admin" }, new { @class = "adminLink" })%></li>
    
    <%
        }        
    %>
</ul>

<!--
<ul id="forum">
	<li><a href="#">Forum</a></li>
</ul>
-->