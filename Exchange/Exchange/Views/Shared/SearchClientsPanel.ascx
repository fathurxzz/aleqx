<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Exchange.Models" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>
<%
    var client = (Client)ViewData["client"];
%>
<script type="text/javascript">
    ClientsSelector.initialize();
</script>
<%=Html.Hidden("podrid", WebSession.CurrentUser.Podrid) %>
<%=Html.Hidden("subjId", client != null ? client.SubjId.ToString() : "")%>
<%=Html.Hidden("okpo", client != null ? client.ClientCode : "")%>
<%=Html.Hidden("cntrCode", client != null ? client.ContrCode.ToString() : "")%>
<%=Html.Hidden("clientName", client != null ? client.ClientName : "")%>
<p class="title">
    <%=Html.ResourceString("Client")%>
</p>
<p>
    <%=Html.ResourceLabel("Okpo")%>
    <input type="text" id="tOkpo" /><br />
    <span id="sOkpo"><%=Html.Encode(client!=null?client.ClientCode:"") %></span>
</p>
<p>
    <%=Html.ResourceLabel("CntrCode")%>
    <input type="text" id="tCntrCode" /><br />
    <span id="sCntrCode">
        <%=Html.Encode(client!=null?client.ContrCode.ToString():"") %></span>
</p>
<p>
    <%=Html.ResourceLabel("ClientName")%>
    <select id="drClientNames">
    </select><br />
    <span id="sClientName"><%=Html.Encode(client!=null?client.ClientName:"") %></span>
</p>
<div id="notificationContainer">
</div>
<input type="button" id="clientSearchButton" value="<%=Html.ResourceString("Search") %>" />
