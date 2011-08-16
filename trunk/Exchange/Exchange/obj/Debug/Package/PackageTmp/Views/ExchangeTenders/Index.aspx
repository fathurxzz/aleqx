<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Exchange.Models.Tender>>" %>

<%@ Import Namespace="Exchange.Models" %>
<%@ Import Namespace="Exchange.Models.Enum" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 style="float: left">
        <%=Html.ResourceString("Tenders")%></h2>
    <div style="float: right">
        ---</div>
    <div style="clear: both">
    </div>
    <div class="line">
    </div>

    <table class="filterContainer">
    <tr>
        <td rowspan="4" valign="top">
            <%Html.RenderPartial("TotalVolumeInfo", ViewData["totalVolumeInfo"], new ViewDataDictionary { new KeyValuePair<string, object>("showCourses", true) });%>
        </td>
        <td colspan="3">
            <%Html.RenderPartial("StatusSelector", ViewData["StatusList"]);%>
            
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <%Html.RenderPartial("CurrencySelector", ViewData["CurrencyList"]);%>
            
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <%Html.RenderPartial("OperSignSelector", ViewData["OperSignList"]);%>
        </td>
    </tr>
    <tr>
        <td>
            <%Html.RenderPartial("CourseFilter", ViewData["CourseList"]);%>
        </td>
        <td>
            <%Html.RenderPartial("SingleDateSelector");%>
        </td>
        <td>
            <%Html.RenderPartial("MfoSelector");%>
        </td>
    </tr>
    </table>
    <% using (Html.BeginForm())
       {

    %>

    

    <table class="tenders">
        <tr>
            <th>
            <%=Html.CheckBox("checkAll") %>
            </th>
            <th>
                <%=Html.ResourceString("Number")%>
            </th>
            <th>
                <%=Html.ResourceString("Podrid")%>
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
            <%if (WebSession.Operation == EOperation.Sell)
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
        <tr class="<%=!item.IncludeInTotal?"notIncudeInTotal":""%>">
            <td>
                <%=Html.CheckBox("cb_" + item.Id, new{@class="item"})%>
            </td>
            <td>
                <%=Html.ActionLink(item.Id.ToString(), "Edit", new {id = item.Id})%>
            </td>
            <td>
                <%=Html.Encode(item.Podrid)%>
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
            <%if (WebSession.Operation == EOperation.Sell)
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
        <%=Html.ResourceActionLink("Create", "Create", new[] { SiteRoles.MasterAdmin }) %>
        <%if (WebSession.SelectedCurrency != 0 && WebSession.OperSign != 0)
        { %>
        <%=Html.ResourceSubmitButton("Execute", FormAction.Execute)%>
        <% } %>
        <%=Html.ResourceSubmitButton("Cancel", FormAction.Cancel, "AreYouSureToCancel")%>
    </div>
    <%
        }%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">


</asp:Content>
