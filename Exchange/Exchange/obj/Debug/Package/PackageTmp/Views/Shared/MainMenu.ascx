<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>
<%
    var currentControllerName = (string)ViewContext.RouteData.Values["controller"];
    var currentActionName = (string)ViewContext.RouteData.Values["action"];
    var operId = (object)ViewContext.RouteData.Values["operId"];
    var dict = new Dictionary<object, object> { { "controller", currentControllerName }, { "action", currentActionName } };
%>
<div id="menu">
    
    <%= Html.ResourceMenuActionLink("MainPage", "Index", "Home", null, new[] { SiteRoles.MasterAdmin, SiteRoles.DealerRegion, SiteRoles.AdminFrontOffice, SiteRoles.AdminFrontOfficeBranch, SiteRoles.DealerBackOfficeBranch, SiteRoles.DealerBackOffice, SiteRoles.DealerFrontOfficeBranch, SiteRoles.DealerFrontOffice }, dict)%>
    <%= Html.ResourceMenuActionLink("Conversations", "Index", "Conversations", null, new[] { SiteRoles.MasterAdmin, SiteRoles.DealerRegion, SiteRoles.AdminFrontOffice, SiteRoles.AdminFrontOfficeBranch, SiteRoles.DealerBackOfficeBranch, SiteRoles.DealerBackOffice, SiteRoles.DealerFrontOfficeBranch, SiteRoles.DealerFrontOffice }, dict)%>
    <%= Html.ResourceMenuActionLink("Users", "Index", "Users", null, new[] { SiteRoles.MasterAdmin, SiteRoles.DealerRegion, SiteRoles.AdminFrontOffice, SiteRoles.AdminCurrencyControler, SiteRoles.AdminFrontOfficeBranch, SiteRoles.AdminBackOffice }, dict)%>
    <%= Html.ResourceMenuActionLink("Moderators", "Moderators", "Users", null, new[] { SiteRoles.MasterAdmin, SiteRoles.AdminFrontOffice }, dict)%>
    <%= Html.ResourceMenuActionLink("Offices", "Index", "Office", null, new[] { SiteRoles.MasterAdmin, SiteRoles.AdminFrontOffice }, dict)%>
    <%= Html.ResourceMenuActionLink("Accounts", "Index", "Accounts", null, new[] { SiteRoles.MasterAdmin, SiteRoles.DealerFrontOffice }, dict)%>
    <%= Html.ResourceMenuActionLink("Accounts", "BranchAccounts", "Accounts", null, new[] { SiteRoles.MasterAdmin, SiteRoles.DealerBackOfficeBranch, SiteRoles.DealerFrontOfficeBranch }, dict)%>
    <%= Html.ResourceMenuActionLink("Officials", "Index", "Signers", null, new[] { SiteRoles.MasterAdmin, SiteRoles.AdminFrontOffice, SiteRoles.AdminFrontOfficeBranch, SiteRoles.DealerFrontOfficeBranch, SiteRoles.DealerFrontOffice }, dict)%>
    <%= Html.ResourceMenuActionLink("Tenders", "Index", "Tenders", null, new[] { SiteRoles.MasterAdmin, SiteRoles.DealerFrontOfficeBranch }, dict)%>
    <%= Html.ResourceMenuActionLink("BranchTenders", "Index", "BranchTenders", null, new[] { SiteRoles.MasterAdmin, SiteRoles.DealerRegion }, dict)%>
    <%= Html.ResourceMenuActionLink("Buy", "Index", "TendersBuy", null, new[] { SiteRoles.MasterAdmin, SiteRoles.DealerFrontOffice }, new Dictionary<object, object> { { "controller", currentControllerName }, { "action", currentActionName }, { "operId", operId } })%>
    <%= Html.ResourceMenuActionLink("Sell", "Index", "TendersSell", null, new[] { SiteRoles.MasterAdmin, SiteRoles.DealerFrontOffice }, new Dictionary<object, object> { { "controller", currentControllerName }, { "action", currentActionName }, { "operId", operId } })%>
    <%= Html.ResourceMenuActionLink("Orders", "Index", "Orders", null, new[] { SiteRoles.MasterAdmin, SiteRoles.DealerFrontOffice }, dict)%>
    <%= Html.ResourceMenuActionLink("Deals", "Index", "CentreDeals", null, new[] { SiteRoles.MasterAdmin, SiteRoles.DealerFrontOffice }, dict)%>
    <%= Html.ResourceMenuActionLink("CompletedOperations", "Index", "Deals", null, new[] { SiteRoles.MasterAdmin, SiteRoles.DealerFrontOfficeBranch }, dict)%>
    <%= Html.ResourceMenuActionLink("Transactions", "Index", "Transactions", null, new[] { SiteRoles.MasterAdmin, SiteRoles.DealerFrontOffice }, dict)%>
    <%= Html.ResourceMenuActionLink("Pay", "Index", "Pay", null, new[] { SiteRoles.MasterAdmin, SiteRoles.DealerBackOffice }, dict)%>
    <%= Html.ResourceMenuActionLink("Payments", "Payments", "Pay", null, new[] { SiteRoles.MasterAdmin, SiteRoles.DealerBackOffice }, dict)%>
    <%= Html.ResourceMenuActionLink("Settings", "Index", "Settings", null, new[] { SiteRoles.MasterAdmin, SiteRoles.DealerFrontOffice }, dict)%>
    <div class="clear">
    </div>
</div>
