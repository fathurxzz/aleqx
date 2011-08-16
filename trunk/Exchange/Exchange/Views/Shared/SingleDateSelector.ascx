<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Exchange.Models" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>
<%
    var controllerName = (string)ViewContext.RouteData.Values["controller"];
%>
<script type="text/javascript">

    function processSingleDate(event) {

        if (event.keyCode == 13) {
            var date = $("#date").val();
            location.href = "/<%=controllerName%>?date=" + date;
        }
    }
</script>
<div>
    <%=Html.ResourceString("Date") %>:
    <input type="text" id="date" onkeydown="processSingleDate(event)" value="<%=WebSession.Date.ToShortDateString()%>" />
</div>
