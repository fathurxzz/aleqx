<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ViaCon.Models.Content>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ViaCon - ������� ����������������� - �������� �������
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%
    var parentId = (int?)ViewData["parentId"];
    var horisontal = (bool?)ViewData["horisontal"];
    var collapsible = (bool?)ViewData["collapsible"];
     %>
    <h2>�������� �������</h2>

    <% using (Html.BeginForm("UpdateContent", "Admin"))
       {%>
        
        
        <%=Html.Hidden("parentId",parentId)%>
        <%=Html.Hidden("id",int.MinValue)%>
        <%=Html.Hidden("horisontal", horisontal)%>
         <%=Html.Hidden("collapsible", collapsible)%>
        
        <fieldset>
            <legend></legend>
            <p>
                <label for="ContentId">�������������:</label>
                <br />
                <%= Html.TextBox("contentId", "", new { style = "width:90%" })%>
            </p>
            
            <p>
                <label for="Title">��������� (�� �� ����� ����):</label>
                <br />
                <%= Html.TextBox("title", "", new { style = "width:90%" })%>
            </p>
            <p>
                <label for="Text">�����:</label>
                <br />
                <%= Html.TextArea("text") %>
            </p>
            <p>
                <label for="Keywords">Keywords:</label>
                <br />
                <%= Html.TextBox("keywords", "", new { style = "width:90%" })%>
            </p>
            <p>
                <label for="Description">Description:</label>
                <br />
                <%= Html.TextArea("description","",5,70,null) %>
            </p>
            <p>
                <label for="SortOrder">������� ����������:</label>
                <br />
                <%= Html.TextBox("sortOrder", "",new{style="width:100%"}) %>
            </p>
            <p>
                <label for="IsGalleryItem">�������:</label>
                <%= Html.CheckBox("isGalleryItem",false)%>
            </p>
            <p>
                <input type="submit" value="�������" />
            </p>
        </fieldset>

    <% } %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentContainerTitle" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Includes" runat="server">
    <script type="text/javascript" src="/Scripts/jquery.FCKEditor.js"></script>
    <script type="text/javascript">
        $(function() {
            $.fck.config = { path: '<%= VirtualPathUtility.ToAbsolute("~/Controls/fckeditor/") %>', config: { DefaultLanguage: "ru", AutoDetectLanguage: false, SkinPath: "/Controls/fckeditor/editor/skins/office2003/"} };
            $("#text").fck({ height: 500, width:600 });
        });
    </script>
</asp:Content>

