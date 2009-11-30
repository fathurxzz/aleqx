<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="bigs.Helpers" %>

<link href="/Content/Request.css" rel="stylesheet" type="text/css" />


<%using (Html.BeginForm("SaveRequest", "Requests", new { contentUrl = Html.ResourceString("Request") }, FormMethod.Post, null))
{
    %>

<div id="requestTitle">
<%=Html.ResourceString("PleaseFillForm")%>
</div>

<div class="yellowBoxContainer">
<div class="yellowBoxContainerTop"></div>
<div class="innerYellowBoxContainer">


<div class="yellowBoxTitle">
<%=Html.ResourceString("AboutYou")%>:
</div>


<div class="textBoxContainer">
<div class="textBoxTitle"><%=Html.ResourceString("NameOfYourCompany")%></div>
<div>
    <div class="textBoxLeftSide"></div>  
        <%=Html.TextBox("nameOfYourCompany", "", new { @class = "textBox" })%>  
    <div class="textBoxRightSide"></div>    
</div>
</div>

<div class="textBoxContainer">
<div class="textBoxTitle"><%=Html.ResourceString("YourContacts")%></div>
<div>
    <div class="textBoxLeftSide"></div>  
        <%=Html.TextBox("yourContacts", "", new { @class = "textBox" })%>  
    <div class="textBoxRightSide"></div>    
</div>
</div>

<div class="textBoxContainer">
<div class="textBoxTitle"><%=Html.ResourceString("ContactTelephone")%></div>
<div>
    <div class="textBoxLeftSide"></div>  
        <%=Html.TextBox("contactTelephone", "", new { @class = "textBox" })%>  
    <div class="textBoxRightSide"></div>    
</div>
</div>


</div>
<div class="yellowBoxContainerBottom"></div>
</div>




<div class="yellowBoxContainer">
<div class="yellowBoxContainerTop"></div>
<div class="innerYellowBoxContainer">


<div class="yellowBoxTitle">
<%=Html.ResourceString("AboutTheCargo")%>:
</div>


<div class="textBoxContainer">
<div class="textBoxTitle"><%=Html.ResourceString("TeleportWhereFrom")%></div>
<div class="textBoxSubTitle">(<%=Html.ResourceString("CityAndPostalCode")%>)</div>
<div>
    <div class="textBoxLeftSide"></div>  
        <%=Html.TextBox("teleportWhereFrom", "", new { @class = "textBox" })%>  
    <div class="textBoxRightSide"></div>    
</div>
</div>

<div class="textBoxContainer">
<div class="textBoxTitle"><%=Html.ResourceString("AndWhereTo")%></div>
<div class="textBoxSubTitle">(<%=Html.ResourceString("CityAndPostalCode")%>)</div>
<div>
    <div class="textBoxLeftSide"></div>  
        <%=Html.TextBox("andWhereTo", "", new { @class = "textBox" })%>  
    <div class="textBoxRightSide"></div>    
</div>
</div>


<div class="textAreaContainer">
<div class="textBoxTitle"><%=Html.ResourceString("InformationAboutTheCargo")%></div>
<div class="textBoxSubTitle">(<%=Html.ResourceString("TypeOfTheCargo")%>)</div>
<div class="textAreaTop"></div>
    <%=Html.TextArea("cargoInformation", new { @class = "textArea" })%>
<div class="textAreaBottom"></div>
</div>

<div class="textBoxTitle"><%=Html.ResourceString("EnterTheCodeFromThePicture")%></div>
<div class="textBoxTitle"><%=Html.ResourceString("YouAreNotARobot")%></div>
<input type="submit" id="requestSubmitButton" value="<%=Html.ResourceString("Done")%>" />

</div>
<div class="yellowBoxContainerBottom"></div>
</div>
<%}%>