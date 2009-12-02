<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="bigs.Helpers" %>
<link href="/Content/Request.css" rel="stylesheet" type="text/css" />
<%
    string requestStatus = (string)ViewData["requestStatus"];
    if (!string.IsNullOrEmpty(requestStatus))
        Response.Write(requestStatus);
    else
    {
%>

<%using (Html.BeginForm("Index", "Requests", new { contentUrl = Html.ResourceString("Request") }, FormMethod.Post, null))
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
<div class="textBoxTitle"><%=Html.ResourceString("NameOfYourCompany")%><%= Html.ValidationMessage("companyName", "*", new { @class = "validationError" })%></div>
<div>
    <div class="textBoxLeftSide"></div>  
        <%=Html.TextBox("nameOfYourCompany", "", new { @class = "textBox" })%>  
    <div class="textBoxRightSide"></div>    
</div>
</div>

<div class="textBoxContainer">
<div class="textBoxTitle"><%=Html.ResourceString("YourContacts")%><%= Html.ValidationMessage("clientName", "*", new { @class = "validationError" })%></div>
<div>
    <div class="textBoxLeftSide"></div>  
        <%=Html.TextBox("yourContacts", "", new { @class = "textBox" })%>  
    <div class="textBoxRightSide"></div>    
</div>
</div>

<div class="textBoxContainer">
<div class="textBoxTitle"><%=Html.ResourceString("ContactTelephone")%><%= Html.ValidationMessage("phoneEmail", "*", new { @class = "validationError" })%></div>
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
<div class="textBoxTitle"><%=Html.ResourceString("TeleportWhereFrom")%><%= Html.ValidationMessage("teleportFrom", "*", new { @class = "validationError" })%></div>
<div class="textBoxSubTitle">(<%=Html.ResourceString("CityAndPostalCode")%>)</div>
<div>
    <div class="textBoxLeftSide"></div>  
        <%=Html.TextBox("teleportWhereFrom", "", new { @class = "textBox" })%>  
    <div class="textBoxRightSide"></div>    
</div>
</div>

<div class="requestItemsSeparator"></div>

<div class="textBoxContainer">
<div class="textBoxTitle"><%=Html.ResourceString("AndWhereTo")%><%= Html.ValidationMessage("teleportTo", "*", new { @class = "validationError" })%></div>
<div class="textBoxSubTitle">(<%=Html.ResourceString("CityAndPostalCode")%>)</div>
<div>
    <div class="textBoxLeftSide"></div>  
        <%=Html.TextBox("andWhereTo", "", new { @class = "textBox" })%>  
    <div class="textBoxRightSide"></div>    
</div>
</div>
<div class="requestItemsSeparator"></div>

<div class="textAreaContainer">
<div class="textBoxTitle"><%=Html.ResourceString("InformationAboutTheCargo")%><%= Html.ValidationMessage("cargoInfo", "*", new { @class = "validationError" })%></div>
<div class="textBoxSubTitle">(<%=Html.ResourceString("TypeOfTheCargo")%>)</div>
<div class="textAreaTop"></div>
    <%=Html.TextArea("cargoInformation", new { @class = "textArea", rows = "5" })%>
<div class="textAreaBottom"></div>
</div>
<div class="requestItemsSeparator"></div>

<div class="textBoxTitle"><%=Html.ResourceString("EnterTheCodeFromThePicture")%> <%=Html.ResourceString("YouAreNotARobot")%><%= Html.ValidationMessage("captchaInvalid", "*", new { @class = "validationError" })%></div>

<div id="capchaContainer">
<%= Html.CaptchaImage(50, 160)%><br />
<%= Html.TextBox("captcha", "")%>
</div>



<div id="requestSubmitButtonContainer">
<input type="submit" id="requestSubmitButton" value="<%=Html.ResourceString("Done")%>" />
</div>

</div>
<div class="yellowBoxContainerBottom"></div>
</div>
<%}
    }%>