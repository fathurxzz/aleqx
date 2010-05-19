<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ViaCon.Models.Gallery>" %>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<%@ Import Namespace="ViaCon.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ViaCon - Система администрирования - Редактирование аттрибутов изображения
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Редактирование аттрибутов изображения</h2>


    <% using (Html.BeginForm("UpdateImageAttributes", "Admin"))
       {%>

        <%=Html.Hidden("id",Model.Id) %>
        <%=Html.Hidden("contentId",Model.Content.ContentId) %>
        <fieldset>
            <legend></legend>
            <p>
                <%= Html.Image(GraphicsHelper.GetCachedImage("~/Content/GalleryImages", Model.ImageSource, "thumbnail1"))%>
            </p>
            
            <p>
                <label for="MaterialText">Название материала:</label>
                <br />
                <%= Html.TextBox("MaterialText", Model.MaterialText, new { style = "width:90%" })%>
            </p>
            <p>
                <label for="MaterialUrl">Ссылка на материал (http://...):</label>
                <br />
                <%= Html.TextBox("MaterialUrl", Model.MaterialUrl, new { style = "width:90%" })%>
            </p>
            <p>
                <label for="Location">Локация:</label>
                <br />
                <%= Html.TextBox("Location", Model.Location, new { style="width:90%"})%>
            </p>
            <p>
                <label for="SortOrder">Порядок отображения:</label>
                <br />
                <%= Html.TextBox("SortOrder", Model.SortOrder, new { style = "width:90%" })%>
            </p>
            <p>
                <input type="submit" value="Сохранить" />
            </p>
        </fieldset>

    <% } %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentContainerTitle" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="GalleryContent" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="CollapsibleContent" runat="server">
</asp:Content>

