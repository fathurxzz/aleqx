﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% using (Html.BeginForm("AddPhoto", "Content", FormMethod.Post, new { enctype = "multipart/form-data" }))
       {%>
    <%=Html.Hidden("contentId") %>
    <table>
        <tr>
            <td>
                Файл
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
                <%=Html.TextArea("description","", 6,30,null)%>
            </td>
        </tr>
        <tr>
            <td>
                Порядок отображения:
            </td>
            <td>
                <%=Html.TextBox("sortOrder") %>
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
Добавление фотографии
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="HeaderTitleSignContent" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="SeoContent" runat="server">
</asp:Content>
