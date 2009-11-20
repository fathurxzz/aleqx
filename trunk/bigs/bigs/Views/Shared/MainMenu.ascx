﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="bigs.Helpers" %>
<%@ Import Namespace="bigs.Controllers" %>
<div id="menuContainer">
    <div id="menuLeftSide">
    </div>
    
    <div id="menuCentre">
        <div id="menuItems">
        <%
            
            string[] separator = new string[] { "contentUrl=" };
            string[] xParameters = Request.Url.AbsoluteUri.Split(separator, StringSplitOptions.None);
            string xPath = string.Empty;
            if (xParameters.Length > 1)
                xPath = xParameters[1];
            string shortLang = SystemSettings.CurrentLanguageShort;
            string controllerName = ViewContext.RouteData.Values["controller"].ToString().ToUpperInvariant();
        %>
        
        
        
        <%=Html.ActionLink(" ", "Index", "About", new { contentUrl = Html.ResourceString("About") }, new { @class = controllerName == "ABOUT" ? "bigs bigsActive" : "bigs" })%>
        <%=Html.ActionLink(" ", "Index", "Service", new { contentUrl = Html.ResourceString("Service") }, new { @class = controllerName == "SERVICE" ? shortLang + "services " + shortLang + "servicesActive" : shortLang + "services" })%>
        <%=Html.ActionLink(" ", "Index", "Requests", new { contentUrl = Html.ResourceString("Request") }, new { @class = controllerName == "REQUESTS" ? shortLang + "request " + shortLang + "requestActive" : shortLang + "request" })%>
        <%=Html.ActionLink(" ", "Index", "Documents", new { contentUrl = Html.ResourceString("Capital") }, new { @class = controllerName == "DOCUMENTS" ? shortLang + "capital " + shortLang + "capitalActive" : shortLang + "capital" })%>
        <%=Html.ActionLink(" ", "Index", "Vacancies", new { contentUrl = Html.ResourceString("Vacancy") }, new { @class = controllerName == "VACANCIES" ? shortLang + "vacancy " + shortLang + "vacancyActive" : shortLang + "vacancy" })%>
        <%=Html.ActionLink(" ", "Index", "Contacts", new { contentUrl = Html.ResourceString("Contacts") }, new { @class = controllerName == "CONTACTS" ? shortLang + "contacts " + shortLang + "contactsActive" : shortLang + "contacts" })%>
 
        
        </div>
    </div>
    <div id="menuRightSide">
    </div>
</div>