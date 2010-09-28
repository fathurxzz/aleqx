<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Excursions.Models.Excursion>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	AddEdit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <h2 class="editContentTitle">Редактирование содержимого</h2>

    <% using (Html.BeginForm("AddEdit","Excursions",FormMethod.Post,new { area="Admin", enctype = "multipart/form-data" }))
       {%>
        <%= Html.ValidationSummary(true) %>
         
        <div class="adminEditContentContainer">
        
        <table class="adminEditContentTable">
        <tr>
            <td>Файл изображения:</td>
            <td><input type="file" name="image" /></td>
        </tr>
        <tr>
            <td>
                Название экскурсии
            </td>
            <td>
                
                <%= Html.TextBoxFor(model => model.Title, new { style = "width:300px;" })%>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                Краткое содержимое страницы (отображается в списке)<br />
                <%= Html.TextAreaFor(model => model.ShortDescription)%>
            </td>
        </tr>

        <tr>
            <td colspan="2">
                Содержимое страницы<br />
                <%= Html.TextAreaFor(model => model.Text)%>
            </td>
        </tr>
        <tr>
            <td>Популярная</td>
            <td><%=Html.CheckBoxFor(model=>model.Popular) %></td>
        </tr>
        <tr>
            <td>
                Цена
            </td>
            <td>
                <%=Html.TextBoxFor(model=>model.Price) %>
            </td>
        </tr>

        <tr>
            <td style="width:200px;">Порядок отбражения:<br />
                <span style="font-size:10px;">(каким по очереди будет этот раздел, необходимо ввести только цифру)</span></td>
            <td> 
            <%= Html.TextBoxFor(model => model.SortOrder, new { style = "width:20px;" })%>
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
                        <%= Html.TextBoxFor(model => model.Keywords, new { style = "width:300px;" })%>
                    </td>
                </tr>
                <tr>
                    <td>
                       Описание страницы
                    </td>
                    <td>
                       <%= Html.TextAreaFor(model => model.Description, 5, 50, null)%>
                    </td>
                </tr>
                </table>
           
           </div>
           
           
         <div style="position:relative; margin:auto; width:110px;">
         <input  type="submit" value="Сохранить всё" />
         </div>

      
    <% } %>

    <div>
        <%= Html.ActionLink("Назад к списку экскурсий", "Index", "Excursions", new { area=""},null)%>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
<link href="/Content/Admin2.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/Scripts/jquery.FCKEditor.js"></script>
    <script type="text/javascript">
        $(function () {
            $.fck.config = { path: '<%= VirtualPathUtility.ToAbsolute("~/Controls/fckeditor/") %>', config: { Toolbar: "Basic", DefaultLanguage: "ru", AutoDetectLanguage: false, SkinPath: "/Controls/fckeditor/editor/skins/office2003/"} };
            $("#Text").fck({ toolbar: "Basic", height: 400, width: 500 });
            $("#ShortDescription").fck({ toolbar: "Basic", height: 200, width: 500 });
        });
    </script>
</asp:Content>



