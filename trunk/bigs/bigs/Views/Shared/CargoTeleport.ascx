<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="bigs.Models" %>
<%@ Import Namespace="bigs.Controllers" %>
<%@ Import Namespace="bigs.Helpers" %>

<link href="/Content/Slider.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/ui.core.js" type="text/javascript"></script>
<script src="/Scripts/ui.slider.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script type="text/javascript">
    $(function() {
        $("#slider").slider({
            value: 1,
            min: 1,
            max: 5,
            step: 1,
            slide: function(event, ui) {
                for (var j = 1; j <= 5; j++) {
                    $("#transfer" + j).css({ 'display': 'none' });
                }
                $("#transfer" + ui.value).css({ 'display': 'block' });
            }
        });
    });
</script>    
   





<table id="steps">
<tr>
    <td align="center"><%=Html.ResourceString("Step").ToUpper()%>&nbsp;1</td>
    <td align="center"><%=Html.ResourceString("Step").ToUpper()%>&nbsp;2</td>
    <td align="center"><%=Html.ResourceString("Step").ToUpper()%>&nbsp;3</td>
    <td align="center"><%=Html.ResourceString("Step").ToUpper()%>&nbsp;4</td>
    <td align="center"><%=Html.ResourceString("Step").ToUpper()%>&nbsp;5</td>
</tr>
</table>

   

<div id="sliderContainer">
    <div id="slider"></div>
</div>


<div id="sliderResultContainer">
<% 
    
    using (DataStorage context = new DataStorage())
    {
        for (int i = 1; i <= 5; i++)
        {
            string contentName = "transfer" + i;

            Response.Write(i == 1 ? "<div id=\"" + contentName + "\" style=\"display:block;\">" : "<div id=\"" + contentName + "\" style=\"display:none;\">");

            SiteContent transferContent = (from content in context.SiteContent.Where(c => c.Name == contentName && c.Language == SystemSettings.CurrentLanguage) select content).First();

            if (Request.IsAuthenticated)
            {
                Response.Write("<div class=\"adminEditLink\">");
                Response.Write(Html.ActionLink("�������������", "EditTransfers", "Admin", new { contentUrl = transferContent.Url, controllerName = ViewContext.RouteData.Values["controller"] }, null));
                Response.Write("</div>");
            }


            Response.Write(transferContent.Text);




            Response.Write("</div>");

        }
    }
     %>
</div>
  


