<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Exchange.Models.CustomMessage>" %>
<% if(Model==null)return; %>
<div class="UIMessageBox <%=Model.Type.ToString().ToLower()%>">
    <div class="title">
        <%=Model.Title%></div>
    <div class="text">
        <%=Model.Text%></div>
</div>

