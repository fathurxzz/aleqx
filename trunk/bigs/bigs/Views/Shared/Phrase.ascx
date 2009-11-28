<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="bigs.Models" %>
<% 
    using (DataStorage context = new DataStorage())
    {
        var rnd = new Random();
        List<ImageContent> images = (from im in context.ImageContent select im).ToList();
        string fileName = images.Skip(rnd.Next(images.Count)).Take(1).First().FileName;
        
        %>
        <table border="1" style=" border-color:Black; border-collapse:collapse; margin:0px 0px 0px 0px; padding:0px 0px 0px 0px;" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width:99%"></td>
            <td>
                <img id="phraseImage" alt="<%=fileName%>" src="/Content/Objects/<%=fileName%>" />
            </td>
        </tr>
        </table>
        <%
       
    
    }
%>