<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Excursions.Models.Excursion>" %>
<%@ Import Namespace="Excursions.Helpers" %>

<% using (Html.BeginForm("Create", "Comments", FormMethod.Post, new { excursionId = Model.Id }))
       {
       %>
<div class="addComment">

<%=Html.Hidden("excursionId",Model.Id)%>
<div class="addCommentAuthor">
Автор:
</div>
<%=Html.TextBox("author") %>

<div class="addCommentText">Текст:</div>

<%=Html.TextArea("commentText","",10,50,null)%>
<br />

<div id="capchaContainer">
<%= Html.CaptchaImage(50, 160)%><br />
<%= Html.TextBox("captcha", "")%>
</div>


<input type="submit" value="Отправить" />
</div>
<%
       }%>