<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>

<div class="selectorContainer filterSelector resetSelector">
<%=Html.ResourceActionLink("ResetAllFilters", "Index", new { curId = 0, status = -100, operId = 0,dateFrom=DateTime.Now.Date.ToShortDateString(),dateTo = DateTime.Now.Date.ToShortDateString() }, null) %>
</div>