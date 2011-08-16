<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    Response.AppendHeader("Content-Type", "application/x-ms-download");
    Response.AppendHeader("Content-Disposition", "attachment; filename=" + ViewData["filename"]);
    Response.AppendHeader("Encoding", "dos-866");
    Response.Clear();
    Response.Write(ViewData["content"]);
%>
