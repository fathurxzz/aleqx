<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="AvenueGreen.Helpers"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import Namespace="AvenueGreen.Models"%>
<script type="text/javascript">
    /*$(function() {
    $('#widget2').ifixpng();
    });*/
</script>
<%
    
    
    using (var context = new ContentStorage())
    {
        var widgets = context.Widgets.Where(w => w.Type == 2).Select(w => w).ToList();
        if (widgets.Count > 0)
        {
            var rnd = new Random();
            var widget = widgets.Skip(rnd.Next(widgets.Count)).Take(1).First();
   
        %>
            <div id="widget2Sign">А может, Вы<br />ищете...</div>
        <%if (widget != null)
          { %>
            <%=Html.Image(GraphicsHelper.GetCachedImage("~/Content/WidgetImages", widget.ImageSource, "thumbnail2"), "", new { id = "widget2" })%>
            <div id="widget2Title"><a href="<%=widget.Url%>"><%=widget.Title%></a></div>
            <%} %>
            <%
        }
    }%>



  
<%
    
    if(Request.IsAuthenticated){ %>


<div>
    <a class="adminLink" style="margin-left:50px" href="/Widgets/2">[редактировать список]</a>
</div>
<%} %>

