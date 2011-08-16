<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Exchange.Models.OperationSign>>" %>
<%@ Import Namespace="Exchange.Models" %>
<%if (Model.Count() == 0) return;
  var controllerName = (string)ViewContext.RouteData.Values["controller"];
%>
<div class="selector">
    Признак операции:
        <%
            foreach (var operSign in Model)
            {
        %>
            <%=Html.ActionLink(operSign.OperSignName, "Index", controllerName, new { operSign = (int)operSign.OperSign }, new { @class = (WebSession.OperSign == operSign.OperSign ? "selected" : "") })%>
        <%
            }
        %>
</div>
