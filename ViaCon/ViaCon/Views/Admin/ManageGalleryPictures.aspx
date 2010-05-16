<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ViaCon.Models.Gallery>>" %>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<%@ Import Namespace="ViaCon.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ManageGalleryPictures
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%
    int id = (int)ViewData["id"];
%>
    <h2>�������������� ����������� �������</h2>

    <table id="adminGalleryTable">
        <tr>
            <th>��������</th>
            <th>��������</th>
            <th>�������</th>
            <th></th>
        </tr>
    <% foreach (var item in Model) 
       { %>
        <tr>
            <td><%= Html.Image(GraphicsHelper.GetCachedImage("~/Content/GalleryImages", item.ImageSource, "thumbnail1"))%></td>
            <td><a href="<%=item.MaterialUrl%>"><%=item.MaterialText%></a>  </td>
            <td><%= Html.Encode(item.Location) %></td>
            <td><%= Html.ActionLink("�������", "DeleteImage", new { contentId = id, id = item.Id }, new { onclick = "return confirm('�� �������?')" })%></td>
        </tr>
    <% } %>
    </table>

    
        <%using (Html.BeginForm("AddImageToGallery", "Admin", FormMethod.Post, new { enctype = "multipart/form-data" }))
          { %>
          <%=Html.Hidden("id",id) %>
        <div id="addMore">
            <p>�������� ���:</p>
            
            <table>
            <tr>
                <td>���������:</td>
                <td><%=Html.TextBox("title") %></td>
            </tr>
            <tr>
                <td>�������� �����:</td>
                <td><%=Html.TextBox("materialText") %></td>
            </tr>
            <tr>
                <td>�������� URL:</td>
                <td><%=Html.TextBox("materialUrl") %></td>
            </tr>
            <tr>
                <td>�������:</td>
                <td><%=Html.TextBox("location")%></td>
            </tr>
            <tr>
                <td>����:</td>
                <td><input type="file" name="image" />
            </td>
            </tr>
            </table>
            <input type="submit" value="��������" />
        </div>
        <%} %>
    
<br /><br />
<%=Html.ActionLink("����� � �������","Gallery") %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentContainerTitle" runat="server">
</asp:Content>

