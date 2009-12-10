<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="bigs.Helpers" %>
<link href="/Content/Teleported.css" rel="stylesheet" type="text/css" />

<div id="teleportedContainer">
<div id="teleportedTop">
</div>
<div id="teleportedMiddle">
<img id="teleportedImage" src="<%=ViewData["teleportObjectImageUrl"]%>" alt="moto" />
</div>
<div id="telepordedBottom">
</div>

<div id="text">
Так мы работаем. Причём телепортируемым объектом может быть не только <%=ViewData["teleportMessage"]%>, но и любой другой груз.
<br />
<%=Html.ActionLink("Узнайте", "Index", "Requests", new { contentUrl = Html.ResourceString("Request") },null)%> у нас!
</div>
</div>








