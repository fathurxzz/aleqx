<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Excursions.Models.Excursion>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">



<h2><%=Model.Title %></h2>
<div class="big_photo">
<img height="217" width="260" alt="" src="../../Content/img/big_photo.jpg" />
</div>
<div class="text">
<%=Model.Text %>
</div>
<%=Html.ActionLink("К списку экскурсий", "Index", "Excursions", null, new { @class = "more" })%>


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



