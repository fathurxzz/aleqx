<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Excursions.Models.Excursion>" %>
<div class="block">
    <a href="#">
    <img alt="" src="../../Content/img/img1.jpg" width="180" height="126" /> 
    </a>
    <div>
        <h4><%=Model.Title%></h4>
        <%if (Request.IsAuthenticated){%><%= Html.ActionLink("Редактировать", "AddEdit","Excursions", new { area="Admin", id = Model.Id },new{@class="adminLink"})%><%} %>
        <p><%=Model.ShortDescription%></p>
        <span class="price"><%=Model.Price.ToString()%></span>
        <%=Html.ActionLink("more details", "Details", "Excursions", new { area = "", id = Model.Id }, new { @class = "more" })%>
    </div>
</div>
