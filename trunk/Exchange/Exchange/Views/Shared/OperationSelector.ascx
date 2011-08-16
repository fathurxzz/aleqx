<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Exchange.Models.Oper>>" %>
<%@ Import Namespace="Exchange.Models" %>
<%if(Model.Count()==0) return;
  var controllerName = (string)ViewContext.RouteData.Values["controller"];
  var actionName = (string)ViewContext.RouteData.Values["action"];
%>
<div class="selector">
    Операция:
        <%
            foreach (var operation in Model)
            {
        %>
            <%=Html.ActionLink(operation.OperName, actionName, controllerName, new { operId = (int)operation.Operation }, new { @class = (WebSession.Operation == operation.Operation ? "selected" : "") })%>
        <%
        }
        %>
</div>
