<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Excursions.Models.Excursion>" %>
<div class="block">
    <a href="#">
    <img alt="" src="../../Content/img/img1.jpg" width="180" height="126" /> 
    </a>
    <div>
        <h4><%=Model.Title%></h4>
        <%if (Request.IsAuthenticated){%><%= Html.ActionLink("Редактировать", "AddEdit","Excursions", new { area="Admin", id = Model.Id },new{@class="adminLink"})%><%} %>
        <p><%=Model.Text%></p>
        <span class="price"><%=Model.Price.ToString()%></span>
        <a class="more" href="#">more details</a>
    </div>
</div>
