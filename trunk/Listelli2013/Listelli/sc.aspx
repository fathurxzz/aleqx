﻿<%@ Page Language="C#"%>
<% 
    HttpCookie cookie = new HttpCookie("list2013", "true");
    cookie.Expires = DateTime.Now.AddYears(1);
    Response.Cookies.Add(cookie);
    Response.Redirect("~/Home");    
%>