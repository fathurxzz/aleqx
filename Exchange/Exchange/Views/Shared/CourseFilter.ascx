<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<SelectListItem>>" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>

<%
    var controllerName = (string)ViewContext.RouteData.Values["controller"];
%>
<script type="text/javascript">

    function processCourse() {
        var course = $("#courseFilter").val();
        location.href = "/<%=controllerName%>?course=" + course.replace(",",".");
    }
</script>


<div>
    <%=Html.ResourceString("Course")%>: <%=Html.DropDownList("courseFilter", Model, new { onchange = "return processCourse()" })%>
</div>

