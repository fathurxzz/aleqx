<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.Modules.HomePageCategories"
    CodeBehind="HomePageCategories.ascx.cs" %>
<div class="home-page-category-grid">
    <div class="home-page-category-grid-header">
        ������ ����������� � <b>8 ����</b> �� <b>20 ������</b>, � ������������ - <b>�������������</b>.<br />
        ���� � �������� ������� <b>� ������ �������� � ����������</b>.<br />
        ����������� �������, <a href="<%=Page.ResolveUrl("~/shippinginfo.aspx")%>">������ � ���� ���� ������</a>. ;-)
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
