<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Dev.Mvc.Helpers" %>
<div id="mainMenu">
<a href="/<%=SiteHelper.GetPathByContentType(1) %>" id="sites"></a>
<a href="/<%=SiteHelper.GetPathByContentType(2) %>" id="firmStyle"></a>
<a href="/<%=SiteHelper.GetPathByContentType(3) %>" id="advDesign"></a>
</div>
