<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="bigs.Helpers" %>
<%@ Import Namespace="bigs.Controllers" %>
<link href="/Content/Teleported.css" rel="stylesheet" type="text/css" />

<div id="teleportedContainer">
<div id="teleportedTop" class="teleportedTop-<%=SystemSettings.CurrentLanguageShort%>">
</div>
<div id="teleportedMiddle">
<img id="teleportedImage" src="<%=ViewData["teleportObjectImageUrl"]%>" alt="moto" />
</div>
<div id="telepordedBottom">
</div>

<div id="text">
<%=Html.ResourceString("ThatIsTheWayWeWorkBegin")%> <%=ViewData["teleportMessage"]%>, <%=Html.ResourceString("ThatIsTheWayWeWorkEnd")%>
<br />
<%=Html.ActionLink(Html.ResourceString("AskUs"), "Index", "Requests", new { contentUrl = Html.ResourceString("Request") },null)%>
</div>
</div>








