﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Exchange.Models.Tender>>" %>

<%@ Import Namespace="Exchange.Models" %>
<%@ Import Namespace="Exchange.Models.Enum" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="server">
    Заявки
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 style="float: left">
        <%=Html.ResourceString("Tenders")%></h2>
    <div style="float: right">
        refresh</div>
    <div style="clear: both">
    </div>
    <div class="line">
    </div>
    <% using (Html.BeginForm())
       {

    %>
    <table class="filterContainer">
        <tr>
            <td rowspan="3" valign="top">
                <%Html.RenderPartial("TenderTotalsSummary", ViewData["tenderTotalsSummary"]);%>
            </td>
            <td>
                <%Html.RenderPartial("ResetAllSelector"); %>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <%Html.RenderPartial("CurrencySelector", ViewData["CurrencyList"]);%>
            </td>
            <td>
                <%Html.RenderPartial("StatusSelector", ViewData["StatusList"]);%>
            </td>
        </tr>
        <tr>
            <td>
                <%Html.RenderPartial("OperationSelector", ViewData["OperList"]); %>
            </td>
            <td>
                <%Html.RenderPartial("DateSelector"); %>
            </td>
        </tr>
    </table>
    <table class="tenders">
        <tr>
            <th>
            </th>
            <th>
                <%=Html.ResourceString("Number")%>
            </th>
            <th>
                <%=Html.ResourceString("TenderStatus")%>
            </th>
            <th>
                <%=Html.ResourceString("Office")%>
            </th>
            <th>
                <%=Html.ResourceString("Operation")%>
            </th>
            <th>
                <%=Html.ResourceString("Currency")%>
            </th>
            <th>
                <%=Html.ResourceString("DocSum")%>
            </th>
            <th>
                <%=Html.ResourceString("Rest")%>
            </th>
            <th>
                <%=Html.ResourceString("Course")%>
            </th>
            <th>
                <%=Html.ResourceString("Commission")%>
            </th>
            <th>
                <%=Html.ResourceString("Nls")%>
            </th>
            <th>
                <%=Html.ResourceString("SendDate")%>
            </th>
            <%if (WebSession.Operation != EOperation.Buy)
              {%>
            <th>
                <%=Html.ResourceString("KvDate")%>
            </th>
            <%
                }%>
            <th>
                <%=Html.ResourceString("ClientName")%>
            </th>
            <th>
                <%=Html.ResourceString("InfoFromCentre")%>
            </th>
        </tr>
        <%
            foreach (var item in Model)
            {%>
        <tr>
            <td>
                <%=Html.CheckBox("cb_" + item.Id)%>
            </td>
            <td>
                <%=Html.Encode(item.Id)%>
            </td>
            <td>
                <%=Html.ActionLink(item.StatusName, "Create", new {id = item.Id})%>
            </td>
            <td>
                <%=Html.Encode(item.OfficeName)%>
            </td>
            <td>
                <%=Html.Encode(item.OperName)%>
            </td>
            <td class="currency c<%=item.CurId%>">
                <%=Html.Encode(item.CurId)%>
            </td>
            <td class="sum">
                <%=Html.Encode(item.DocSum.ToString("N"))%>
            </td>
            <td class="sum">
                <%=Html.Encode(item.Rest.ToString("N"))%>
            </td>
            <td>
                <%=Html.Encode(item.CourseMorSign ? "MOR" : item.Course.ToString())%>
            </td>
            <td>
                <%=Html.Encode(item.Commission)%>
            </td>
            <td>
                <%=Html.Encode(item.Nls)%>
            </td>
            <td>
                <%=Html.Encode(String.Format("{0:g}", item.SendDate))%>
            </td>
            <%if (WebSession.Operation != EOperation.Buy)
              {%>
            <td>
                <%=Html.Encode(String.Format("{0:g}", item.KvDate))%>
            </td>
            <%
                }%>
            <td>
                <%=Html.Encode(item.ClientName)%>
            </td>
            <td>
                <%=Html.Encode(item.InfoForBranch)%>
            </td>
        </tr>
        <%
            }%>
    </table>
    <div class="buttonsContainer">
        <%=Html.ResourceActionLink("Create", "Create") %>
        <%=Html.ResourceSubmitButton("Send", FormAction.Send, "AreYouSureToSend")%>
        <%=Html.ResourceSubmitButton("Delete", FormAction.Delete, "AreYouSureToDelete")%>
    </div>
    <%
        }%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Includes" runat="server">
    <%=Html.RegisterCss("Tenders.css") %>
</asp:Content>
