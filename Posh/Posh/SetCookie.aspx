<%@ Page Language="C#"%>
<% 
    HttpCookie cookie = new HttpCookie("posh", "true");
    cookie.Expires = DateTime.Now.AddYears(1);
    Response.Cookies.Add(cookie);
    Response.Redirect("~/");    
%>
