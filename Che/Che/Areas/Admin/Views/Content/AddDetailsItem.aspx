<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Che.Models.Content>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm("Add", "Content", FormMethod.Post, new { enctype = "multipart/form-data" }))
       {%>
       <%= Html.ValidationSummary(true) %>

    <%=Html.HiddenFor(x=>x.Id) %>
    <%=Html.HiddenFor(x=>x.ContentType) %>
    <%=Html.HiddenFor(x=>x.ContentLevel) %>


    <div class="adminEditContentContainer">
        <table class="adminEditContentTable">
            <tr>
                <td>
                    Фото
                </td>
                <td>
                    <input type="file" name="logo" />
                </td>
            </tr>
            <tr>
                <td>
                    <%= Html.LabelFor(model => model.Description) %>
                </td>
                <td>
                    <%= Html.TextAreaFor(model => model.Description, 3, 40, null)%>
                    <%= Html.ValidationMessageFor(model => model.Description) %>
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
        </table>
        <div class="buttonsContainer">
            <input type="submit" value="Сохранить" />
        </div>
    </div>

    
    <% } %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="includes" runat="server">
<link href="../../Admin.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Title" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="SubTitle" runat="server">
</asp:Content>
