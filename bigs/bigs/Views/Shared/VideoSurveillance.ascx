<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
 <style type="text/css">
		.hidden{visibility:hidden;}
	</style>
<script type="text/javascript">
    $(function() { window.setInterval(blinkCamera, 600); });
    function blinkCamera() { Sys.UI.DomElement.toggleCssClass($get("cameraRecord"), "hidden"); };
</script>

<div class="<%=bigs.Controllers.SystemSettings.CurrentLanguageShort%>video">
<div id="cameraRecordBg">
<div id="cameraRecord">
</div>
</div>

</div>
