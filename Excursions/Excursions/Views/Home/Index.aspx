<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ContentPlaceHolderID="LeftContent" runat="server">
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

<asp:Content ContentPlaceHolderID="RightContent" runat="server">
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



<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    		<div class="welcome">
			<p>Dolor sit amet, consetetur sadipscing elitr, seddiam nonumy 
eirmod tempor. invidunt ut labore et dolore magna aliquyam erat, sed 
diam voluptua. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, 
sed diam nonumy eirmod tempor invidunt ut labore et dolore magna 
aliquyam erat, sed diam voluptua. Lorem ipsum dolor sit amet, consetetur
sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et 
dolore magna aliquyam erat, sed diam voluptua. Lorem ipsum dolor sit 
amet, consetetur. </p>																																																	<div class="inner_copy"><a href="http://www.bestfreetemplates.org/">free templates</a><a href="http://www.bannermoz.com/">banner templates</a></div>
		</div>
		<div class="search">
			<form action="#">
				<span>Search</span> <input type="text"/> <a href="#" id="ok"><img src="../../Content/img/button.jpg" alt="" width="45" height="24"/></a>
			</form>
		</div>
		<div class="block">
			<a href="#"><img src="../../Content/img/img1.jpg" alt="" width="180" height="126"/></a>
			<div>
				<h4>Lorem ipsum dolor sit amet</h4>
				<p>Dolor sit amet, consetetur sadipscing elitr, seddiam nonumy 
eirmod tempor. invidunt ut labore et dolore magna aliquyam erat, sed 
diam voluptua. Lorem ipsum dolor sit amet, consetetur sadipscing elitr.</p>
				<span class="price">$1500</span>
				<a href="#" class="more">more details</a>
			</div>
		</div>
		<div class="block">
			<a href="#"><img src="../../Content/img/img2.jpg" alt="" width="180" height="126"/></a>
			<div>
				<h4>Lorem ipsum dolor sit amet</h4>
				<p>Dolor sit amet, consetetur sadipscing elitr, seddiam nonumy 
eirmod tempor. invidunt ut labore et dolore magna aliquyam erat, sed 
diam voluptua. Lorem ipsum dolor sit amet, consetetur sadipscing elitr.</p>
				<span class="price">$2500</span>
				<a href="#" class="more">more details</a>
			</div>
		</div>
		<div class="block">
			<a href="#"><img src="../../Content/img/img3.jpg" alt="" width="180" height="126"/></a>
			<div>
				<h4>Lorem ipsum dolor sit amet</h4>
				<p>Dolor sit amet, consetetur sadipscing elitr, seddiam nonumy 
eirmod tempor. invidunt ut labore et dolore magna aliquyam erat, sed 
diam voluptua. Lorem ipsum dolor sit amet, consetetur sadipscing elitr.</p>
				<span class="price">$3500</span>
				<a href="#" class="more">more details</a>
			</div>
		</div>
</asp:Content>
