<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Excursions.Models.Excursion>" %>
<%@ Import Namespace="Excursions.Helpers" %>
<div class="block">
    <%= Html.CachedImage("~/Content/Images", Model.ImageSource, "thumbnail1",Model.Title)))%>
    <div class="innerBlock">
        <h4><%=Model.Title%></h4>
        <%if (Request.IsAuthenticated){%><%= Html.ActionLink("Редактировать", "AddEdit","Excursions", new { area="Admin", id = Model.Id },new{@class="adminLink"})%><br/><%} %>
        
        
            <%=Model.ShortDescription %>
        
        <div class="line">&nbsp;</div>
        <span class="price"><%=Model.Price.ToString()%></span>
        <%=Html.ActionLink("подробнее", "Details", "Excursions", new { area = "", id = Model.Id }, new { @class = "more" })%>
    </div>
</div>
