<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<link href="/Content/Teleport.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
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
</script>

<div>
�������� ������, �������� ��� � ��������������.
    <div id="monik">
        <div id="moped"></div>
    </div>

    <div id="arrowsContainer">
        <a id="leftArrow" class="leftArrow arrow"></a>
        <div id="arrowSign"></div>
        <a id="rightArrow" class="rightArrow arrow"></a>
    </div>

    <div id="editTextContainer">
        <div id="objectName"></div>

        <div id="editLeftSide"></div>
        <%=Html.TextBox("textBox", "", new { @class = "textBox",maxlength ="10" })%>  
        <div id="editRightSide"></div>
        
        <div id="maxLength">( 10 �������� )</div>
    </div>
    
    
    
    <div id="teleportButtonSubmitContainer">
        <a></a>
    </div>
    
    
    
</div>