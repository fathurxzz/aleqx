<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="bigs.Helpers" %>
<%@ Import Namespace="bigs.Controllers" %>
<% 
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
            
                //if (Request.IsAuthenticated)
                //{
                Response.Write(currentLanguage == "ru-RU" ? "<a class=\"ru ruActive\"></a>" : "<a class=\"ru\" href=\"" + "/Languages/SetLanguage?language=ru-RU&contentController=" + controller + "&contentUrl=" + HttpUtility.UrlPathEncode(contentUrl) + "\"></a>");
                Response.Write(currentLanguage == "en-US" ? "<a class=\"en enActive\"></a>" : "<a class=\"en\" href=\"" + "/Languages/SetLanguage?language=en-US&contentController=" + controller + "&contentUrl=" + HttpUtility.UrlPathEncode(contentUrl) + "\"></a>");
                Response.Write(currentLanguage == "it-IT" ? "<a class=\"it itActive\"></a>" : "<a class=\"it\" href=\"" + "/Languages/SetLanguage?language=it-IT&contentController=" + controller + "&contentUrl=" + HttpUtility.UrlPathEncode(contentUrl) + "\"></a>");
                //}
                //else
                //{
                //    Response.Write("<a class=\"ru ruActive\"></a>");
                //    Response.Write("<a class=\"en\"></a>");
                //    Response.Write("<a class=\"it\"></a>");
                //}
            
            %>
        </div>
    </div>
    <div id="languageBarRightSide">
    </div>
</div>
