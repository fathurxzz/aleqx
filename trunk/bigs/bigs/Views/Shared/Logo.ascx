<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="bigs.Controllers" %>

<%
    string absolutePath = Request.Url.AbsolutePath;
    if (absolutePath != "/")
    { }
 %>
 
<link href="/Content/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />

<style type="text/css">
		.toggler { width: 500px; height: 200px; }
		#button { padding: .5em 1em; text-decoration: none; }
		#effect { width: 240px; height: 135px; padding: 0.4em; position: relative; }
		#effect h3 { margin: 0; padding: 0.4em; text-align: center; }
		.ui-effects-transfer { border: 2px dotted gray; } 
	</style>
	
	<script type="text/javascript">
	    $(function() {

	        //run the currently selected effect
	        function runEffect() {
	            //get effect type from
	            var selectedEffect = "blind";

	            //most effect types need no options passed by default
	            var options = {};
	            //check if it's scale, transfer, or size - they need options explicitly set
	            if (selectedEffect == 'scale') { options = { percent: 100 }; }
	            else if (selectedEffect == 'transfer') { options = { to: "#button", className: 'ui-effects-transfer' }; }
	            else if (selectedEffect == 'size') { options = { to: { width: 280, height: 185} }; }

	            //run the effect
	            $("#effect").show(selectedEffect, options, 500, callback);
	        };

	        //callback function to bring a hidden box back
	        function callback() {
	            /*setTimeout(function() {
	                $("#effect:visible").removeAttr('style').hide().fadeOut();
	            }, 1000);*/
	        };

	        //set effect from select menu value
	        $("#button").click(function() {
	            runEffect();
	            return false;
	        });

	        $("#effect").hide();
	    });
	</script>


<div id="logo" class="<%=SystemSettings.CurrentLanguageShort%>logo">

    <div class="toggler">
	    <div id="effect" class="ui-widget-content ui-corner-all">
	    </div>
    </div>
<a href="#" id="button" class="ui-state-default ui-corner-all">Run Effect</a>
</div>
