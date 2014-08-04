<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sc.aspx.cs" Inherits="Shop.WebSite.sc" %>

<%
    HttpCookie cookie = new HttpCookie("active-land","true");
    cookie.Expires = DateTime.Now.AddYears(1);
    Response.Cookies.Add(cookie);
    Response.Redirect("~/");
%>
