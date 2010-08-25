<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	����� - ������� ����������������� - �������� �������
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2 class="editContentTitle">�������� ����������</h2>
<% using (Html.BeginForm("UpdateContent", "Admin"))
       {%>
        
        <%=Html.Hidden("id",int.MinValue)%>
        
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
                       <%= Html.TextArea("description", "",5,50,null)%>
                    </td>
                </tr>
                </table>
           
           </div>
        
           
           
          
            
         <div style="position:relative; margin:auto; width:110px;">
         <input  type="submit" value="��������� ��" />
         </div>
      

    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentContainerTitle" runat="server">
</asp:Content>
