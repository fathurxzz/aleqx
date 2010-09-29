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
<%=Html.ActionLink("К списку экскурсий", "Index", "Excursions", null, new { @class = "more" })%>
</div>



<div class="comments">
Комментарии:
    <%

    var comments = Model.Comments.Select(c => c).OrderBy(c => c.Date).ToList();
            
    foreach (var item in comments)
    {
        Html.RenderPartial("Comment", item);
    } 
    %>
</div>


<%
    Html.RenderPartial("AddComment", Model);
%>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>



