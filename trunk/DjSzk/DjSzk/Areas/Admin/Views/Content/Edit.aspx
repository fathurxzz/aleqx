﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DjSzk.Models.Content>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Система администрирования - Редактирование содержимого
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% Html.EnableClientValidation(); %>


<% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>
        <%=Html.HiddenFor(x=>x.Id) %>


       <div class="adminEditContentContainer">
        <table class="adminEditContentTable">
            <tr>
                <td>
                    <%= Html.LabelFor(model => model.Name) %><br />
                    <span style="font-size: 11px;">(только имя, латиницей, без указания расширения)</span>
                </td>
                <td>
                    <%= Html.TextBoxFor(model => model.Name) %>
                    <%= Html.ValidationMessageFor(model => model.Name) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Html.LabelFor(model => model.PageTitle) %>
                </td>
                <td>
                    <%= Html.TextBoxFor(model => model.PageTitle) %>
                    <%= Html.ValidationMessageFor(model => model.PageTitle) %>
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
                <td>
                    <%= Html.LabelFor(model => model.MenuTitle) %>
                </td>
                <td>
                    <%= Html.TextBoxFor(model => model.MenuTitle)%>
                    <%= Html.ValidationMessageFor(model => model.MenuTitle)%>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Html.LabelFor(model => model.SortOrder) %><br />
                    <span style="font-size: 11px;">(каким по очереди будет этот раздел, необходимо ввести
                        только цифру)</span>
                </td>
                <td>
                    <%= Html.TextBoxFor(model => model.SortOrder, new { style = "width:20px;" })%>
                    <%= Html.ValidationMessageFor(model => model.SortOrder) %>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <%= Html.LabelFor(model => model.Text) %>
                    <br/>
                
                    <%= Html.TextAreaFor(model => model.Text, new { @class = "ckeditor" })%>
                    <%= Html.ValidationMessageFor(model => model.Text) %>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    Для поисковых систем
                </td>
            </tr>
            <tr>
                <td>
                    <%= Html.LabelFor(model => model.SeoDescription) %>
                </td>
                <td>
                    <%= Html.TextAreaFor(model => model.SeoDescription, 3, 40, null)%>
                    <%= Html.ValidationMessageFor(model => model.SeoDescription) %>
                </td>
            </tr>
            <tr>
                <td>
                    <%= Html.LabelFor(model => model.SeoKeywords) %>
                </td>
                <td>
                    <%= Html.TextAreaFor(model => model.SeoKeywords,3,40,null) %>
                    <%= Html.ValidationMessageFor(model => model.SeoKeywords) %>
                </td>
            </tr>
        </table>
        <div class="buttonsContainer">
            <input type="submit" value="Сохранить" />
            <a href="javascript:history.back();">Отмена</a>
        </div>
    </div>

    <% } %>

    

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
    <script type="text/javascript" src="/Scripts/MicrosoftAjax.js"></script>
    <script type="text/javascript" src="/Scripts/MicrosoftMvcValidation.js"></script>
    <script type="text/javascript" src="/Controls/ckeditor/ckeditor.js"></script>
    <script type="text/javascript" src="/Controls/ckeditor/adapters/jquery.js"></script>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainMenu" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentTitle" runat="server">
Редактирование содержимого
</asp:Content>
