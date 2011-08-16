<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<Exchange.Models.CustomMessage>>" %>
<%@ Import Namespace="Exchange.Models" %>
<%if (Model != null && Model.Count() > 0)
  {
      foreach (MessageType i in Enum.GetValues(typeof (MessageType)))
      {
          var messages = Model.Where(m => m.Type == i).ToList();
          if (messages.Count() > 0)
          {
    %>
    <div class="UIMessageBox <%=i.ToString().ToLower()%>">
    <%
              foreach (var message in messages)
              {
    %>
    <div class="title">
        <%=message.Title%></div>
    <div class="text">
        <%=message.Text%></div>

    <%
              }
    %>
    </div>
    <%
          }
      }
  }
%>    