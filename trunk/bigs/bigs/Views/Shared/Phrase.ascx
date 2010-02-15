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

            var imagesWeigted = images.Select(i => new { image = i, weight = (!string.IsNullOrEmpty(i.Url) ? 5 : 1) }).OrderBy(i=>rnd.Next(10) * 1/i.weight);

            ImageContent imageItem = imagesWeigted.First().image;
            //string fileName = images.Skip(rnd.Next(images.Count)).Take(1).First().FileName;
            string fileName = imageItem.FileName;
            string url = imageItem.Url;
            if (System.IO.File.Exists(imageLocationPath + fileName))
            {
                System.Drawing.Image image = System.Drawing.Image.FromFile(imageLocationPath + fileName);
                int paddingTop = 160 - image.Height;
                int paddingLeft = 660 - image.Width;
        %>
        
        <%if(!string.IsNullOrEmpty(url)){ %>
                <a href="<%=url%>">
                <img id="phraseImage" style="padding-top:<%=paddingTop%>px; padding-left:<%=paddingLeft%>px; border:0" alt="<%=fileName%>" src="/Content/Objects/<%=fileName%>" />
                </a>
        <%}else{ %>
                <img id="phraseImage" style="padding-top:<%=paddingTop%>px; padding-left:<%=paddingLeft%>px" alt="<%=fileName%>" src="/Content/Objects/<%=fileName%>" />
                <%} %>
        <%
            }
        }
    }
%>