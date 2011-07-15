<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Dev.Mvc.Helpers" %>
<div id="mainMenu">


<%if (ViewData["contentType"] != null && (int)ViewData["contentType"] == 1)
  { %>
  <div class="sites"></div>
<% }
  else
  { %>
<a href="/<%=SiteHelper.GetPathByContentType(1)%>" class="sites"></a>
<% } %>


<%if (ViewData["contentType"] != null && (int)ViewData["contentType"] == 2)
  { %>
  <div class="firmStyle"></div>
<% }
  else
  { %>
<a href="/<%=SiteHelper.GetPathByContentType(2)%>" class="firmStyle"></a>
<% } %>

<%if (ViewData["contentType"] != null && (int)ViewData["contentType"] == 3)
  { %>
  <div class="advDesign"></div>
<% }
  else
  { %>
<a href="/<%=SiteHelper.GetPathByContentType(3)%>" class="advDesign"></a>
<% } %>
</div>
