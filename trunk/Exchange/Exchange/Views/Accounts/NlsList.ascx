<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Exchange.Models.Account>>" %>

<%
foreach (var item in Model)
{
    Response.Write("<option value=\"" + item.Nls + "\">" + item.Nls + "</option>");
}
%>
