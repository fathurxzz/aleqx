<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="bigs.Helpers" %>
<%@ Import Namespace="bigs.Controllers" %>
<%@ Import Namespace="bigs.Models" %>

<div id="buttonContainer">
<%
    using (DataStorage context = new DataStorage())
    {
        List<ButtonStatuses> buttons = (from button in context.ButtonStatuses where button.Language == SystemSettings.CurrentLanguage orderby button.SortOrder ascending select button).ToList();
        foreach (var button in buttons)
        {
            string color = button.SwitchedOn ? "green" : "red";
            Response.Write(Html.ActionLink(" ", "Index", "Home", null, new {@class =  button.Language.Substring(0, 2) + button.Name + color }));
        }
    }
%>
</div>
