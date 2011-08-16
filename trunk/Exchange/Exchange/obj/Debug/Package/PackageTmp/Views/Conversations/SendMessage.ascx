<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>
<script type="text/javascript">
    ExchangeScripts.initialize();
</script>
<h2>
    <%=Html.ResourceString("NewMessage") %></h2>
<%using (Html.BeginForm())
  {%>
<%=Html.Hidden("recipientsIds") %>
<%=Html.Hidden("officeIds")%>
<br />
<br />
<div>
    <div id="recipients">
    </div>
    <div class="clear">
    </div>
    <table id="sendMessageTable">
        <tr>
            <td>
                Кому:
            </td>
            <td style="text-align: left">
                <%=Html.TextBox("tRecipient", "", new { autocomplete ="off", style="width:500px"})%>
                <div id="recipientList">
                </div>
            </td>
        </tr>
        <tr>
            <td>
                Текст:
            </td>
            <td style="text-align: left">
                <%=Html.TextArea("taText","",7,60,null)%>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div class="buttonsContainer">
                    <%=Html.ResourceSubmitButton("Send")%>
                    <%= Html.ResourceButton("Back", "javascript:$.fancybox.close();")%>
                </div>
            </td>
        </tr>
    </table>
</div>
<% } %>
