<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AvenueGreen.Models.Widgets>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <h2 class="editContentTitle">Редактирование содержимого</h2>


    <% using (Html.BeginForm("UpdateWidgetItem", "Widget", FormMethod.Post))
       {%>

    <%=Html.Hidden("id",Model.Id) %>
    <%=Html.Hidden("widgetType", Model.Type)%>
        
        
        <div class="adminEditContentContainer">
        
         <table class="adminEditContentTable">
        <tr>
            <td>
                Подпись<br />
            </td>
            <td>
                <%= Html.TextBox("title", Model.Title, new { style = "width:300px;" })%>
            </td>
        </tr>
        <tr>
            <td>
                Адрес (http://...)<br />
            </td>
            <td>
                <%= Html.TextBox("url", Model.Url, new { style = "width:300px;" })%>
            </td>
        </tr>
        </table>
        
        </div>
        <div style="position:relative; margin:auto; width:110px;">
        <table>
        <tr>
            <td>
            <input  type="submit" value="Сохранить всё" /> 
            
            </td>
            <td>
                <input type="button" onclick="javascript:history.back()" value="Назад" />
            </td>
        </tr>
        </table>
        
            
            </div>
          
               


    <% } %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentTitle" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="GalleryContent" runat="server">
</asp:Content>

