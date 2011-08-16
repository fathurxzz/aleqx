<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Exchange.Models.Views.TenderExecute>" %>

<%@ Import Namespace="Exchange.Models" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>
<%@ Import Namespace="Exchange.Models.Views" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%
        using (Html.BeginForm("Execute", "ExchangeTenders"))
        {
            if (Model.Tenders != null && Model.ExecuteMethod != ExecuteMethod.ByCourse && Model.Tenders.Any(t => t.Rest == 0))
            {
                var customMessage = new CustomMessage
                                                  {
                                                      Text = "Список заявок содержит заявки с нулевым остатком, которые не будут отправлены в распоряжение",
                                                      Type = MessageType.Warning
                                                  };
                Html.RenderPartial("CustomMessage", customMessage);
            }
    %>
    <%=Html.HiddenFor(x=>x.TenderIds) %>
    <%=Html.HiddenFor(x=>x.OperId) %>
    <%=Html.HiddenFor(x=>x.CurId) %>
    <%=Html.HiddenFor(x=>x.OperSign) %>
    <%=Html.HiddenFor(x=>x.ExecuteMethod) %>
    <%
        if (Model.ExecuteMethod == ExecuteMethod.Single)
        {
    %>
    <table style="margin-bottom: 10px;">
        <tr class="title">
            <th colspan="5">
                К выполнению выбрана следующая заявка
            </th>
        </tr>
        <tr>
            <th>
                <%=Html.ResourceString("Operation")%>
            </th>
            <th>
                <%=Html.ResourceString("OperSign")%>
            </th>
            <th>
                <%=Html.ResourceString("Currency")%>
            </th>
            <th>
                <%=Html.ResourceString("DocSum")%>
            </th>
            <th>
                <%=Html.ResourceString("Course")%>
            </th>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString(Model.Operation.ToString())%>
            </td>
            <td>
                <%=Html.ResourceString(((EOperSign)Model.OperSign).ToString())%>
            </td>
            <td class="currency c<%=Model.CurId %>">
                <%=Model.CurId%>
            </td>
            <td class="sum">
                <%=Model.DocSum.ToString("N")%>
            </td>
            <td>
                <%=Model.Course%>
            </td>
        </tr>
    </table>
    <%
        }

        if (Model.ExecuteMethod == ExecuteMethod.Single || Model.ExecuteMethod == ExecuteMethod.Multiply)
        {
    %>
    <table style="margin-bottom: 10px;">
        <tr class="title">
            <th colspan="5">
                <%=Html.ResourceString("ExistedOrders")%>
            </th>
        </tr>
        <tr>
            <th>
                <%=Html.ResourceString("Number")%>
            </th>
            <th>
                <%=Html.ResourceString("Currency")%>
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
        </tr>
        <%
            foreach (var order in Model.ExistedOrders)
            {
        %>
        <tr>
            <td>
                <%=order.Number%>
            </td>
            <td class="currency c<%=Model.CurId %>">
                <%=order.CurId%>
            </td>
            <td class="sum">
                <%=order.DocSum.ToString("N")%>
            </td>
            <td>
                <%=order.Course%>
            </td>
            <td class="sum">
                <%=order.Equivalent.ToString("N")%>
            </td>
        </tr>
        <%
            }
        %>
    </table>
    <%
        }


    %>
    <table>
        <%if (Model.ExecuteMethod == ExecuteMethod.ByCourse)
          {%>
        <tr>
            <td>
                <%=Html.ResourceString("ExchangeMarket")%>:
            </td>
            <td>
                <%=Html.DropDownListFor(x => x.ExchangeMarket, Model.ExchangeMarket)%>
            </td>
        </tr>
        <%
            }%>
        <%if (Model.ExecuteMethod == ExecuteMethod.Single || Model.ExecuteMethod == ExecuteMethod.Multiply)
          {%>
        <tr>
            <td>
                Номер:
            </td>
            <td>
                <%=Html.TextBoxFor( x => x.OrderNumber)%>
            </td>
        </tr>
        <%
            }%>
        <%if (Model.ExecuteMethod == ExecuteMethod.Single)
          {%>
        <tr>
            <td>
                Сумма:
            </td>
            <td>
                <%=Html.TextBoxFor( x => x.DocSum)%>
            </td>
        </tr>
        <%
            }%>
        <%if (Model.ExecuteMethod == ExecuteMethod.ByCourse)
          {%>
        <tr>
            <td>
                Валюта:
            </td>
            <td>
                <%=Html.DisplayFor(x=>x.CurId)%>
            </td>
        </tr>
        <tr>
            <td>Курс:</td>
            <td style="text-align: left">
                <% foreach (var course in Model.Courses)
{
    %>
    <%=Html.CheckBox("cb_"+course,true)%><%=course%><br/>
    <%
} %>
            </td>
        </tr>
        <%
            }%>
        <tr>
            <td>
                Дата:
            </td>
            <td>
                <%=Html.TextBoxFor(x => x.OrderDate)%>
            </td>
        </tr>
    </table>
    <div class="buttonsContainer">
        <%=Html.ResourceSubmitButton("Execute") %>
        <span class="UIButton">
            <%= Html.ResourceActionLink("Back", "Index") %>
        </span>
    </div>
    <%  
        } %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>
