<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Exchange.Models.Message>>" %>

<%@ Import Namespace="Exchange.Models.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2>
            <%=Html.ResourceString("Messages") %></h2>
        <div class="newMessageLink">
            <%=Html.ResourceActionLink("NewMessage","SendMessage","Conversations",null,new{@class="fancy"}) %>
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
                    <%if (item.UnreadCount > 0)
                      { %>
                    <span class="unreadCnt">(<%=item.UnreadCount %>)</span>
                    <% } %>
                    <%=Html.ActionLink(item.UserName, "Details", "Conversations", new { id = item.UserId }, new { @class = "userName" })%>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
    <%=Html.RegisterCss("Conversations.css")%>
    <%=Html.RegisterJS("jquery.fancybox.js")%>
    <%=Html.RegisterCss("fancybox/jquery.fancybox-1.3.1.css")%>
    
    <script type="text/javascript">
            $(function () {
                $(".fancy").fancybox(
            {
                scrolling: 'no',
                hideOnContentClick: false,
                hideOnOverlayClick: false,
                showCloseButton: true,
                autoScale:true,
                autoDimensions:true
            });
            });
    </script>
    
</asp:Content>
