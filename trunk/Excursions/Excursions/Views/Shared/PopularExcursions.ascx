<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Excursions.Controllers" %>
<%@ Import Namespace="Excursions.Models" %>
<img src="../../Content/img/gtop.gif" alt="" width="191" height="8" />
<div class="title1">

<%
    ExcursionType excursionType = ExcursionType.Excursion;
    if (ViewData["ExcursionType"] != null)
        excursionType = (ExcursionType) ViewData["ExcursionType"];
    if (excursionType == ExcursionType.Sight)
    {%>Sights<%}
    else
    {%>Popular Tours<%}
%>
</div>
<ul class="contries">
    <%
        using (var context = new ContentStorage())
        {
            var exType = (int)excursionType;
            var popularExcursions = context.Excursion.Select(e => e).Where(e => e.Popular && (e.ExcursionType == exType || exType == 0)).OrderBy(e => e.SortOrder).ToList();

            foreach (var excursion in popularExcursions)
            {
                %>
                <li><%=Html.ActionLink(excursion.Title, "Details", "Excursions", new { area = "", id = excursion.Name },null)%></li>
                <%
            }
    }  
    %>
</ul>



    
    <%if (excursionType == ExcursionType.Sight){%>
    <a id="A1" href="~/Sights" runat="server" class="more">more sights</a>
    <%}else{%>
    <a id="A2" href="~/Excursions" runat="server" class="more">more tours</a>
    <%}%>

<img src="../../Content/img/gbot.gif" alt="" width="191" height="8" />