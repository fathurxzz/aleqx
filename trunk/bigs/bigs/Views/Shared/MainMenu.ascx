<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
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
        %>
        
        <%=Html.ActionLink(" ", "Index", "Services", new { contentUrl = Html.ResourceString("About") }, new { @class = xPath == Html.ResourceString("About") ? "bigs bigsActive" : "bigs" })%>
        <%=Html.ActionLink(" ", "Index", "Services", new { contentUrl = Html.ResourceString("Service") }, new { @class = xPath == Html.ResourceString("Service") ? shortLang + "services " + shortLang + "servicesActive" : shortLang + "services" })%>
        <%=Html.ActionLink(" ", "Index", "Services", new { contentUrl = Html.ResourceString("Request") }, new { @class = xPath == Html.ResourceString("Request") ? shortLang + "request " + shortLang + "requestActive" : shortLang + "request" })%>
        <%=Html.ActionLink(" ", "Index", "Services", new { contentUrl = Html.ResourceString("Capital") }, new { @class = xPath == Html.ResourceString("Capital") ? shortLang + "capital " + shortLang + "capitalActive" : shortLang + "capital" })%>
        <%=Html.ActionLink(" ", "Index", "Services", new { contentUrl = Html.ResourceString("Vacancy") }, new { @class = xPath == Html.ResourceString("Vacancy") ? shortLang + "vacancy " + shortLang + "vacancyActive" : shortLang + "vacancy" })%>
        <%=Html.ActionLink(" ", "Index", "Services", new { contentUrl = Html.ResourceString("Contacts") }, new { @class = xPath == Html.ResourceString("Contacts") ? shortLang + "contacts " + shortLang + "contactsActive" : shortLang + "contacts" })%>
        </div>
    </div>
    <div id="menuRightSide">
    </div>
</div>