<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Excursions.Models.Excursion>" %>
<%@ Import Namespace="Excursions.Helpers" %>
<div class="block">
    <%= Html.CachedImage("~/Content/Images", Model.ImageSource, "thumbnail1",Model.Title)%>
    <div class="innerBlock">
        <h4><%=Html.ActionLink(Model.Title, "Details", "Excursions", new { area = "", id = Model.Name }, new { @class = "h4" })%></h4>
        <%if (Request.IsAuthenticated){%><%= Html.ActionLink("Edit", "AddEdit","Excursions", new { area="Admin", id = Model.Id },new{@class="adminLink"})%>
        <%= Html.ActionLink("Delete", "Delete", "Excursions", new { area = "Admin", id = Model.Id }, new { @class = "adminLink", onclick = "return confirm('Are you sure?')" })%><br/>
        <%} %>
            <%=Model.ShortDescription %>
        <div class="line">&nbsp;</div>
        <span class="price">
        <%=(Model.Price ?? "&nbsp")%>
        </span>
        <%=Html.ActionLink("more details", "Details", "Excursions", new { area = "", id = Model.Name }, new { @class = "more" })%>
        <div class="separator">&nbsp;</div>
    </div>
</div>

