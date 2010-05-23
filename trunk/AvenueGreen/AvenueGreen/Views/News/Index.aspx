<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<AvenueGreen.Models.Article>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Новости</h2>
            <% if (Request.IsAuthenticated)
           Response.Write(Html.ActionLink("Создать новость", "AddEditArticle", "Admin", null,  new { @class = "adminLink" })); %>
    <div id="newsContainer">
    <%
        if(Model!=null)
        foreach (var item in Model) { %>
        <div class="newsItem" name="<%= item.Name %>" id="<%= item.Name %>">
            <div class="date">
                <%= item.Date.ToString("dd.MM.yyyy") %>
            </div>
            <h3>
                <%= item.Title %>
            </h3>
            <% if (Request.IsAuthenticated){ %>
                <div>
                    <%= Html.ActionLink("Редактировать", "AddEditArticle", "Admin", new { id = item.Name }, new { @class = "adminLink" })%>
                </div>
                <div>
                    <%= Html.ActionLink("Удалить", "DeleteArticle", "Admin", new { id = item.Name }, new { @class = "adminLink" })%>
                </div>
            <%} %>
            <div class="text">
                <%= item.Text %>
            </div>
        </div>
        <div class="line"></div>
    
    <% } %>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentTitle" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="GalleryContent" runat="server">
</asp:Content>

