<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.Modules.HomePageCategories"
    CodeBehind="HomePageCategories.ascx.cs" %>
<div class="home-page-category-grid">
    <div class="home-page-category-grid-header">
        <div class="home-page-category-grid-header-line">
        Заявки принимаются с <b>8 утра</b> до <b>20 вечера</b>, <br />а доставляются &mdash; <b>круглосуточно</b>.
        </div>
        <div class="home-page-category-grid-header-line">
        Цены в каталоге указаны <b>с учётом доставки и оформления</b>.
        </div>
        <div class="home-page-category-grid-header-line">
        Обязательно узнайте, <a href="<%=Page.ResolveUrl("~/shippinginfo.aspx")%>">почему с нами жить хорошо</a>. ;-)
        </div>
    </div>
    <asp:DataList ID="dlCategories" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
        RepeatLayout="Table" OnItemDataBound="dlCategories_ItemDataBound" ItemStyle-CssClass="item-box" EnableViewState="false">
        <ItemTemplate>
            <div class="category-item">
                <h2 class="title">
                    <asp:HyperLink ID="hlCategory" runat="server" Text='<%#Server.HtmlEncode(Eval("LocalizedName").ToString()) %>' />
                    </h2>
                <div class="picture">
                    <asp:HyperLink ID="hlImageLink" runat="server" />
                </div>
            </div>
        </ItemTemplate>
    </asp:DataList>
</div>
