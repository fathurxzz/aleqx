<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Exchange.Models" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>
<%
    var controllerName = (string)ViewContext.RouteData.Values["controller"];
%>
<script type="text/javascript">

    function processDate() {
        var dateFrom = $("#dateFrom").val();
        var dateTo = $("#dateTo").val();
        location.href = "/<%=controllerName%>?dateFrom=" + dateFrom + "&dateTo=" + dateTo;
    }
</script>
<div>
    <%=Html.ResourceString("Date") %>:
    <input type="text" id="dateFrom" value="<%=WebSession.DateFrom.ToShortDateString()%>" />
    <input type="text" id="dateTo" value="<%=WebSession.DateTo.ToShortDateString()%>" />
    <input type="button" onclick="return processDate();" value=">>>" />
</div>
