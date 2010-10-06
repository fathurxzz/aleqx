<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Excursions.Models.Excursion>" %>
<%@ Import Namespace="Excursions.Helpers" %>

<% using (Html.BeginForm("CreateComment", "Excursions", FormMethod.Post, new { excursionId = Model.Id }))
       {
       %>
<div class="addComment" id="addComment">

<%=Html.Hidden("excursionId",Model.Id)%>
<div class="addCommentAuthor">
Name:
</div>
<%=Html.TextBox("author") %>

<div class="addCommentText">Текст:</div>

<%=Html.TextArea("commentText","",10,50,null)%>
<br />

<div id="capchaContainer">
<%= Html.CaptchaImage(50, 160)%><br />
<%= Html.TextBox("captcha", "")%>
</div>


<input type="submit" value="Send" />
</div>
<%
}%>