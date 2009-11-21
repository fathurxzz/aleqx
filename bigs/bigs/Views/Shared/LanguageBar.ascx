<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="bigs.Helpers" %>
<%@ Import Namespace="bigs.Controllers" %>
<% 
    //string absolutePath = Request.Url.AbsolutePath;
    //string returnUri = Request.Url.AbsoluteUri;
    //string[] separator = new string[] { Request.Url.AbsolutePath };
    //string[] xParameters = returnUri.Split(separator,StringSplitOptions.None);
    //string returnUrl = absolutePath + xParameters[1];
    string controller = (string)ViewContext.RouteData.Values["controller"];
    string contentUrl = (string)ViewContext.RouteData.Values["contentUrl"];
    string currentLanguage = ((string)Session["lang"]) ?? "ru-RU";
    
    
    %>

<div id="languageBarContainer">
    <div id="languageBarLeftSide">
    </div>
    <div id="languageBarCentre">
        <div id="languageBarItems">
        <%
            Response.Write(currentLanguage == "ru-RU" ? "<a class=\"ru ruActive\"></a>" : "<a class=\"ru\" href=\"/Languages/SetLanguage?language=ru-RU&contentController=" + controller + "&contentUrl=" + contentUrl + "\"></a>");
            Response.Write(currentLanguage == "en-US" ? "<a class=\"en enActive\"></a>" : "<a class=\"en\" href=\"/Languages/SetLanguage?language=en-US&contentController=" + controller + "&contentUrl=" + contentUrl + "\"></a>");
            Response.Write(currentLanguage == "it-IT" ? "<a class=\"it itActive\"></a>" : "<a class=\"it\" href=\"/Languages/SetLanguage?language=it-IT&contentController=" + controller + "&contentUrl=" + contentUrl + "\"></a>");
        %>
            
<%--        <%
            Response.Write(currentLanguage == "ru-RU" ? "<a class=\"ru ruActive\"></a>":Html.ResourceActionLink<LanguagesController>(new { @class = "ru" }, c => c.SetRussian(returnUrl)));
            Response.Write(currentLanguage == "en-US" ? "<a class=\"en enActive\"></a>" : Html.ResourceActionLink<LanguagesController>(new { @class = "en" }, c => c.SetEnglish(returnUrl)));
            Response.Write(currentLanguage == "it-IT" ? "<a class=\"it itActive\"></a>" : Html.ResourceActionLink<LanguagesController>(new { @class = "it" }, c => c.SetItalian(returnUrl)));
         %>--%>
        </div>
    </div>
    <div id="languageBarRightSide">
    </div>
</div>
