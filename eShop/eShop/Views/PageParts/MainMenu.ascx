<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="eShop.Helpers" %>


<%=Html.ResourceActionLink("MainPage","Index","Home").ToLower() %>
<span style="color: #ffffff"><b>|</b></span>
<%=Html.ResourceActionLink("News","News","Home").ToLower() %>
