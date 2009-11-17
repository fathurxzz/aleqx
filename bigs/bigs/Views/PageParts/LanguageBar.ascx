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
    
    <div id="languageBarCentre">
        <div id="languageBarItems">
        
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
 
 <%
     %>
        <a class="<%=ruClassname %>" href=""></a>
        <a class="<%=enClassname %>" href=""></a>
        <a class="<%=itClassname %>" href=""></a>
        </div>
    </div>
    <div id="languageBarRightSide">
    </div>
</div>