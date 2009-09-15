<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="eShop.Helpers" %>


<%=Html.ResourceActionLink("MainPage","Index","Admin").ToLower() %>
<span style="color: #ffffff"><b>|</b></span>
<%=Html.ResourceActionLink("Categories","Categories","Admin").ToLower() %>

