<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Excursions.Models.Excursion>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div id="big">

<h2><%=Model.Title %></h2>
<div class="big_photo">
<img height="217" width="260" alt="" src="../../Content/img/big_photo.jpg" />
</div>
<div class="text">
<%=Model.Text %>
</div>
<%=Html.ActionLink("К списку экскурсий", "Index", "Excursions", null, new { @class = "more" })%>
</div>

<div class="comments">
Коментарии:
    <% foreach (var item in Model.Comments)
    {
        Html.RenderPartial("Comment", item);
    } 
    %>
</div>


<% using (Html.BeginForm("Create", "Comments", FormMethod.Post, new { excursionId = Model.Id }))
       {
       %>
<div class="addComment">

<%=Html.Hidden("excursionId",Model.Id)%>
<%=Html.TextBox("author") %>
<br />
<%=Html.TextArea("commentText","",10,50,null)%>
<br />

<input type="submit" value="Отправить" />
</div>
<%
       }%>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="LeftContent" runat="server">
<div id="left_navigation">
			<img src="../../Content/img/gtop.gif" alt="" width="191" height="8"/>
			<div class="title1">Popular Tours</div>
			<ul class="contries">
				<li><a href="#">Argentina</a></li>
				<li><a href="#">Australia</a></li>
				<li><a href="#">Brasilia</a></li>
				<li><a href="#">China</a></li>
				<li><a href="#">France</a></li>
				<li><a href="#">England</a></li>
				<li><a href="#">Jamaica</a></li>
				<li><a href="#">Israel</a></li>
				<li><a href="#">Italy</a></li>
				<li><a href="#">Germany</a></li>
				<li><a href="#">Netherlands</a> <span class="new">- NEW</span></li>
				<li><a href="#">Spain</a></li>
				<li><a href="#">Russia</a> <span class="new">- NEW</span></li>
				<li><a href="#">Peru</a></li>
			</ul>
			<a href="#" class="more">more tours</a>
			<img src="../../Content/img/gbot.gif" alt="" width="191" height="8"/>
		</div>
		<a href="#" class="banner"><img src="../../Content/img/banner.jpg" alt="" width="191" height="346"/></a>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="RightContent" runat="server">

</asp:Content>

