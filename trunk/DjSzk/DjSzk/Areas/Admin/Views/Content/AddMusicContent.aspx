﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DjSzk.Models.MusicContent>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Добавить музыкальный контент
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% using (Html.BeginForm("AddMusicContent", "Content", FormMethod.Post, new { enctype = "multipart/form-data" }))
       {%>
    <%= Html.ValidationSummary(true) %>
    <%=Html.Hidden("contentId") %>
    <div class="adminEditContentContainer">
        <table class="adminEditContentTable">
            <tr>
                <td>
                    <%= Html.LabelFor(model => model.FileSource) %>
                </td>
                <td>
                    <input type="file" name="logo" />
                </td>
            </tr>
            <tr>
                <td>
                    <%= Html.LabelFor(model => model.Title) %>
                </td>
                <td>
                    <%= Html.TextBoxFor(model => model.Title) %>
                    <%= Html.ValidationMessageFor(model => model.Title) %>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <%= Html.LabelFor(model => model.Description)%>
                    <br />
                    <%= Html.TextAreaFor(model => model.Description, new { @class = "ckeditor" })%>
                    <%= Html.ValidationMessageFor(model => model.Description)%>
                </td>
            </tr>
        </table>
        <div class="buttonsContainer">
            <input type="submit" value="Сохранить" />
            <a href="#" onclick="javascript:history.back();">Отмена</a>
        </div>
    </div>
    <% } %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainMenu" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentTitle" runat="server">
Добавить музыкальный контент
</asp:Content>
