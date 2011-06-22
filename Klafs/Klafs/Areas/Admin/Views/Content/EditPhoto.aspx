<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Klafs.Models.GalleryItem>" %>
<%@ Import Namespace="Dev.Mvc.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <% using (Html.BeginForm("EditPhoto", "Content", FormMethod.Post, new { enctype = "multipart/form-data" }))
       {%>
    <%=Html.HiddenFor(x=>x.Id) %>
    <%=Html.Hidden("contentName")%>
    <table>
        <tr>
        <td>Фото</td>
        <td>
        <%=Html.CachedImage("~/Content/Photos/", Model.ImageSource, "thumbnail", Model.ImageSource, true)%>
        </td>
        </tr>
        <tr>
            <td>
            Если хотите заменить фото &ndash; выберите файл
            </td>
            <td>
                <input type="file" name="logo" />
            </td>
        </tr>
        <tr>
            <td>
                Описание
            </td>
            <td>
                <%=Html.TextAreaFor(x=>x.Description,6,30,null) %>
            </td>
        </tr>
        <tr>
            <td>
                Порядок отображения:
            </td>
            <td>
                <%=Html.TextBoxFor(x=>x.SortOrder) %>
            </td>
        </tr>
    </table>
    <div class="buttonsContainer">
    <input type="submit" value="Сохранить" />
    </div>
    <%
        }%>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="includes" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="HeaderTitleContent" runat="server">
Редактирование фотографии
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="HeaderTitleSignContent" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="SeoContent" runat="server">
</asp:Content>
