<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Honda.Models.Content>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	СП «Биомедика-Сервис» - Система администрирования - Редактирование содержимого
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%
    var parentId = (int?)ViewData["parentId"];
    var horisontal = (bool?)ViewData["horisontal"];
    var isGalleryItem = (bool) ViewData["isGalleryItem"];
     %>
    <h2 class="editContentTitle">Редактирование содержимого</h2>
 <% using (Html.BeginForm("UpdateContent","Admin"))
       {%>

        <%=Html.Hidden("parentId",parentId)%>
        <%=Html.Hidden("id",Model.Id)%>
        <%=Html.Hidden("horisontal", horisontal)%>
        
        
        <div class="adminEditContentContainer">
        
        <table class="adminEditContentTable">
        <tr>
            <td>
                Техническое имя страницы<br />
                <span style="font-size:10px;">(цельное слово, только латинские буквы)</span>
            </td>
            <td>
                <%= Html.TextBox("contentId", Model.ContentId,new{style="width:300px;"}) %>
            </td>
        </tr>
        
        <tr>
            <td>
                Название пункта меню<br />
                <span style="font-size:10px;">(пишите кириллицей)</span>
            </td>
            <td>
                <%= Html.TextBox("title", Model.Title, new { style = "width:300px;" })%>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                Содержимое страницы<br />
                 <%= Html.TextArea("text", Model.Text)%>
            </td>
        </tr>
        <tr>
            <td>Поставьте галочку, чтобы создать галерею</td>
            <td> <%= Html.CheckBox("isGalleryItem", isGalleryItem)%></td>
        </tr>
        <tr>
            <td style="width:200px;">Порядок отбражения:<br />
                <span style="font-size:10px;">(каким по очереди будет этот раздел, необходимо ввести только цифру)</span></td>
            <td>  <%= Html.TextBox("sortOrder", Model.SortOrder,new{style="width:20px;"}) %></td>
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
                       <%= Html.TextBox("keywords", Model.Keywords, new { style = "width:300px;" })%>
                    </td>
                </tr>
                <tr>
                    <td>
                       Описание страницы
                    </td>
                    <td>
                       <%= Html.TextArea("description", Model.Description,5,50,null)%>
                    </td>
                </tr>
                </table>
           
           </div>
           
           
         <div style="position:relative; margin:auto; width:110px;">
         <input  type="submit" value="Сохранить всё" />
         </div>
        

    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentBoxTitle" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Includes" runat="server">
<script type="text/javascript" src="/Scripts/jquery.FCKEditor.js"></script>
    <script type="text/javascript">
        $(function() {
            $.fck.config = { path: '<%= VirtualPathUtility.ToAbsolute("~/Controls/fckeditor/") %>', config: {Toolbar:"Basic", DefaultLanguage: "ru", AutoDetectLanguage: false, SkinPath: "/Controls/fckeditor/editor/skins/office2003/"} };
            $("#text").fck({ toolbar: "Basic", height: 500, width:500 });
        });
    </script>
</asp:Content>