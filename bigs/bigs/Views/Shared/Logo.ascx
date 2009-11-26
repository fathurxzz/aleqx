<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="bigs.Controllers" %>
<link href="/Content/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />
<script src="../../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>
    <%
        bool isRoot = Request.Url.AbsolutePath == "/";
    if (isRoot)
    {%>
    <style type="text/css">
		#logo{position:relative; top:-256px;}
		.hidden{visibility:hidden;}
	</style>
	<script type="text/javascript">
	    var cnt = 0;
	    $(function() {
	    $("#logo").animate({ top: 0 }, 1000, function() { window.setInterval(blink, 300); });
	        
	    });

	    function blink() {
	        if (cnt < 10) {
	            Sys.UI.DomElement.toggleCssClass($get("connectionSign"), "hidden");
	            cnt++;
	        }
	    };
	</script>
    <%} %>

<%if (!isRoot)
  { %><a href="/"><%} %>
<div id="logo" class="<%=SystemSettings.CurrentLanguageShort%>logo">
<div id="connectionSign" class="<%=SystemSettings.CurrentLanguageShort%>connection"></div>
</div>
<%if (!isRoot)
  { %></a><%} %>


