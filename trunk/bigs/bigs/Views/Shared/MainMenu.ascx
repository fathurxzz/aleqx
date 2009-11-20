<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="bigs.Helpers" %>
<%@ Import Namespace="bigs.Controllers" %>
<div id="menuContainer">
    <div id="menuLeftSide">
    </div>
    
    <div id="menuCentre">
        <div id="menuItems">
        <%
            string absolutePath = Request.Url.AbsolutePath;
            string shortLang = SystemSettings.CurrentLanguageShort;
        %>
        
        <%=Html.ActionLink(" ", "Bigs", "Home", null, new { @class = absolutePath == "/Home/Bigs" ? shortLang + "bigs " + shortLang + "bigsActive" : shortLang + "bigs" })%>
        <%=Html.ActionLink(" ", "Services", "Home", null, new { @class = absolutePath == "/Home/Services" ? shortLang + "services " + shortLang + "servicesActive" : shortLang + "services" })%>
        <%=Html.ActionLink(" ", "Request", "Home", null, new { @class = absolutePath == "/Home/Request" ? shortLang + "request " + shortLang + "requestActive" : shortLang + "request" })%>
        <%=Html.ActionLink(" ", "Capital", "Home", null, new { @class = absolutePath == "/Home/Capital" ? shortLang + "capital " + shortLang + "capitalActive" : shortLang + "capital" })%>
        <%=Html.ActionLink(" ", "Vacancy", "Home", null, new { @class = absolutePath == "/Home/Vacancy" ? shortLang + "vacancy " + shortLang + "vacancyActive" : shortLang + "vacancy" })%>
        <%=Html.ActionLink(" ", "Contacts", "Home", null, new { @class = absolutePath == "/Home/Contacts" ? shortLang + "contacts " + shortLang + "contactsActive" : shortLang + "contacts" })%>
        </div>
    </div>
    <div id="menuRightSide">
    </div>
</div>