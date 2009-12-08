<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<link href="/Content/Teleport.css" rel="stylesheet" type="text/css" />


<div>
Выберите объект, назовите его и телепортируйте.
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
        
        <div id="maxLength">( 10 символов )</div>
    </div>
    
    
    <input id="teleportButton" type="submit" value="ТЕЛЕПОРТИРОВАТЬ" />
    
</div>