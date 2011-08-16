<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Exchange.Models.Status>>" %>
<%@ Import Namespace="Exchange.Models" %>
<%if (Model.Count() == 0) return;
  var controllerName = (string)ViewContext.RouteData.Values["controller"];
    %>

<div class="selector">
    Статус:
        <%foreach (var item in Model)
          {%>
            <%=Html.ActionLink(item.Name, "Index", controllerName, new { status = item.Id }, new { @class = (item.Selected ? "selected" : "") })%>
        <%}%>
</div>
