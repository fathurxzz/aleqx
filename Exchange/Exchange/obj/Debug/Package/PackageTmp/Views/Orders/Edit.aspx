<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Exchange.Models.Order>" %>

<%@ Import Namespace="Exchange.Models.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Edit</h2>
    <% using (Html.BeginForm())
       {%>
    <%=Html.HiddenFor(x=>x.Id)%>
    
    <table>
    <tr>
        <td><%=Html.ResourceString("Info")%></td>
        <td><%=Html.TextAreaFor(x=>x.Info)%></td>
    </tr>
    <tr>
        <td><%=Html.ResourceString("InfoForBranch")%></td>
        <td><%=Html.TextAreaFor(x=>x.InfoForBranch)%></td>
    </tr>
    <tr>
        <td><%=Html.ResourceString("InfoForPay")%></td>
        <td><%=Html.TextAreaFor(x=>x.InfoForPay)%></td>
    </tr>
    <tr>
        <td><%=Html.ResourceString("DealWithNBU")%></td>
        <td><%=Html.CheckBoxFor(x=>x.DealWithNBU)%></td>
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
