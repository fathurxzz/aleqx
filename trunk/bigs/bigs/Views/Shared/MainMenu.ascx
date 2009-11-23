<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="bigs.Helpers" %>
<%@ Import Namespace="bigs.Controllers" %>
<script src="../../Scripts/jquery.ifixpng.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function() {
    //$('#nice').ifixpng();
    });
</script>

<div id="menuContainer">
    <div id="menuLeftSide">
    </div>
    
    <div id="menuCentre">
        <div id="menuItems">
        <%
            string shortLang = SystemSettings.CurrentLanguageShort;
            string controllerName = ViewContext.RouteData.Values["controller"].ToString().ToUpperInvariant();
        %>
        
        
        
        <%=Html.ActionLink(" ", "Index", "About", new { contentUrl = Html.ResourceString("About") }, new { id = "about", @class = controllerName == "ABOUT" ? "bigs bigsActive" : "bigs" })%>
        <%=Html.ActionLink(" ", "Index", "Services", new { contentUrl = Html.ResourceString("Services") }, new { id = "services", @class = controllerName == "SERVICES" ? shortLang + "services " + shortLang + "servicesActive" : shortLang + "services" })%>
        <%=Html.ActionLink(" ", "Index", "Requests", new { contentUrl = Html.ResourceString("Request") }, new { id = "request", @class = controllerName == "REQUESTS" ? shortLang + "request " + shortLang + "requestActive" : shortLang + "request" })%>
        <%=Html.ActionLink(" ", "Index", "Documents", new { contentUrl = Html.ResourceString("Documents") }, new { id = "documents", @class = controllerName == "DOCUMENTS" ? shortLang + "documents " + shortLang + "documentsActive" : shortLang + "documents" })%>
        <%=Html.ActionLink(" ", "Index", "Vacancies", new { contentUrl = Html.ResourceString("Vacancies") }, new { id = "vacancies", @class = controllerName == "VACANCIES" ? shortLang + "vacancies " + shortLang + "vacanciesActive" : shortLang + "vacancies" })%>
        <%=Html.ActionLink(" ", "Index", "Contacts", new { contentUrl = Html.ResourceString("Contacts") }, new { id = "contacts", @class = controllerName == "CONTACTS" ? shortLang + "contacts " + shortLang + "contactsActive" : shortLang + "contacts" })%>
 
        
        </div>
    </div>
    <div id="menuRightSide">
    </div>
</div>