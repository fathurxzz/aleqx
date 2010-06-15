<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AvenueGreen.Models.Content>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%
    var parentId = (int?)ViewData["parentId"];
    var contentLevel = (int)ViewData["contentLevel"];
    var isGalleryItem = (bool)ViewData["isGalleryItem"];
     %>
     <h2 class="editContentTitle">�������� ����������</h2>
    

    <% using (Html.BeginForm("UpdateContent", "Admin"))
       {%>
        
        <%=Html.Hidden("parentId",parentId)%>
        <%=Html.Hidden("id",int.MinValue)%>
        <%=Html.Hidden("contentLevel", contentLevel)%>
        <%=Html.Hidden("isGalleryItem", isGalleryItem)%>
        
        
        <div class="adminEditContentContainer">
        
        <table class="adminEditContentTable">
        <tr>
            <td>
                ����������� ��� ��������<br />
                <span style="font-size:10px;">(������� �����, ������ ��������� �����)</span>
            </td>
            <td>
                <%= Html.TextBox("contentId", "",new{style="width:300px;"}) %>
            </td>
        </tr>
         <tr>
            <td>
                �������� ������ ����<br />
                <span style="font-size:10px;">(������ ����������)</span>
            </td>
            <td>
                <%= Html.TextBox("title", "", new { style = "width:300px;" })%>
            </td>
        </tr>
         <tr>
            <td colspan="2">
                ���������� ��������<br />
                 <%= Html.TextArea("text")%>
            </td>
        </tr>
         <tr>
            <td>��������� �������, ����� ������� �������</td>
            <td> <%= Html.CheckBox("isGalleryItem", false)%></td>
        </tr>
        <tr>
            <td style="width:200px;">������� ����������:<br />
                <span style="font-size:10px;">(����� �� ������� ����� ���� ������, ���������� ������ ������ �����)</span></td>
            <td>  <%= Html.TextBox("sortOrder", "0", new { style = "width:20px;" })%></td>
        </tr>
        </table>
        
        </div>
        
        
        <h2 class="editContentTitle">��� ��������� ������</h2>
          
           <div class="adminEditContentContainer">
                <table class="adminEditContentTable">
                <tr>
                    <td>
                        �������� ����� ���� ��������<br />
                        <span style="font-size:10px;">(�������� ����� �������)</span>
                    </td>
                    <td>
                       <%= Html.TextBox("keywords", "", new { style = "width:300px;" })%>
                    </td>
                </tr>
                <tr>
                    <td>
                       �������� ��������
                    </td>
                    <td>
                       <%= Html.TextArea("description", "",5,40,null)%>
                    </td>
                </tr>
                </table>
           
           </div>
        
           
           
          
            
         <div style="position:relative; margin:auto; width:110px;">
         <input  type="submit" value="��������� ��" />
         </div>
      

    <% } %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
AvenueGreen - ������� ����������������� - �������� �������
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
 <script type="text/javascript" src="/Scripts/jquery.FCKEditor.js"></script>
    <script type="text/javascript">
        $(function() {
            $.fck.config = { path: '<%= VirtualPathUtility.ToAbsolute("~/Controls/fckeditor/") %>', config: {Toolbar:"Basic", DefaultLanguage: "ru", AutoDetectLanguage: false, SkinPath: "/Controls/fckeditor/editor/skins/office2003/"} };
            $("#text").fck({ toolbar: "Basic", height: 500, width:450 });
        });
    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentTitle" runat="server">
</asp:Content>
