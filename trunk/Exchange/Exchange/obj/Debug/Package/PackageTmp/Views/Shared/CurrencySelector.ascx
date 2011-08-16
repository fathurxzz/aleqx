<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Exchange.Models.Currency>>" %>
<%@ Import Namespace="Exchange.Models" %>
<%if(Model.Count()==0) return;
  var controllerName = (string)ViewContext.RouteData.Values["controller"];
  var actionName = (string)ViewContext.RouteData.Values["action"];
%>
<div class="selector">
Валюта:
    <% foreach (var item in Model){ %>
    <%=Html.ActionLink(item.CurId == 0 ? item.CurName : item.CurId.ToString(), actionName, controllerName, new { curId = item.CurId }, new { @class = "currency c" + item.CurId + " " + (item.Selected ? "selected" : "") })%>
    <%}%>
</div>
