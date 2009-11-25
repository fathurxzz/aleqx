<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="bigs.Controllers" %>

<%
    string absolutePath = Request.Url.AbsolutePath;
    if (absolutePath != "/")
    { }
 %>
 
<link href="/Content/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />

<script src="../../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<style type="text/css">
		.toggler { width: 500px; height: 200px; }
		#button { padding: .5em 1em; text-decoration: none; }
		#effect { width: 240px; height: 135px; padding: 0.4em; position: relative; }
		#effect h3 { margin: 0; padding: 0.4em; text-align: center; }
		.ui-effects-transfer { border: 2px dotted gray; } 
		#logo{position:relative; top:-256px;}
		.hidden{visibility:hidden;}
	</style>
	
	<script type="text/javascript">
	    $(function() {
	        $("#logo").animate({ top: 0 }, 1000, function() {window.setInterval(runEffect, 300); });
	        
	    });

	    function runEffect() {
	        Sys.UI.DomElement.toggleCssClass($get("logo"), "hidden");
	    };

	</script>


<div id="logo" class="<%=SystemSettings.CurrentLanguageShort%>logo">
</div>
