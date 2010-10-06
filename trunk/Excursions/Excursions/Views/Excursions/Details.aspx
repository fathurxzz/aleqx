<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Excursions.Models.Excursion>" %>
<%@ Import Namespace="Excursions.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">



<h2><%=Model.Title %></h2>
<div class="big_photo">
<%= Html.CachedImage("~/Content/Images", Model.ImageSource, "thumbnail1",Model.Title)%>

</div>
<div class="text">
<%=Model.Text %>
<br />
<%=Html.ActionLink("Back to list", "Index", "Excursions", null, new { @class = "more" })%>
</div>



<div class="comments">
Comments:
    <%

    var comments = Model.Comments.Select(c => c).OrderBy(c => c.Date).ToList();
            
    foreach (var item in comments)
    {
        Html.RenderPartial("Comment", item);
    } 
    %>
</div>

 <div id="collapsibleLink">
    <a href="#"  onclick="showCollapsibleBox()">Add comment</a>

    <br />
    <a id="feedbackForm" class="iframe" href="/Home/FeedbackForm">Форма обратной связи</a>
    </div>

<%
    Html.RenderPartial("AddComment", Model);
%>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
<script type="text/javascript">
    function showCollapsibleBox() {

        $("#collapsibleLink").css("display", "none");

        $("#addComment").slideDown("fast");
    }
    </script>



    <script type="text/javascript" src="/Scripts/jquery-1.4.1.js"></script>
    <%= Ajax.DynamicCssInclude("/Content/fancybox/jquery.fancybox.css") %>
    <%= Ajax.ScriptInclude("/Scripts/jquery.fancybox.js")%>
    <script type="text/javascript">
        $(function () {
            $("#feedbackForm").fancybox(
            {
                hideOnContentClick: false,
                hideOnOverlayClick: false,
                showCloseButton: true
            });
        });
    </script>

</asp:Content>



