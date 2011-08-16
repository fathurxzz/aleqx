<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Exchange.Models.Tender>" %>

<%@ Import Namespace="Exchange.Models" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=Html.ResourceString("ViewTender")%></h2>
    <% using (Html.BeginForm())
       {%>
    <%= Html.ValidationSummary(true) %>
    <%=Html.HiddenFor(x=>x.Id) %>
    <table class="details">
        <tr>
            <td>
                <%=Html.ResourceString("Number")%>
            </td>
            <td>
                <%=Model.Id %>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("Podrid")%>
            </td>
            <td>
                <%= Html.DisplayTextFor(model => model.Podrid) %>
            </td>
        </tr>
        <%if (Model.OfficeId != Model.ParentOfficeId)
          {%>
        <tr>
            <td>
                <%=Html.ResourceString("ParentOffice")%>
            </td>
            <td>
                <%=Html.DisplayTextFor(model => model.ParentOfficeName)%>
            </td>
        </tr>
        <%
            }%>
        <tr>
            <td>
                <%=Html.ResourceString("Office")%>
            </td>
            <td>
                <%= Html.DisplayTextFor(model => model.OfficeName)%>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("UserName")%>
            </td>
            <td>
                <%= Html.DisplayTextFor(model => model.UserName) %>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("Phone")%>
            </td>
            <td>
                <%= Html.DisplayTextFor(model => model.UserPhone) %>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("Status")%>
            </td>
            <td>
                <%=Html.DisplayTextFor(model => model.StatusName)%>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("Operation")%>
            </td>
            <td>
                <%=Html.ResourceString(Model.Operation.ToString()) %>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("OperSign")%>
            </td>
            <td>
                <%=Html.ResourceString(Model.OperSign.ToString())%>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("Currency")%>
            </td>
            <td class="currency c<%=Model.CurId%>">
                <%= Html.DisplayTextFor(model => model.CurId) %>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("Nls")%>
            </td>
            <td>
                <%= Html.DisplayTextFor(model => model.Nls)%>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("InitSum")%>
            </td>
            <td>
                <%= Model.InitSum.ToString("N") %>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("DocSum") %>
            </td>
            <td>
                <%= Model.DocSum.ToString("N")%>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("Rest") %>
            </td>
            <td>
                 <%= Model.Rest.ToString("N")%>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("Course")%>
            </td>
            <td>
                 <%= Model.Course%>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("CourseOrient")%>
            </td>
            <td>
                <%= Html.TextBoxFor(model => model.CourseOrient, String.Format("{0:F}", Model.CourseOrient)) %>
                <%= Html.ValidationMessageFor(model => model.CourseOrient) %>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("MOR")%>
            </td>
            <td>
                <%=Html.CheckBoxFor(x=>x.CourseMorSign) %>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("Commission")%>
            </td>
            <td>
                <%= Html.TextBoxFor(model => model.Commission, String.Format("{0:F}", Model.Commission))%>
                <%= Html.ValidationMessageFor(model => model.Commission)%>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("ExtraCommissionSum")%>
            </td>
            <td>
                <%= Html.TextBoxFor(model => model.ExtraCommissionSum, String.Format("{0:F}", Model.ExtraCommissionSum)) %>
                <%= Html.ValidationMessageFor(model => model.ExtraCommissionSum) %>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("ClientName")%>
            </td>
            <td>
                <%= Html.DisplayTextFor(model => model.ClientName)%>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("ClientCode")%>
            </td>
            <td>
                <%= Html.DisplayTextFor(model => model.ClientCode)%>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("SendDate")%>
            </td>
            <td>
                <%= Html.DisplayTextFor(model => model.SendDate) %>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("CreateDate")%>
            </td>
            <td>
                <%= Html.DisplayTextFor(model => model.CreateDate)%>
            </td>
        </tr>
                <%if (Model.TenderDate.HasValue)
{%>
        <tr>
            <td>
                <%=Html.ResourceString("TenderDate")%>
            </td>
            <td>
                <%=Model.TenderDate.Value.ToShortDateString()%>
            </td>
        </tr>
        <%
}%>
         <%if (Model.Operation == EOperation.Sell)
{%>
        <tr>
            <td>
                <%=Html.ResourceString("SellTypeName")%>
            </td>
            <td>
                <%= Html.DisplayTextFor(model => model.SellTypeName)%>
            </td>
        </tr>
                <%
}%>
        <tr>
            <td>
                <%=Html.ResourceString("InfoForBranch")%>
            </td>
            <td>
                <%= Html.TextAreaFor(model => model.InfoForBranch)%>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("InfoFromBranch")%>
            </td>
            <td>
                <%= Html.DisplayTextFor(model => model.InfoFromBranch)%>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("CancelInfo")%>
            </td>
            <td>
                <%= Html.TextAreaFor(model => model.CancelInfo)%>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("ExecuteSign")%>
            </td>
            <td>
                <%= Html.CheckBoxFor(model => model.ExecuteSign)%>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("IncludeInTotal")%>
            </td>
            <td>
                <%= Html.CheckBoxFor(model => model.IncludeInTotal)%>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("IsLabeled")%>
            </td>
            <td>
                <%= Html.CheckBoxFor(model => model.IsLabeled)%>
            </td>
        </tr>
        <%if (Model.Operation == EOperation.Buy)
          {%>
        <tr>
            <td>
                <%=Html.ResourceString("PurposeBuy")%>
            </td>
            <td>
                <%=Html.DisplayTextFor(model => model.Purpose)%>
            </td>
        </tr>
        <%
            }%>
              <%if (Model.Operation == EOperation.Sell)
{%>
        <tr>
            <td>
                <%=Html.ResourceString("KvDate")%>
            </td>
            <td>
                <%= Html.DisplayTextFor(model => model.KvDate)%>
            </td>
        </tr>
                <%
}%>
        <tr>
            <td>
                <%=Html.ResourceString("CourseClientRecommend")%>
            </td>
            <td>
                <%= Html.DisplayTextFor(model => model.CourseClientRecommend)%>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("ClientUahPodrid")%>
            </td>
            <td>
                <%= Html.DisplayTextFor(model => model.ClientUahPodrid)%>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("ClientUahNls")%>
            </td>
            <td>
                <%= Html.DisplayTextFor(model => model.ClientUahNls)%>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("ClientCurrPodrid")%>
            </td>
            <td>
                <%= Html.DisplayTextFor(model => model.ClientCurrPodrid)%>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("ClientCurrNls")%>
            </td>
            <td>
                <%= Html.DisplayTextFor(model => model.ClientCurrNls)%>
            </td>
        </tr>
    </table>
    <div class="buttonsContainer">
        <%=Html.ResourceSubmitButton("Save") %>
        <span class="UIButton">
            <%= Html.ResourceActionLink("Back", "Index") %>
        </span>
    </div>
    <% } %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>
