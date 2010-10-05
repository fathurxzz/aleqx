<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Excursions.Models.Excursion>" %>
<%@ Import Namespace="Excursions.Helpers" %>
<div class="block">
    <%= Html.CachedImage("~/Content/Images", Model.ImageSource, "thumbnail1",Model.Title)%>
    <div class="innerBlock">
        <h4><%=Html.ActionLink(Model.Title, "Details", "Excursions", new { area = "", id = Model.Id }, new { @class = "h4" })%></h4>
        <%if (Request.IsAuthenticated){%><%= Html.ActionLink("Редактировать", "AddEdit","Excursions", new { area="Admin", id = Model.Id },new{@class="adminLink"})%>
        <%= Html.ActionLink("Удалить", "Delete", "Excursions", new { area = "Admin", id = Model.Id }, new { @class = "adminLink", onclick = "return confirm('При удалении записи будут так же удалены все комментарии к ней. Вы уверены что хотите удалить запись?')" })%><br/>
        <%} %>
            <%=Model.ShortDescription %>
        <div class="line">&nbsp;</div>
        <span class="price">
        <%=(Model.Price!=0? Model.Price.ToString():"&nbsp")%>
        </span>
        <%=Html.ActionLink("подробнее", "Details", "Excursions", new { area = "", id = Model.Id }, new { @class = "more" })%>
    </div>
</div>
