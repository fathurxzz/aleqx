<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="bigs.Helpers" %>
<%@ Import Namespace="bigs.Controllers" %>
<script type="text/javascript">
    $(function() {
    $('#menuItems a').ifixpng();
    });
</script>


<div id="nice"></div>

<div id="menuContainer">
    <div id="menuLeftSide">
    </div>
    
    <div id="menuCentre">
        <div id="menuItems">
        <%
            string shortLang = SystemSettings.CurrentLanguageShort;
            string controllerName = ViewContext.RouteData.Values["controller"].ToString().ToUpperInvariant();
        %>
        
        
        <%=Html.ActionLink(" ", "Index", "About", new { contentUrl = Html.ResourceString("About") }, new { id = "about", @class = controllerName == "ABOUT" ? "bigs active" : "bigs" })%>
        <%=Html.ActionLink(" ", "Index", "Services", new { contentUrl = Html.ResourceString("Services") }, new { id = "services", @class = controllerName == "SERVICES" ? shortLang + "services active" : shortLang + "services" })%>
        <%=Html.ActionLink(" ", "Index", "Requests", new { contentUrl = Html.ResourceString("Request") }, new { id = "request", @class = controllerName == "REQUESTS" ? shortLang + "request active" : shortLang + "request" })%>
        <%=Html.ActionLink(" ", "Index", "Documents", new { contentUrl = Html.ResourceString("Documents") }, new { id = "documents", @class = controllerName == "DOCUMENTS" ? shortLang + "documents active" : shortLang + "documents" })%>
        <%=Html.ActionLink(" ", "Index", "Vacancies", new { contentUrl = Html.ResourceString("Vacancies") }, new { id = "vacancies", @class = controllerName == "VACANCIES" ? shortLang + "vacancies active" : shortLang + "vacancies" })%>
        <%=Html.ActionLink(" ", "Index", "Contacts", new { contentUrl = Html.ResourceString("Contacts") }, new { id = "contacts", @class = controllerName == "CONTACTS" ? shortLang + "contacts active" : shortLang + "contacts" })%>
        <a href="#" style="height:43px; width:1px; margin-top:-11px;"></a>
        
        </div>
    </div>
    <div id="menuRightSide">
    </div>
</div>