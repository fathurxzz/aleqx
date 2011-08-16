<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Exchange.Models.Transaction>" %>

<%@ Import Namespace="Exchange.Models.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Edit</h2>
    <% using (Html.BeginForm())
       {%>
    <%=Html.HiddenFor(x=>x.Id) %>
    <table class="details">
        <tr>
            <td>
                <%=Html.ResourceString("Number")%>
            </td>
            <td>
                <%=Model.TrId %>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("Date")%>
            </td>
            <td>
                <%=Model.TrDate.ToShortDateString() %>
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
                <%=Html.ResourceString("Currency")%>
            </td>
            <td class="currency c<%=Model.CurId%>">
                <%= Html.DisplayTextFor(model => model.CurId) %>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("DocSum")%>
            </td>
            <td>
                <%= Model.TrSum.ToString("N")%>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("PaymentDetail")%>
            </td>
            <td>
                <%= Html.Encode(Model.Detail)%>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("KvSum")%>
            </td>
            <td>
                <%= Html.TextBoxFor(model => model.KvSum, String.Format("{0:F}", Model.KvSum))%>
                <%= Html.ValidationMessageFor(model => model.KvSum)%>
            </td>
        </tr>
        <%if (Model.KvDate.HasValue)
          {%>
        <tr>
            <td>
                <%=Html.ResourceString("Tender")%>
            </td>
            <td>
                <%=Html.ActionLink(Model.TenderId.ToString(), "Edit", "ExchangeTenders", new { id = Model.TenderId }, null) %>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("KvDate")%>
            </td>
            <td>
                <%=Model.KvDate%>
            </td>
        </tr>
        <%
            }%>
        <%if (Model.Deleted)
          {%>
        <tr>
            <td>
                <%=Html.ResourceString("Deleted")%>
            </td>
            <td>
                <%=Html.CheckBoxFor(x => x.Deleted)%>
            </td>
        </tr>
        <%
            }%>
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
