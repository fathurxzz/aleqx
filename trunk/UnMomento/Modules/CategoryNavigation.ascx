<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="NopSolutions.NopCommerce.Web.Modules.CategoryNavigation" Codebehind="CategoryNavigation.ascx.cs" %>
<div class="block block-category-navigation">
    <div class="bubGift">
    <p class="ot3">
        <!--<%=GetLocaleResourceString("Category.Categories")%>-->
        Выберите подарок...
    </p>
    </div>
    <div class="clear"></div>
    <div class="listbox">
        <ul>
            <asp:PlaceHolder runat="server" ID="phCategories" EnableViewState="false" />
        </ul>
    </div>
</div>
