<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Babich.Models.Gallery>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="mainContent">

<% Html.EnableClientValidation(); %>
    <h2 class="editContentTitle">
        Редактирование галереи</h2>

    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>
        

         <%=Html.HiddenFor(x=>x.Id) %>


         <div class="adminEditContentContainer">

         <table class="adminEditContentTable">
         <tr>
            <td style="width:200px;">
            <%= Html.LabelFor(model => model.Description)%>
            </td>
            <td>
                <div id="editDescriptionUkr">
                    UKR:<br />
                    <%= Html.TextAreaFor(model => model.Description,4,40,null)%>
                    <br /><br />
                </div>
                <div id="editDescriptionEng">
                ENG:<br />
                    <%= Html.TextAreaFor(model => model.DescriptionEng,4, 40, null)%>
                </div>
            </td>
        </tr>
         <tr>
            <td>
                <%= Html.LabelFor(model => model.SortOrder)%><br />
                    <span style="font-size: 11px;">(каким по очереди будет этот раздел, необходимо ввести
                        только цифру)</span>
            </td>
            <td>
             <%= Html.TextBoxFor(model => model.SortOrder, new { style = "width:20px;" })%>
                    <%= Html.ValidationMessageFor(model => model.SortOrder)%>
            </td>
        </tr>
        </table>

         </div>

          <div class="buttonsContainer">
        <input type="submit" value="Сохранить" />&nbsp;
        <%= Html.ActionLink("Назад", "Index", "Home", new { area = "", id = ViewData["contentName"] }, null)%>
    </div>

    <% } %>

</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="includes" runat="server">
<script type="text/javascript" src="/Scripts/MicrosoftAjax.js"></script>
    <script type="text/javascript" src="/Scripts/MicrosoftMvcValidation.js"></script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="LogoContainer" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="SubMenuContent" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="ContentTitleContent" runat="server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="GalleryContent" runat="server">
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>

