<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Excursions.Models.Content>" %>
<%@ Import Namespace="Excursions.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Kyiv Tours - Contacts
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%
    if(Request.IsAuthenticated)
    {
    %>
    
    <%=Html.ActionLink("Edit page", "Update", "Contacts", new { area = "Admin" }, new { @class = "adminLink" })%>
    <%    
        
    } %>

    <%=Model.Text %>
    
    <a id="feedbackForm" class="iframe" href="/Home/FeedbackForm">Leave a message</a>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
    <script type="text/javascript" src="/Scripts/jquery-1.4.1.js"></script>
    <%= Ajax.DynamicCssInclude("/Content/fancybox/jquery.fancybox.css") %>
    <%= Ajax.ScriptInclude("/Scripts/jquery.fancybox.js")%>
    <script type="text/javascript">
        $(function () {
            $("#feedbackForm").fancybox(
            {
                scrolling:'no',
                hideOnContentClick: false,
                hideOnOverlayClick: false,
                showCloseButton: true
            });
        });
    </script>
</asp:Content>
