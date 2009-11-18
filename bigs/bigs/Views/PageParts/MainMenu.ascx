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
        %>
        
        <%=Html.ActionLink(" ", "Bigs", "Home", null, new { @class = absolutePath == "/Home/Bigs" ? "bigs bigsActive" : "bigs" })%>
        <%=Html.ActionLink(" ", "Services", "Home", null, new { @class = absolutePath == "/Home/Services" ? "services servicesActive" : "services" })%>
        <%=Html.ActionLink(" ", "Request", "Home", null, new { @class = absolutePath == "/Home/Request" ? "request requestActive" : "request" })%>
        <%=Html.ActionLink(" ", "Capital", "Home", null, new { @class = absolutePath == "/Home/Capital" ? "capital capitalActive" : "capital" })%>
        <%=Html.ActionLink(" ", "Vacancy", "Home", null, new { @class = absolutePath == "/Home/Vacancy" ? "vacancy vacancyActive" : "vacancy" })%>
        <%=Html.ActionLink(" ", "Contacts", "Home", null, new { @class = absolutePath == "/Home/Contacts" ? "contacts contactsActive" : "contacts" })%>
        </div>
    </div>
    <div id="menuRightSide">
    </div>
</div>