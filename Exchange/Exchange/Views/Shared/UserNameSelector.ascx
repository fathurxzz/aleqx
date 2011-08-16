<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Exchange.Models" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>
<%
    var controllerName = (string)ViewContext.RouteData.Values["controller"];
%>
<script type="text/javascript">

    function generateLinkUserName(event) {
        if (event.keyCode == 13) {
            var userName = $("#userName").val();
            location.href = "/<%=controllerName%>?username=" + userName;
        }
    }
</script>

<div>
<%=Html.ResourceString("UserName") %>:
    <input type="text" id="userName" onkeydown="generateLinkUserName(event)"  value="<%=WebSession.UserName%>" />
</div>
