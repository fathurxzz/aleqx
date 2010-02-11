<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/ContentAdmin.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<bigs.Models.ImageContent>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	�������������� ������ �����������
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    var enables = {};
    function collectStatusChanges() {
        var enablities = $get("enablities");
        enablities.value = Sys.Serialization.JavaScriptSerializer.serialize(enables);
        return true;
    }

    function updateEnables(value, id) {
        enables[id] = value;
    }
</script>

<% using (Html.BeginForm("EditPictureUrl", "Admin", FormMethod.Post))
   { %>
    <%= Html.Hidden("enablities")%>
    <table>
        <tr>
            <th>
                ��� �����
            </th>
            <th>
                �����������
            </th>
            <th>
                URL
            </th>
            <th>
                �������
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
                <%=Html.TextBox("tb"+item.Id, item.Url, new { onblur = "updateEnables(this.value, " + item.Id + ")" })%>
            </td>
            <td>
                <%=Html.ActionLink("�������", "DeletePicture", "Admin", new { id = item.Id, controllerName = ViewData["controllerName"], contentUrl = ViewData["contentUrl"] }, new { onclick = "return confirm('������� ������?');" })%>
            </td>
        </tr>
    
    <% } %>

    </table>
    
    <input type="submit" value="���������" onclick="return collectStatusChanges()" />
    <br />
    <br />
    <br />
    
<%} %>
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
            <td>URL:</td>
            <td><input type="text" name="url" /></td>
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

