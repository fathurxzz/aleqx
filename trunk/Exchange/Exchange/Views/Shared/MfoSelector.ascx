<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Exchange.Models" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>
<%
    var controllerName = (string)ViewContext.RouteData.Values["controller"];
%>
<script type="text/javascript">

    function generateLinkPdrid(event) {
        if (event.keyCode == 13) {
            var podrid = $("#podrid").val();
            location.href = "/<%=controllerName%>?podrid=" + podrid;
        }
    }
</script>

<div>
<%=Html.ResourceString("Podrid") %>:
    <input type="text" id="podrid" onkeydown="generateLinkPdrid(event)"  value="<%=WebSession.Podrid%>" />
</div>