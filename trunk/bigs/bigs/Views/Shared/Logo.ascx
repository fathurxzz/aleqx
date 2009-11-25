<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="bigs.Controllers" %>
<link href="/Content/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />
<script src="../../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>
    <%
    if (Request.Url.AbsolutePath=="/")
    {%>
    <style type="text/css">
		#logo{position:relative; top:-256px;}
		.hidden{visibility:hidden;}
	</style>
	<script type="text/javascript">
	    $(function() {
	    $("#logo").animate({ top: 0 }, 1000, function() { window.setInterval(blink, 300); });
	        
	    });

	    function blink() {
	        Sys.UI.DomElement.toggleCssClass($get("connectionSign"), "hidden");
	    };
	</script>
    <%} %>

<div id="logo" class="<%=SystemSettings.CurrentLanguageShort%>logo">
<div id="connectionSign" class="<%=SystemSettings.CurrentLanguageShort%>connection"></div>
</div>
