<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="bigs.Models" %>
<% 
    using (DataStorage context = new DataStorage())
    {
        var rnd = new Random();
        List<ImageContent> images = (from im in context.ImageContent select im).ToList();
        string fileName = images.Skip(rnd.Next(images.Count)).Take(1).First().FileName;
        System.Drawing.Image image;
        image = System.Drawing.Image.FromFile(Server.MapPath("/Content/Objects/" + fileName));
        int paddingTop = 160 - image.Height;
        int paddingLeft = 660 - image.Width;
        %>
                <img id="phraseImage" style="padding-top:<%=paddingTop%>px; padding-left:<%=paddingLeft%>px" alt="<%=fileName%>" src="/Content/Objects/<%=fileName%>" />
        <%
    }
%>