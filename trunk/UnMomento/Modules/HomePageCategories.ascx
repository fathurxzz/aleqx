<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.Modules.HomePageCategories"
    CodeBehind="HomePageCategories.ascx.cs" %>
<div class="home-page-category-grid">
    <div class="home-page-category-grid-header">
        <div class="home-page-category-grid-header-line">
        ������ ����������� � <b>8 ����</b> �� <b>20 ������</b>, <br />� ������������ &mdash; <b>�������������</b>.
        </div>
        <div class="home-page-category-grid-header-line">
        ���� � �������� ������� <b>� ������ �������� � ����������</b>.
        </div>
        <div class="home-page-category-grid-header-line">
        ����������� �������, <a href="<%=Page.ResolveUrl("~/shippinginfo.aspx")%>">������ � ���� ���� ������</a>. ;-)
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
