<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Babich" %>

<%if(SiteSettings.GetCurrentLanguage=="uk-UA"){ %>
<%=Html.ActionLink("ENG","SetLanguage","Home",new{id="en-US"},null)  %>
<br />
UKR
<%}else{ %>
ENG
<br />
<%=Html.ActionLink("UKR","SetLanguage","Home",new{id="uk-UA"},null)  %>
<%} %>
