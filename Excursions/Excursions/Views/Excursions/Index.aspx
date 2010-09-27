<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Excursions.Models.Excursion>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% if (Model != null)
           foreach (var item in Model)
           {
               Html.RenderPartial("Excursion", item);
           } %>
    <%
        if (Request.IsAuthenticated)
        {
    %>
    <%= Html.ActionLink("Создать", "AddEdit", "Excursions", new { area = "Admin" }, new { @class = "adminLink" })%>
    <%
        } %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="LeftContent" runat="server">
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
		<a href="#" class="banner"><img src="../../Content/img/banner.jpg" alt="" width="191" height="346"/></a></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="RightContent" runat="server">
		<div class="right_block">
			<p class="title2">Latest News</p>
			<div class="item">
				<span>26 june</span>
				<p>Dolor sit amet, consetetur sadipscing elitr, seddiam nonumy 
eirmod tempor. invidunt ut labore et dolore magna aliquyam erat, sed 
diam voluptua. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, 
sed diam nonumy eirmod tempor invidunt ut labore et dolore magna 
aliquyam erat, sed diam voluptua. </p>
				<a href="#">read more</a>
			</div>
			<img src="../../Content/img/right_bot.gif" alt="" width="261" height="21"/>
		</div>
		<div class="right_block">
			<p class="title3">Contest Photos</p>
			<div class="item">
				<div class="photo"><img src="../../Content/img/photo.jpg" alt="" width="197" height="148"/></div>
				<p class="name"><u>Martin Bishop</u> - <strong>The Name Photo</strong></p>
				<a href="#" class="details">more info</a>
			</div>
			<img src="../../Content/img/right_bot.gif" alt="" width="261" height="21"/>
		</div>
</asp:Content>
