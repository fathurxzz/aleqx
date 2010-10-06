<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Excursions.Models" %>
<img src="../../Content/img/gtop.gif" alt="" width="191" height="8" />
<div class="title1">Popular Tours</div>
<ul class="contries">
    <%
        using (var context = new ContentStorage())
        {
            var popularExcursions = context.Excursion.Select(e => e).Where(e => e.Popular).OrderBy(e=>e.SortOrder).ToList();

            foreach (var excursion in popularExcursions)
            {
                %>
                <li><%=Html.ActionLink(excursion.Title, "Details", "Excursions", new { area = "", id = excursion.Name },null)%></li>
                <%
            }
    }  
    %>
</ul>
<a href="~/Excursions" runat="server" class="more">more tours</a>
<img src="../../Content/img/gbot.gif" alt="" width="191" height="8" />