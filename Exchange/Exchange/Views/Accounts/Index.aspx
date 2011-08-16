<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Exchange.Models.Account>>" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        AccountsViewer.initialize();
    </script>

    <h2> <%=Html.ResourceString("Accounts") %></h2>

    <%=Html.ResourceString("Office") %>
    <%=Html.DropDownList("drOffice", (IEnumerable<SelectListItem>)ViewData["offices"])%>
    <br />
    <br />


    <div id="acccountsContent"></div>

    <p>
        <%= Html.ResourceActionLink("CreateNew", "Create")%>
    </p>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
<%=Html.ResourceString("Accounts") %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>

