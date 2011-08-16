<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Exchange.Models.Recepient>>" %>
<%@ Import Namespace="Exchange.Models" %>
<table id="recList">
    <% foreach (var item in Model)
       { %>
    <tr id="<%=item.Id%>" class="recipientItem" recipientname="<%=(item.Type==RecipientType.User?item.Name:item.OfficeName)%>"
        type="<%=(int)item.Type%>">
        <td>
            <%= Html.Encode(item.Name) %> - <%= Html.Encode(item.OfficeName) %>
        </td>
    </tr>
    <% } %>
</table>
<script type="text/javascript">
    $(function () {
        $(".recipientItem").click(function () {
            $("#recipientList").html("");
            var recipientId = $(this).attr("id");


            var recipientName = $(this).attr("recipientName");

            var recipientType = $(this).attr("type");

            var recipientsIds = $("#recipientsIds").val();
            var officeIds = $("#officeIds").val();

            if (recipientType == 1)
                recipientsIds = recipientsIds + recipientId + ",";
            else if (recipientType == 2)
                officeIds = officeIds + recipientId + ",";

            $("#recipientsIds").val(recipientsIds);
            $("#officeIds").val(officeIds);

            var recepients = $("#recipients").html();
            recepients = recepients + "<div class=\"recItem\" onclick=\"removeRec(" + recipientType + ",this," + recipientId + ")\">" + recipientName + "</div>";
            $("#tRecipient").val("");
            $("#recipients").html(recepients);
            $("#tRecipient").focus();
            $.fancybox.resize();
        });
    });

    function removeRec(recType, obj, id) {
        if (recType == 1) {
            var recipientsIds = $("#recipientsIds").val();
            recipientsIds = recipientsIds.replace(id + ",", "");
            $("#recipientsIds").val(recipientsIds);
        }
        else if (recType == 2) {
            var officeIds = $("#officeIds").val();
            officeIds = officeIds.replace(id + ",", "");
            $("#officeIds").val(officeIds);
        }
        $(obj).css("display", "none");
        $.fancybox.resize();
    }
</script>
