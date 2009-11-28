<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/ContentAdmin.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<bigs.Models.ImageContent>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EditPicture
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>EditPicture</h2>

    <table>
        <tr>
            <th>
                FileName
            </th>
            <th>
                
            </th>
            <th>
                �������
            </th>
        </tr>

    <% 
        if(Model!=null)
        foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.Encode(item.FileName) %>
            </td>
            <td>
                <img alt="<%= Html.Encode(item.FileName) %>" src="/Content/Objects/<%=Html.Encode(item.FileName)%>" />
            </td>
            <td>
                <%=Html.ActionLink("�������", "DeletePicture", "Admin", new { id = item.Id }, null)%>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        �������� ����� �����������
    </p>



<% using (Html.BeginForm("EditPicture", "Admin", FormMethod.Post, new {enctype="multipart/form-data"})){ %>
    <%= Html.Hidden("id") %>
    <table>
        <tr>
            <td>�����������:</td>
            <td><input type="file" name="image" /></td>
        </tr>
        <tr>
            <td></td>
            <td><input type="submit" value="���������" /></td>
        </tr>
    </table>
<%} %>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentTitle" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>

