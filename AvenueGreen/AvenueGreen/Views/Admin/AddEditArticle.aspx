<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AvenueGreen.Models.Article>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>�������� �������</h2>
    <% using (Html.BeginForm("AddEditArticle", "Admin", FormMethod.Post, new { id = "AddEditArticle" }))
       {%>
        <%= Html.Hidden("isNew") %>
        
        
        
         <div class="adminEditContentContainer">
         <table class="adminEditContentTable">
        <tr>
            <td>
              �������� ������:<span id="idErrorHolder"></span> <br />
                <span style="font-size:10px;">(������� �����, ������ ��������� �����)</span>
            </td>
            <td>
            <%if((bool)ViewData["isNew"]){ %>
                    <%= Html.TextBox("id") %>
                <%} %>
                <%else{
                      
                       %>
                       <%=Html.Hidden("id", ViewData["id"])%>
                    <strong><%= ViewData["id"] %></strong>
                <%} %>
            </td>
        </tr>
         <tr>
            <td>
                ��������� �������:
                <span style="font-size:10px;">(������ ����������)</span>
            </td>
            <td>
                <%= Html.TextBox("title") %>
            </td>
        </tr>
         <tr>
            <td>
               ����:
                <span style="font-size:10px;">(������ ����������)</span>
            </td>
            <td>
               <%= Html.TextBox("date") %>
            </td>
        </tr>
        
         <tr>
            <td colspan="2">
                �����:<br />
                 <%= Html.TextArea("text")%>
            </td>
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
                       <%= Html.TextBox("keywords")%>
                    </td>
                </tr>
                <tr>
                    <td>
                       �������� ��������
                    </td>
                    <td>
                       <%= Html.TextArea("description")%>
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
AvenueGreen - ������� ����������������� - <%=ViewData["cTitle"] %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
<script type="text/javascript" src="/Scripts/jquery.FCKEditor.js"></script>
 <script type="text/javascript">
        $(function() {
            $.fck.config = { path: '<%= VirtualPathUtility.ToAbsolute("~/Controls/fckeditor/") %>', config: { DefaultLanguage: "ru", AutoDetectLanguage: false, SkinPath: "/Controls/fckeditor/editor/skins/office2003/"} };
            $("#text").fck({ height: 500, width:450 });
        });
</script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentTitle" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="GalleryContent" runat="server">
</asp:Content>

