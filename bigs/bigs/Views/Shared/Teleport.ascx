<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<link href="/Content/Teleport.css" rel="stylesheet" type="text/css" />


<div>
�������� ������, �������� ��� � ��������������.
    <div id="monik"></div>

    <div id="arrowsContainer">
        <a class="leftArrow"></a>
        <div id="arrowSign"></div>
        <a class="rightArrow"></a>
    </div>

    <div id="editTextContainer">
        <div id="objectName"></div>

        <div id="editLeftSide"></div>
        <%=Html.TextBox("textBox", "", new { @class = "textBox",maxlength ="10" })%>  
        <div id="editRightSide"></div>
        
        <div id="maxLength">( 10 �������� )</div>
    </div>
    
    
    <input id="teleportButton" type="submit" value="���������������" />
    
</div>