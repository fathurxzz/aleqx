<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="ViaCon.Models" %>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<%
    var contentId = (string)ViewData["contentId"];
    Html.RenderAction<ViaCon.Controllers.ContentController>(a => a.HorisontalMenuLine(contentId, 0,false, null, contentId));
%>
