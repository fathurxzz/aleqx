<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Exchange.Models.Message>>" %>

<%@ Import Namespace="Exchange.Models" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2>
            <%=ViewData["userName"]%></h2>
        <div class="officeNameTitle">
            <%=ViewData["officeName"]%>
        </div>
        <div class="clear">
        </div>
    </div>
    <div>
        <% foreach (var item in Model)
           { %>
        <div class="userMessageListItem">
            <div>
                <div class="userName">
                    <%=item.UserName%>
                </div>
                <div class="officeName">
                    <%=item.OfficeName %>
                </div>
                <div class="clear">
                </div>
            </div>
            <div>
                <div class="messageText">
                    <%=item.Text %>
                </div>
                <div class="date">
                    <%=(item.Date.Date==DateTime.Now.Date? item.Date.ToShortTimeString():item.Date.ToShortDateString()) %>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
        <% } %>
    </div>
    <div class="buttonsContainer">
        <span class="UIButton">
            <%= Html.ResourceActionLink("Back", "Index", "Conversations")%>
        </span>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
    <%=Html.RegisterCss("Conversations.css")%>
</asp:Content>
