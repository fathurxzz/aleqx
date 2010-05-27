<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl"%>
<%@ Import Namespace="AvenueGreen.Helpers"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import Namespace="AvenueGreen.Models"%>

<%
    using (var context = new ContentStorage())
    {
        var widgets = context.Widgets.Where(w=>w.Type==1).Select(w => w).ToList();
        if (widgets.Count > 0)
        {
            var rnd = new Random();
            var widget = widgets.Skip(rnd.Next(widgets.Count)).Take(1).First();
            if (widget != null)
            {
%>
<%=Html.Image(GraphicsHelper.GetCachedImage("~/Content/WidgetImages", widget.ImageSource, "thumbnail2"), "", new { id = "projectsWidgetPicture" })%>
    
<%
            }
        }
    }%>
<%if(Request.IsAuthenticated){ %>
<div>
<a class="adminLink" style="margin-left:50px" href="\Widgets\1">[редактировать список]</a>
</div>
<%} %>


