<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<link href="/Content/Teleport.css" rel="stylesheet" type="text/css" />

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

        window.setInterval(blink, 50);

        //$("#mainForm").submit();
        
    }

    function blink() {
        if (cnt < 19) {
            Sys.UI.DomElement.toggleCssClass($get("diods"), "faded");
            Sys.UI.DomElement.toggleCssClass($get("teleportButton"), "buttonActive");
            cnt++;
        }
        if (cnt == 19) {
            Sys.UI.DomElement.toggleCssClass($get("picture"), " hidepicture");
        }
    };
    
</script>

<%using (Html.BeginForm("ActionName", "ControllerName", FormMethod.Post, new { id = "mainForm" }))
  {%>

<div>
Выберите объект, назовите его и телепортируйте.
    <div id="monik">
        <div id="diods" class="">
            <div id="picture" class=""></div>
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
        
        <div id="maxLength">( 10 символов )</div>
    </div>
    
    
    
    <div id="teleportButtonSubmitContainer" onclick="submitForm()">
        <a id="teleportButton" ></a>
    </div>

    
    
</div>

<%} %>