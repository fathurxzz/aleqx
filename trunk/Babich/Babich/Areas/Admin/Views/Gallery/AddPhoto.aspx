<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	AddPhoto
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="mainContent">
    <h2>Добавление фотографии</h2>


    <%using (Html.BeginForm("AddPhoto", "Gallery", FormMethod.Post, new { enctype = "multipart/form-data" }))
      {
          %>
         <%=Html.Hidden("galleryId")%>
         <%=Html.Hidden("contentName")%>

                Файл:
                <br />
                <input type="file" name="logo" />
                <br />
    <div class="buttonsContainer">
    <input type="submit" value="Сохранить" />
    </div>

         
          
          
          <%
      }%>
      </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="SubMenuContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentTitleContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="GalleryContent" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
