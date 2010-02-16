<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<bigs.Models.ImageContent>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Редактирование списка изображений
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr>
            <th>
                Имя файла
            </th>
            <th>
                Изображение
            </th>
            <th>
                URL
            </th>
            <th>
                Удалить
            </th>
        </tr>

    <% 
    if (Model != null)
        foreach (var item in Model)
        { %>
    
        <tr>
            <td>
                <%= Html.Encode(item.FileName)%>
            </td>
            <td>
                <img alt="<%= Html.Encode(item.FileName) %>" src="/Content/Objects/<%=Html.Encode(item.FileName)%>" />
            </td>
            <td>
                <%= Html.Encode(item.Url)%>
            </td>
            <td>
                <%=Html.ActionLink("Удалить", "DeletePicture", "Admin", new { id = item.Id, controllerName = ViewData["controllerName"], contentUrl = ViewData["contentUrl"] }, new { onclick = "return confirm('Удалить объект?');" })%>
            </td>
        </tr>
    
    <% } %>

    </table>
    
    <br />
    <br />
    

    <p>
        Добавить новое изображение
    </p>


<form enctype="multipart/form-data", action="/Admin/EditPicture" method="post"></form>
<% using (Html.BeginForm("EditPicture", "Admin", new { controllerName = ViewData["controllerName"], contentUrl = ViewData["contentUrl"] }, FormMethod.Post, new { enctype = "multipart/form-data" }))
   { %>
    <%= Html.Hidden("id")%>
    <table>
        <tr>
            <td>Изображение:</td>
            <td><input type="file" name="image" /></td>
        </tr>
        <tr>
            <td>URL:</td>
            <td><input type="text" name="url" /></td>
        </tr>
        
        <tr>
            <td></td>
            <td><input type="submit" value="Загрузить" /></td>
        </tr>
    </table>
<%} %>


</asp:Content>