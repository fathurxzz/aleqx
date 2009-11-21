<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<link href="../../Content/Slider.css" rel="stylesheet"
        type="text/css" />
<script src="../../Scripts/ui.core.js" type="text/javascript"></script>
<script src="../../Scripts/ui.slider.js" type="text/javascript"></script>
<script src="../../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

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

        /*$("#amount").val('$' + $("#slider").slider("value"));*/
    });
</script>    
   
   <input type="text" id="amount" style="border:0; color:#f6931f; font-weight:bold;" />
   
<div id="slider" style="width:300px !important"></div>



<div id="transfer1" style="display:block;">
transfer1
</div>
<div id="transfer2" style="display:none;">
transfer2
</div>
<div id="transfer3" style="display:none;">
transfer3
</div>
<div id="transfer4" style="display:none;">
transfer4
</div>
<div id="transfer5" style="display:none;">
transfer5
</div>

  


