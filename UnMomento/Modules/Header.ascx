<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.Modules.HeaderControl"
    CodeBehind="Header.ascx.cs" %>
<%@ Register TagPrefix="nopCommerce" TagName="CurrencySelector" Src="~/Modules/CurrencySelector.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="LanguageSelector" Src="~/Modules/LanguageSelector.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="TaxDisplayTypeSelector" Src="~/Modules/TaxDisplayTypeSelector.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="HeaderMenu" Src="~/Modules/HeaderMenu.ascx" %>

<div class="header">
    <!--
    <div class="header-logo">
        <a href="<%=CommonHelper.GetStoreLocation()%>" class="logo">&nbsp; </a>
    </div>
    -->
    <div id="currencySelector">
        <nopCommerce:CurrencySelector runat="server" ID="CurrencySelector1"></nopCommerce:CurrencySelector>
    </div>
    <nopCommerce:HeaderMenu ID="ctrlHeaderMenu" runat="server" />
    <!--
    <div id="topCentral">
    </div>
    -->
    
    <!--
    <div class="header-selectors-wrapper">
        <div class="header-taxDisplayTypeSelector">
            <nopCommerce:TaxDisplayTypeSelector runat="server" ID="ctrlTaxDisplayTypeSelector">
            </nopCommerce:TaxDisplayTypeSelector>
        </div>
        
        <div class="header-languageselector">
            <nopCommerce:LanguageSelector runat="server" ID="ctrlLanguageSelector"></nopCommerce:LanguageSelector>
        </div>
    </div>
    -->
    <div class="header-links-wrapper">
        <div class="header-links">
            <ul>
                <asp:LoginView ID="topLoginView" runat="server">
                    <AnonymousTemplate>
                        <li><a href="<%=Page.ResolveUrl("~/register.aspx")%>" class="ico-register">
                            <%=GetLocaleResourceString("Account.Register")%></a></li>
                        <li><a href="<%=Page.ResolveUrl("~/login.aspx")%>" class="ico-login">
                            <%=GetLocaleResourceString("Account.Login")%></a></li>
                    </AnonymousTemplate>
                    
                    <LoggedInTemplate>
                        <li class="accItem">
                            <a href="<%= SEOHelper.GetMyAccountUrl()%>" class="account"><%=Page.User.Identity.Name %></a>
                            <% if (NopContext.Current.IsCurrentCustomerImpersonated)
                               { 
                            %>
                            <span class="impersonate">(<%=string.Format(GetLocaleResourceString("Account.ImpersonatedAs"), CustomerManager.UsernamesEnabled ? Server.HtmlEncode(NopContext.Current.User.Username) : Server.HtmlEncode(NopContext.Current.User.Email))%>
                                -
                                <asp:LinkButton runat="server" ID="lFinishImpersonate" Text="<% $NopResources:Account.ImpersonatedAs.Finish %>"
                                    ToolTip="<% $NopResources:Account.ImpersonatedAs.Finish.Tooltip %>" OnClick="lFinishImpersonate_Click"
                                    CssClass="finish-impersonation"></asp:LinkButton>)</span>
                            <%} %>
                        </li>
                        <li><a href="<%=Page.ResolveUrl("~/logout.aspx")%>" class="ico-logout">
                            <%=GetLocaleResourceString("Account.Logout")%></a> </li>
                            
                        <% if (ForumManager.AllowPrivateMessages)
                           { %>
                        <li><a href="<%=Page.ResolveUrl("~/privatemessages.aspx")%>" class="ico-inbox">
                            <%=GetLocaleResourceString("PrivateMessages.Inbox")%></a>
                            <asp:Literal runat="server" ID="lUnreadPrivateMessages" />
                        </li>
                        <%} %>
                        
                    </LoggedInTemplate>
                   
                </asp:LoginView>

                <!--
                <li><a href="<%= SEOHelper.GetShoppingCartUrl()%>" class="ico-cart">
                    <%=GetLocaleResourceString("Account.ShoppingCart")%>
                </a><a href="<%= SEOHelper.GetShoppingCartUrl()%>">(<%=ShoppingCartManager.GetCurrentShoppingCart(ShoppingCartTypeEnum.ShoppingCart).Count%>)</a>
                </li>
                <% if (SettingManager.GetSettingValueBoolean("Common.EnableWishlist"))
                   { %>
                <li><a href="<%= SEOHelper.GetWishlistUrl()%>" class="ico-wishlist">
                    <%=GetLocaleResourceString("Wishlist.Wishlist")%></a> <a href="<%= SEOHelper.GetWishlistUrl()%>">
                        (<%=ShoppingCartManager.GetCurrentShoppingCart(ShoppingCartTypeEnum.Wishlist).Count%>)</a></li>
                <%} %>
                -->
                <% if (NopContext.Current.User != null && NopContext.Current.User.IsAdmin)
                   { %>
                <li><a href="<%=Page.ResolveUrl("~/administration/")%>" class="ico-admin">
                    <%=GetLocaleResourceString("Account.Administration")%></a> </li>
                <%} %>
            </ul>
        </div>
        <div class="phonePanel">(093) 985-88-32<br />(050) 228-55-79<br />(044) 362-59-57</div>
        <!--<div class="workTimePanel">�������� ���� �����<br/>� 8 ���� �� 20 ������</div>-->
    </div>
    
</div>
