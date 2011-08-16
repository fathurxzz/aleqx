<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Exchange.Models.UserMessage>" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>
<% if (Model == null) return; %>
<div class="UIMessageBox <%=Model.Type.ToString().ToLower()%>">
    <div class="title">
        <%=Model.Title%>
    </div>
    <div class="officeName">
        <%=Model.OfficeName%>
    </div>
    <div class="userName">
        <%=Model.UserName%>
    </div>
    <div class="content">
        <%=Model.Text%>
        <%=Html.ResourceActionLink("Details","Details","Conversations",new{id=Model.UserId},null) %>
    </div>
</div>
