<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AvenueGreen.Models.Content>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%
    var parentId = (int?)ViewData["parentId"];
    var contentLevel = (int)ViewData["contentLevel"];
    var horisontal = (bool?)ViewData["horisontal"];
    var isGalleryItem = (bool) ViewData["isGalleryItem"];
     %>
     
     <h2 class="editContentTitle">�������������� �����������</h2>


   <% using (Html.BeginForm("UpdateContent","Admin"))
       {%>

        <%=Html.Hidden("parentId",parentId)%>
        <%=Html.Hidden("id",Model.Id)%>
        <%=Html.Hidden("horisontal", horisontal)%>
        <%=Html.Hidden("contentLevel", contentLevel)%>
        
        
        <div class="adminEditContentContainer">
        
        <table class="adminEditContentTable">
        <tr>
            <td>
                ����������� ��� ��������<br />
                <span style="font-size:10px;">(������� �����, ������ ��������� �����)</span>
            </td>
            <td>
                <%= Html.TextBox("contentId", Model.ContentId,new{style="width:300px;"}) %>
            </td>
        </tr>
        
        <tr>
            <td>
                �������� ������ ����<br />
                <span style="font-size:10px;">(������ ����������)</span>
            </td>
            <td>
                <%= Html.TextBox("title", Model.Title, new { style = "width:300px;" })%>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                ���������� ��������<br />
                 <%= Html.TextArea("text", Model.Text)%>
            </td>
        </tr>
        <tr>
            <td>��������� �������, ����� ������� �������</td>
            <td> <%= Html.CheckBox("isGalleryItem", isGalleryItem)%></td>
        </tr>
        <tr>
            <td style="width:200px;">������� ����������:<br />
                <span style="font-size:10px;">(����� �� ������� ����� ���� ������, ���������� ������ ������ �����)</span></td>
            <td>  <%= Html.TextBox("sortOrder", Model.SortOrder,new{style="width:20px;"}) %></td>
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
                       <%= Html.TextBox("keywords", Model.Keywords, new { style = "width:300px;" })%>
                    </td>
                </tr>
                <tr>
                    <td>
                       �������� ��������
                    </td>
                    <td>
                       <%= Html.TextArea("description", Model.Description,5,40,null)%>
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
AvenueGreen - ������� ����������������� - ������������� �������
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
<script type="text/javascript" src="/Scripts/jquery.FCKEditor.js"></script>
    <script type="text/javascript">
        $(function() {
            $.fck.config = { path: '<%= VirtualPathUtility.ToAbsolute("~/Controls/fckeditor/") %>', config: { DefaultLanguage: "ru", AutoDetectLanguage: false, SkinPath: "/Controls/fckeditor/editor/skins/office2003/"} };
            $("#text").fck({ height: 500, width: 450, toolbar:"Basic" });
        });
    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentTitle" runat="server">
</asp:Content>

