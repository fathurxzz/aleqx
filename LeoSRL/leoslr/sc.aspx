<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sc.aspx.cs" Inherits="Leo.sc" %>

<%
    HttpCookie cookie = new HttpCookie("leo", "true");
    cookie.Expires = DateTime.Now.AddYears(1);
    Response.Cookies.Add(cookie);
    Response.Redirect("~/");
%>
