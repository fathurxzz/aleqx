<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Honda.Models.Article>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	СП «Биомедика-Сервис» - Система администрирования - Добавить новость
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<%
    var parentId = (int?)ViewData["parentId"];
     %>
   <h2 class="editContentTitle">Добавить новость</h2>
<% using (Html.BeginForm("AddEditArticle", "Admin", FormMethod.Post, new { id = "AddEditArticle" }))
       {%>
        
        <%= Html.Hidden("isNew") %>
        <%=Html.Hidden("parentId",parentId)%>
        <%=Html.Hidden("id",int.MinValue)%>
        
        <div class="adminEditContentContainer">
        
        <table class="adminEditContentTable">
         <tr>
            <td>
                Заглавие<br />
            </td>
            <td>
                <%= Html.TextBox("title", ViewData["title"], new { style = "width:300px;" })%>
            </td>
        </tr>
         <tr>
            <td>
                Дата<br />
            </td>
            <td>
                <%= Html.TextBox("date", ViewData["date"], new { style = "width:300px;" })%>
            </td>
        </tr>
         <tr>
            <td colspan="2">
                Содержимое страницы<br />
                 <%= Html.TextArea("text")%>
            </td>
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
                       <%= Html.TextBox("keywords", ViewData["keywords"], new { style = "width:300px;" })%>
                    </td>
                </tr>
                <tr>
                    <td>
                       Описание страницы
                    </td>
                    <td>
                       <%= Html.TextArea("description", (string)ViewData["keywords"], 5, 50, null)%>
                    </td>
                </tr>
                </table>
           
           </div>
        
           
           
          
            
         <div style="position:relative; margin:auto; width:110px;">
         <input  type="submit" value="Сохранить всё" />
         </div>
      

    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
<script type="text/javascript" src="/Scripts/jquery.FCKEditor.js"></script>
    <script type="text/javascript">
        $(function() {
            $.fck.config = { path: '<%= VirtualPathUtility.ToAbsolute("~/Controls/fckeditor/") %>', config: {Toolbar:"Basic", DefaultLanguage: "ru", AutoDetectLanguage: false, SkinPath: "/Controls/fckeditor/editor/skins/office2003/"} };
            $("#text").fck({ toolbar: "Basic", height: 500, width:500 });
        });
    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentBoxTitle" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="GalleryContent" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="NewsContent" runat="server">
</asp:Content>
