<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Excursions.Models" %>

<div>
		<a id="A1" runat="server" href="~/Excursions">Home</a> | <a id="A2" runat="server" href="~/Contacts">Contacts</a>  
            <%
        using (var context = new ContentStorage())
        {
            var menuImemList = context.Content.Select(m => m).Where(m=>!m.IsContactsPage).OrderBy(m => m.SortOrder).ToList();
            foreach (var item in menuImemList)
            {
                %>
                 | <a href="/<%=item.ContentId%>"><%=item.Title%></a>
                <%
            }
        }
     %>

        
         
	</div>
	<p id="copy">Copyright &#169;. All rights reserved. Design by <a href="http://www.bestfreetemplates.info/" target="_blank" title="Best Free Templates">BFT</a>     </p>