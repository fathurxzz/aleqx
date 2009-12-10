<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<link href="/Content/Teleport.css" rel="stylesheet" type="text/css" />
<link href="/Content/Carousel.css" rel="stylesheet" type="text/css" />
<link href="/Content/CarouselSkin.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jquery.jcarousel.pack.js" type="text/javascript"></script>


<script type="text/javascript">

    var cnt = 0;
    $(function() {
        $(".arrow").mousedown(function() {
            $(this).css("background-position", "0 -120px");
        });
        $(".arrow").mouseout(function() {
            $(this).css("background-position", "0 0px");
        });
        $(".arrow").mouseover(function() {
            $(this).css("background-position", "0 -60px");
        });
        $(".arrow").mouseup(function() {
            $(this).css("background-position", "0 -60px");
        });
    });

    function submitForm() {

        //$("#diods").addClass("faded");

        if ($.trim($get("textBox").value) == "") {
            $("#textBoxValid").html("*");
        }
        else {
            $("#textBoxValid").html("");
            window.setInterval(blink, 50);
        }
                

        //$("#mainForm").submit();
        
    }

    function blink() {
        if (cnt < 19) {
            Sys.UI.DomElement.toggleCssClass($get("diods"), "faded");
            Sys.UI.DomElement.toggleCssClass($get("teleportButton"), "buttonActive");
        }else if (cnt == 19) {
        $("#picture").effect("puff", null, 500, null);
        //$("#picture").effect("drop", { direction: "right" }, 500);
            /*Sys.UI.DomElement.toggleCssClass($get("picture"), " hidepicture");*/
        }
        cnt++;
    };
    
    function mycarousel_initCallback(carousel) {
        jQuery('.jcarousel-control a').bind('click', function() {
            carousel.scroll(jQuery.jcarousel.intval(jQuery(this).text()));
            return false;
        });
     
        jQuery('.jcarousel-scroll select').bind('change', function() {
            carousel.options.scroll = jQuery.jcarousel.intval(this.options[this.selectedIndex].value);
            return false;
        });

        jQuery('#rightArrow').bind('click', function() {
            carousel.next();
            return false;
        });

        jQuery('#leftArrow').bind('click', function() {
            carousel.prev();
            return false;
        });
    };



    jQuery(document).ready(function() {
        jQuery('#mycarousel').jcarousel(
        {
            initCallback: mycarousel_initCallback,
            scroll: 1,
            buttonNextHTML: null,
            buttonPrevHTML: null
        });


    });
    
</script>

<%using (Html.BeginForm("ActionName", "ControllerName", FormMethod.Post, new { id = "mainForm" }))
  {%>

<div>
�������� ������, �������� ��� � ��������������.
    <div id="monik">
        <div id="diods" class="jcarousel-skin-tango">
            <div id="picture">
                <ul id="mycarousel" class="">
                <li><img src="/Content/images/picture.png" alt="" width="300" height="180" /></li>
                <li><img src="/Content/images/picture.png" alt="" width="300" height="180" /></li>
                <li><img src="/Content/images/picture.png" alt="" width="300" height="180" /></li>
                <li><img src="/Content/images/picture.png" alt="" width="300" height="180" /></li>
                <li><img src="/Content/images/picture.png" alt="" width="300" height="180" /></li>
                <li><img src="/Content/images/picture.png" alt="" width="300" height="180" /></li>
                <li><img src="/Content/images/picture.png" alt="" width="300" height="180" /></li>
                </ul>
            </div>
        </div>
    </div>

    <div id="arrowsContainer">
        <a id="leftArrow" class="leftArrow arrow"></a>
        <div id="arrowSign"></div>
        <a id="rightArrow" class="rightArrow arrow"></a>
    </div>

    <div id="editTextContainer">
        <div id="objectName"></div>

        <div id="editLeftSide"></div>
        <%=Html.TextBox("textBox", "", new { @class = "textBox", maxlength = "10" })%>  
        <div id="editRightSide"></div>
        
        <div id="textBoxValid" style=""></div>
        
        <div id="maxLength">( 10 �������� )</div>
    </div>
    
    
    
    <div id="teleportButtonSubmitContainer" onclick="submitForm()">
        <a id="teleportButton" ></a>
    </div>

    
    
</div>

<%} %>