<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Exchange.Models.Tender>" %>

<%@ Import Namespace="Exchange.Models" %>
<%@ Import Namespace="Exchange.Models.Enum" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>
<%@ Register TagPrefix="Exchange" TagName="SearchClientsPanel" Src="~/Views/Shared/SearchClientsPanel.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        AccountsSelector.initialize();
        Validator.initialize();
    </script>
    <% using (Html.BeginForm("Create", "Tenders", FormMethod.Post, new { id = "formNewTender" }))
       {%>
    <%=Html.Hidden("hOfficeId",WebSession.CurrentUser.ParentOfficeId) %>
    <%=Html.HiddenFor(model=>model.Id)%>
    <div>
        <p class="title">
            <%=Html.ResourceString("NewTender")%>
        </p>
        <p>
            <%=Html.ResourceLabel("Operation")%>
            <%=Html.DropDownList("drOper", new List<SelectListItem> { new SelectListItem { Text = Helper.GetResourceString(EOperation.Buy.ToString()), Value = "1", Selected = Model != null && Model.Operation == EOperation.Buy }, new SelectListItem { Text = Helper.GetResourceString(EOperation.Sell.ToString()), Value = "2", Selected = Model != null && Model.Operation == EOperation.Sell } })%>
        </p>
        <p id="fSellType">
            <%=Html.ResourceLabel("SellType")%>
            <%=Html.DropDownList("drSellType", 
                new List<SelectListItem>
                    {
                        new SelectListItem { Text = Helper.GetResourceString(SellType.StrongRequirementSale.ToString()), Value = "1",Selected = Model!=null&&Model.SellType.HasValue&&Model.SellType.Value==SellType.StrongRequirementSale},
                        new SelectListItem { Text = Helper.GetResourceString(SellType.RequirementSale.ToString()), Value = "2",Selected = Model!=null&&Model.SellType.HasValue&&Model.SellType.Value==SellType.RequirementSale}
                    }
            )%>
        </p>
        <p>
            <%=Html.ResourceLabel("OperSign")%>
            <%=Html.DropDownList("drOperSign",
                new List<SelectListItem>
                    {
                        new SelectListItem{Text = Helper.GetResourceString(EOperSign.CurrencyPosition.ToString()), Value = "0",Selected = Model!=null&&Model.OperSign==EOperSign.CurrencyPosition},
                        new SelectListItem{Text = Helper.GetResourceString(EOperSign.ClientCash.ToString()) ,Value = "2",Selected = Model!=null&&Model.OperSign==EOperSign.ClientCash}
                    }) %>
        </p>
        <p>
            <%=Html.ResourceLabel("Currency")%>
            <%=Html.DropDownList("drCurrency", (IEnumerable<SelectListItem>)ViewData["userCurrencies"])%>
        </p>
        <p>
            <%=Html.ResourceLabel("Nls")%>
            <select id="drNls" name="drNls">
            </select>
        </p>
        <p>
            <%=Html.ResourceLabel("DocSum")%>
            <%=Html.TextBoxFor(model=>model.DocSum) %>
        </p>
        <p>
            <%=Html.ResourceLabel("Course")%>
            <%=Html.TextBoxFor(model => model.Course) %>&nbsp;
            <%=Html.CheckBoxFor(model => model.CourseMorSign)%>MOR
        </p>
        <p>
            <%=Html.ResourceLabel("Commission")%>
            <%= Html.TextBoxFor(model => model.Commission) %>
        </p>
        <p id="fPurpose">
            <%=Html.ResourceLabel("PurposeBuy")%>
            <%= Html.TextAreaFor(model => model.Purpose) %>
        </p>
        <p>
            <%=Html.ResourceLabel("InfoFromBranch")%>
            <%= Html.TextAreaFor(model => model.InfoFromBranch) %>
        </p>
        <div id="searchClientPanel">
            <Exchange:SearchClientsPanel ID="SearchClientsPanel1" runat="server" />
        </div>
    </div>
    <div class="buttonsContainer">
        <%if (Model == null||Model.Status==TenderStatus.Created)
          {%>
        <%=Html.ResourceSubmitButton("Save")%>
        <%}%>
        <span class="UIButton">
            <%= Html.ResourceActionLink("Back", "Index") %>
        </span>
    </div>
    <% } %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Includes" runat="server">
    <%=Html.RegisterCss("Tenders.css") %>
</asp:Content>
