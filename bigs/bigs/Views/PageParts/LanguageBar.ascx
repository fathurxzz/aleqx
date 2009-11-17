<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="bigs.Helpers" %>
<%@ Import Namespace="bigs.Controllers" %>
<% 
    string returnUrl = Request.Url.AbsolutePath;
    string currentLanguage = ((string)Session["lang"]) ?? "ru-RU";
    
    %>

<div id="languageBarContainer">
    <div id="languageBarLeftSide">
    </div>
    <%
        string classname = string.Empty;
        string ruClassname = "ru";
        string enClassname = "en";
        string itClassname = "it";
        switch (currentLanguage)
        {
            case "ru-RU":
                ruClassname += " ruActive";
                break;
            case "en-EN":
                enClassname += " enActive";
                break;
            case "it-IT":
                itClassname += " itActive";
                break;
        }
    %>
    <div id="languageBarCentre">
        <div id="languageBarItems">
        <%=Html.RActionLink<HomeController>(new { @class = ruClassname }, c => c.SetRussian(returnUrl))%>
        <%=Html.RActionLink<HomeController>(new { @class = enClassname }, c => c.SetEnglish(returnUrl))%>
        <%=Html.RActionLink<HomeController>(new { @class = itClassname }, c => c.SetItalian(returnUrl))%>
        </div>
    </div>
    <div id="languageBarRightSide">
    </div>
</div>