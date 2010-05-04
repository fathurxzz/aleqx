<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ViaCon - ������� ����������������� - �������������� �������
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<%=Html.ActionLink("�� �������","Index")%>

 <br /><br />
    <br />
    <b>* ContentId</b> - (���������� ���, ������������ � ������ ������)<br />
    <b>* Title</b> - (������������ � ���������� � ������� ����)<br />
    <br />
    <% 
        using (Html.BeginForm("UpdateContent", "Admin", FormMethod.Post))
        {
            Html.RenderAction<ViaCon.Controllers.AdminController>(a => a.GalleryList(null, 0));
        } 
    %>
    <%= Html.ActionLink("�������� �����", "AddGalleryItem", null, new { @class = "adminContentTableLink" })%>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentContainerTitle" runat="server">
�������������� �������
</asp:Content>
