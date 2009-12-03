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
        <%if (controllerName == "ABOUT"){%><a id="about" class="bigs active"></a><%}else{%><%=Html.ActionLink(" ", "Index", "About", new { contentUrl = Html.ResourceString("About") }, new { id = "about", @class = controllerName == "ABOUT" ? "bigs active" : "bigs" })%><%}%>
        <%if (controllerName == "SERVICES"){%><a id="services" class="<%=shortLang%>services active"></a><%}else{%><%=Html.ActionLink(" ", "Index", "Services", new { contentUrl = Html.ResourceString("Services") }, new { id = "services", @class = controllerName == "SERVICES" ? shortLang + "services active" : shortLang + "services" })%><%}%>
        <%if (controllerName == "REQUESTS"){%><a id="request" class="<%=shortLang%>request active"></a><%}else{%><%=Html.ActionLink(" ", "Index", "Requests", new { contentUrl = Html.ResourceString("Request") }, new { id = "request", @class = controllerName == "REQUESTS" ? shortLang + "request active" : shortLang + "request" })%><%}%>
        <%if (controllerName == "DOCUMENTS"){%><a id="documents" class="<%=shortLang%>documents active"></a><%}else{%><%=Html.ActionLink(" ", "Index", "Documents", new { contentUrl = Html.ResourceString("Documents") }, new { id = "documents", @class = controllerName == "DOCUMENTS" ? shortLang + "documents active" : shortLang + "documents" })%><%}%>
        <%if (controllerName == "VACANCIES"){%><a id="vacancies" class="<%=shortLang%>vacancies active"></a><%}else{%><%=Html.ActionLink(" ", "Index", "Vacancies", new { contentUrl = Html.ResourceString("Vacancies") }, new { id = "vacancies", @class = controllerName == "VACANCIES" ? shortLang + "vacancies active" : shortLang + "vacancies" })%><%}%>
        <%if (controllerName == "CONTACTS"){%><a id="contacts" class="<%=shortLang%>contacts active"></a><%}else{%><%=Html.ActionLink(" ", "Index", "Contacts", new { contentUrl = Html.ResourceString("Contacts") }, new { id = "contacts", @class = controllerName == "CONTACTS" ? shortLang + "contacts active" : shortLang + "contacts" })%><%}%>
        <a href="#" style="height:43px; width:1px; margin-top:-11px;"></a>
        
        </div>
    </div>
    <div id="menuRightSide">
    </div>
</div>