<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Exchange.Models.Order>>" %>

<%@ Import Namespace="Exchange.Models" %>
<%@ Import Namespace="Exchange.Models.Enum" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=Html.ResourceString("Orders") %></h2>
    <% using (Html.BeginForm())
       {
           var currentOper = EOperation.Buy;
    %>
    
    <table class="filterContainer">
    <tr>
        <td colspan="2">Покупка</td>
        <td colspan="2">Продажа</td>
    </tr>
    <tr>
        <td>Сумма валюты <%=Html.TextBox("totalDocSumBuy") %></td>
        <td>Эквивалент  <%=Html.TextBox("totalEquivBuy") %></td>
        <td>Сумма валюты <%=Html.TextBox("totalDocSumSell") %></td>
        <td>Эквивалент  <%=Html.TextBox("totalEquivSell") %></td>
    </tr>
    </table>
    <table class="filterContainer">
        <tr>
            <td>
                <div class="selectors">
                    выбрать: 
                    <a href="#" id="checkAllOrders">[все]</a> 
                    <a href="#" id="checkNothing">[ни одного]</a>
                    <a href="#" id="checkBuy">[покупка]</a> 
                    <a href="#" id="checkSell">[продажа]</a> 
                    <a href="#" id="checkCreated">[сформированные]</a>
                </div>
            </td>
            <td>
                <%Html.RenderPartial("CurrencySelector", ViewData["CurrencyList"], new ViewDataDictionary { new KeyValuePair<string, object>("controllerName", "Orders") });%>
            </td>
            <td>
                <%Html.RenderPartial("OperationSelector", ViewData["OperList"]); %>
            </td>
        </tr>
    </table>
    
    

    <table>
        <tr>
            <th>
            </th>
            <th>
                №
            </th>
            <th>
                <%=Html.ResourceString("DocumentStatus") %>
            </th>
            <th>
                <%=Html.ResourceString("Currency") %>
            </th>
            <th>
                <%=Html.ResourceString("Operation") %>
            </th>
            <th>
                <%=Html.ResourceString("OperSign") %>
            </th>
            <th>
                <%=Html.ResourceString("DocSum")%>
            </th>
            <th>
                <%=Html.ResourceString("Course")%>
            </th>
            <th>
                <%=Html.ResourceString("Equiv")%>
            </th>
            <th>
                <%=Html.ResourceString("Info")%>
            </th>
            <th>
                <%=Html.ResourceString("InfoForBranch")%>
            </th>
            <th>
                <%=Html.ResourceString("InfoForPay")%>
            </th>
            <th>
                <%=Html.ResourceString("DealWithNBU")%>
            </th>
            <th>
                <%=Html.ResourceString("SendDate")%>
            </th>
            <th>
                <%=Html.ResourceString("DealsCount")%>
            </th>
            <th class="empty">
            </th>
        </tr>
        <% foreach (var item in Model)
           {
               if (currentOper != item.Operation)
               {
                   currentOper = item.Operation;
        %>
        <tr class="separator">
            <td>
            </td>
            <td colspan="13">
            </td>
        </tr>
        <%
}
        %>
        <tr>
            <td>
                <%=Html.CheckBox("cb_" + item.Id, new { docSum = "" + item.DocSum.ToString().Replace(",", ".") + "", equiv = "" + item.Equivalent.ToString().Replace(",", ".") + "", @class = "item oper" + (int)currentOper + " status" + (int)item.Status + (item.Status == OrderStatus.Created ? " created" : "") + (item.Operation == EOperation.Buy ? " buy" : " sell") })%>
            </td>
            <td>
                <%= Html.Encode(item.Number) %>
            </td>
            <td class="nowrap">
                <%=Html.ResourceActionLink("d"+item.Status, "Details", "Orders", new { id = item.Id },null)%>
            </td>
            <td class="currency c<%=item.CurId%>">
                <%= Html.Encode(item.CurId) %>
            </td>
            <td>
                <%= Html.Encode(item.OperName) %>
            </td>
            <td class="nowrap">
                <%=Html.ResourceString(item.OperSign.ToString()) %>
            </td>
            <td class="sum">
                <%= Html.Encode(String.Format("{0:N}", item.DocSum)) %>
            </td>
            <td>
                <%=Html.ActionLink(item.Course.ToString(), "UpdateCourse", "Orders", new { id = item.Id }, new { @class = item.Course == 0 ? "zero course" : "course" }) %>
            </td>
            <td class="sum">
                <%= Html.Encode(String.Format("{0:N}", item.Equivalent)) %>
            </td>
            <td>
                <%= Html.Encode(item.Info) %>
            </td>
            <td>
                <%= Html.Encode(item.InfoForBranch) %>
            </td>
            <td>
                <%= Html.Encode(item.InfoForPay) %>
            </td>
            <td>
                <%= Html.ResourceString(item.DealWithNBU.ToString()) %>
            </td>
            <td class="nowrap">
                <%= Html.Encode(String.Format("{0:g}", item.SendDate)) %>
            </td>
            <td>
                <%= Html.Encode(item.DealsCount) %>
            </td>
            <td class="empty">
                <%=Html.ResourceActionLink("Edit","Edit","Orders",new{id=item.Id},null) %>
            </td>
        </tr>
        <% } %>
    </table>
    <div class="buttonsContainer">
        <%=Html.ResourceSubmitButton("BackToCreatedStatus", FormAction.BackToCreatedStatus)%>
        <%=Html.ResourceSubmitButton("Delete",FormAction.Delete) %>
        <%=Html.ResourceSubmitButton("SendToBranch", FormAction.SendToBranch)%>
        <%=Html.ResourceSubmitButton("SendToBranchAndPayment", FormAction.SendToBranchAndPayment)%>
        <%=Html.ResourceSubmitButton("Print", FormAction.Print)%>
    </div>
    <% } %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
    <%=Html.RegisterCss("Tenders.css") %>
    <script type="text/javascript">
        $(function () {
            $("#checkAllOrders").click(function () {
                $("input:checkbox.item").attr("checked", "checked");
                calculate();
            });

            $("#checkNothing").click(function () {
                $("input:checkbox.item").removeAttr("checked");
                $("#totalDocSumBuy").val(0);
                $("#totalDocSumSell").val(0);
                $("#totalEquivBuy").val(0);
                $("#totalEquivSell").val(0);
            });

            $("#checkCreated").click(function () {
                $("input:checkbox.item").removeAttr("checked");
                $(".created").attr("checked", "checked");
                calculate();
            });

            $("#checkBuy").click(function () {
                $("input:checkbox.item").removeAttr("checked");
                $(".buy").attr("checked", "checked");
                calculate();
            });

            $("#checkSell").click(function () {
                $("input:checkbox.item").removeAttr("checked");
                $(".sell").attr("checked", "checked");
                calculate();
            });


            $(".buy").change(function () {
                calculate();
            });


            $(".sell").change(function () {
                calculate();
            });

            function calculate() {
                var totalDocSumBuy = 0;
                var totalDocSumSell = 0;
                var totalEquivBuy = 0;
                var totalEquivSell = 0;

                $(".buy").each(function () {
                    if ($(this).attr('checked')) {
                        totalDocSumBuy = totalDocSumBuy + Number($(this).attr('docSum'));
                        totalEquivBuy = totalEquivBuy + Number($(this).attr('equiv'));
                    }
                });
                $(".sell").each(function () {
                    if ($(this).attr('checked')) {
                        totalDocSumSell = totalDocSumSell + Number($(this).attr('docSum'));
                        totalEquivSell = totalEquivSell + Number($(this).attr('equiv'));
                    }
                });
                $("#totalDocSumBuy").val(numFormat(totalDocSumBuy.toFixed(2)));
                $("#totalDocSumSell").val(numFormat(totalDocSumSell.toFixed(2)));
                $("#totalEquivBuy").val(numFormat(totalEquivBuy.toFixed(2)));
                $("#totalEquivSell").val(numFormat(totalEquivSell.toFixed(2)));
            };

            function numFormat(n, d, s) {
                if (arguments.length == 2) { s = " "; }
                if (arguments.length == 1) { s = " "; d = "."; }
                n = n.toString();
                var a = n.split(d);
                var x = a[0];
                var y = a[1];
                var z = "";
                if (typeof (x) != "undefined") {
                    for (i = x.length - 1; i >= 0; i--)
                    z += x.charAt(i);
                    z = z.replace(/(\d{3})/g, "$1" + s);
                    if (z.slice(-s.length) == s)
                    z = z.slice(0, -s.length);
                    x = "";
                    for (i = z.length - 1; i >= 0; i--)
                    x += z.charAt(i);
                    if (typeof (y) != "undefined" && y.length > 0)
                    x += d + y;
                }
                return x;
            }
        });
    </script>
</asp:Content>
