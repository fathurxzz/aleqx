<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	«Ривс» - Система администрирования - Добавить контент
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2 class="editContentTitle">Добавить содержимое</h2>
<% using (Html.BeginForm("UpdateContent", "Admin"))
       {%>
        
        <%=Html.Hidden("id",int.MinValue)%>
        
        <div class="adminEditContentContainer">
        
        <table class="adminEditContentTable">
        <tr>
            <td>
                Техническое имя страницы<br />
                <span style="font-size:10px;">(цельное слово, только латинские буквы)</span>
            </td>
            <td>
                <%= Html.TextBox("contentId", "",new{style="width:300px;"}) %>
            </td>
        </tr>
         <tr>
            <td>
                Название пункта меню<br />
                <span style="font-size:10px;">(пишите кириллицей)</span>
            </td>
            <td>
                <%= Html.TextBox("title", "", new { style = "width:300px;" })%>
            </td>
        </tr>
         <tr>
            <td colspan="2">
                Содержимое страницы<br />
                 <%= Html.TextArea("text")%>
            </td>
        </tr>
        <tr>
            <td style="width:200px;">Порядок отбражения:<br />
                <span style="font-size:10px;">(каким по очереди будет этот раздел, необходимо ввести только цифру)</span></td>
            <td>  <%= Html.TextBox("sortOrder", "0", new { style = "width:20px;" })%></td>
        </tr>
        </table>
        
        </div>
        
        
        <h2 class="editContentTitle">Для поисковых систем</h2>
          
           <div class="adminEditContentContainer">
                <table class="adminEditContentTable">
                <tr>
                    <td>
                        Ключевые слова этой страницы<br />
                        <span style="font-size:10px;">(вводятся через запятую)</span>
                    </td>
                    <td>
                       <%= Html.TextBox("keywords", "", new { style = "width:300px;" })%>
                    </td>
                </tr>
                <tr>
                    <td>
                       Описание страницы
                    </td>
                    <td>
                       <%= Html.TextArea("description", "",5,50,null)%>
                    </td>
                </tr>
                </table>
           
           </div>
        
           
           
          
            
         <div style="position:relative; margin:auto; width:110px;">
         <input  type="submit" value="Сохранить всё" />
         </div>
      

    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentContainerTitle" runat="server">
</asp:Content>
