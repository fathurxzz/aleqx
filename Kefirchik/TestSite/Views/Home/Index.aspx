<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Kefirchik.Graphics" %>
<%@ Import Namespace="TestSite" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div style="margin: 50px;">
        
        <%=Html.CachedImage(SiteSettings.Thumbnails["thumb1"], "~/Content/Images", "1.jpg", "thumb", "alt")%>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
