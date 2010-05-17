<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ViaCon.Models.Content>" %>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<%@ Import Namespace="ViaCon.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ViaCon<% if (Model != null) Response.Write(" - " + Model.Title); %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContainerTitle" runat="server">
    <% if (Model != null) Response.Write(Model.Title); %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% if (Model != null) Response.Write(Model.Text); %>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="GalleryContent" runat="server">
    <%
        if (Model != null)
        {
            
            if (Model.Galleries.Count > 0)
            {
                %>
                <div id="galleryContainer">
                <%
                foreach (var item in Model.Galleries)
                {%>
                <div class="galleryItem">
                <%= Html.Image(GraphicsHelper.GetCachedImage("~/Content/GalleryImages", item.ImageSource, "thumbnail1"))%>
                
                    <div class="galleryItemSign">
                    <b>Локация:</b> <%=Html.Encode(item.Location)%>
                    <br />
                    <b>Материал:</b> <a href="<%=item.MaterialUrl%>"><%=item.MaterialText%></a>
                    </div>
                </div>
                <%}%>
                <div style="clear:both"></div>
                </div>
                <%
            }
        }
         %>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="CollapsibleContent" runat="server">
<%
    if (Model != null)
    {
        ViaCon.Models.Content collapsibleContentItem = null;
        if (Model.Children.Count > 0)
        {
            foreach (var item in Model.Children)
            {
                if (item.Collapsible)
                {
                    collapsibleContentItem = item;
                    break;
                }
            }

            if (collapsibleContentItem!=null)
            {
                %>
                <div id="collapsibleContentContainer">
                <hr />
                <a href="#" id="collapsibleLink" onclick="showCollapsibleBox()"><%=Html.Encode(collapsibleContentItem.Title)%></a>
                <br />
                    <div id="collapsibleContentBox">
                    <%=Html.Encode(collapsibleContentItem.Text)%>
                    </div>
                </div>
                <%
            }
        } 
    }
%>
</asp:Content>


