<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="bigs.Models" %>
<%@ Import Namespace="bigs.Controllers" %>
<% 
    using (DataStorage context = new DataStorage())
    {
        var rnd = new Random();
        List<ImageContent> images = (from im in context.ImageContent where im.Language== SystemSettings.CurrentLanguage select im).ToList();
        if (images.Count > 0)
        {
            string imageLocationPath = Server.MapPath("/Content/Objects/");
            string fileName = images.Skip(rnd.Next(images.Count)).Take(1).First().FileName;
            if (System.IO.File.Exists(imageLocationPath + fileName))
            {
                System.Drawing.Image image = System.Drawing.Image.FromFile(imageLocationPath + fileName);
                int paddingTop = 160 - image.Height;
                int paddingLeft = 660 - image.Width;
        %>
                <img id="phraseImage" style="padding-top:<%=paddingTop%>px; padding-left:<%=paddingLeft%>px" alt="<%=fileName%>" src="/Content/Objects/<%=fileName%>" />
        <%
            }
        }
    }
%>