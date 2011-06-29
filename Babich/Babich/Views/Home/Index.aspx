<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Babich.Models.Content>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

</asp:Content>

<asp:Content ContentPlaceHolderID="GalleryContent" runat="server">
<% Html.RenderPartial("Gallery",ViewData["Galleries"]); %>
</asp:Content>


<asp:Content ContentPlaceHolderID="SubMenuContent" runat="server">
<%Html.RenderPartial("SubMenu"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="ContentTitleContent" runat="server">
<div id="titleLine">
    <%=Model.Title %>
    </div>
</asp:Content>