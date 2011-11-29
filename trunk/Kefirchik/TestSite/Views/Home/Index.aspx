<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Kefirchik.Graphics" %>
<%@ Import Namespace="TestSite" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div style="margin: 50px;">
        <%=Html.CachedImage(SiteSettings.ThumbOptions["thumb"], "1.jpg", "alt")%>
        <%=Html.CachedImage(SiteSettings.ThumbOptions["thumb"], "2.jpg", "alt")%>
        <%=Html.CachedImage(SiteSettings.ThumbOptions["thumb"], "blank19.gif", "alt")%>
        <%=Html.CachedImage(SiteSettings.ThumbOptions["thumb"], "Fiberglass_Geo_Grids.jpg", "alt")%>
        <%=Html.CachedImage(SiteSettings.ThumbOptions["thumb"], "grid.gif", "alt")%>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
