<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AvenueGreen.Models.Content>" %>
<%@ Import Namespace="AvenueGreen.Helpers"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import Namespace="AvenueGreen.Models"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%  if (Model != null)
    {

        int? mparentId = null;

        if (Model.Parent != null)
            mparentId = Model.Parent.Id;
        
        if (Request.IsAuthenticated)
        {
         %>   
            <%=Html.ActionLink("[�������������]", "EditContentItem", "Admin", new { id = Model.Id, contentLevel = Model.ContentLevel, isGalleryItem = Model.IsGalleryItem, parentId = mparentId }, new { @class = "adminLink" })%>
            
           <% 
            
               if (Model.ContentLevel == 1)
               {%>
                   <%=Html.ActionLink("[�������� ����� ��������������� ����]", "AddContentItem", "Admin", new { parentId = Model.Id, contentLevel=2,isGalleryItem = Model.IsGalleryItem}, new { @class = "adminLink"})%> 
               <%}
            if(Model.ContentLevel!=0)
          %>
          <%=Html.ActionLink("[�������]", "DeleteContentItem", "Admin", new { id = Model.Id }, new { @class = "adminLink", onclick = "return confirm('������� ���� �����?')" })%>
          <%  
        }
       %><br/><%
        Response.Write(Model.Text);
       } %>

</asp:Content>



<asp:Content ContentPlaceHolderID="GalleryContent" runat="server">









<%
    if(Model!=null)
        if (Model.IsGalleryItem)
            if(Model.Galleries.Count>0)
        {
            
%>
        <div id="galleryContainer">
        
        <%
        
            foreach (var item in Model.Galleries)
            {
                %>
                <div class="gallery">
                <%if (Request.IsAuthenticated)
                  { %>
                  
                  <%=Html.ActionLink("[�������]", "DeleteGallery", "Admin", new { id = item.Id, contentId = Model.ContentId }, new { @class = "adminLink", onclick = "return confirm('������� ���� �����?')" })%>
                  
                <%} %>
                <div class="galleryMainPicture">
                
                </div>
                
                <div class="galleryTitle">
                    <%=Html.ActionLink(item.Title,"Index","Galleries",new{id=item.Id,contentId=Model.ContentId},null)%>
                    <!--<a href="/Galleries/<%=item.Id%>"><%=item.Title%></a>-->
                </div>
                
                </div>
                <%
            }        
        %>
            <div style="clear:both;"></div>
        </div>

<%
        }
    %>




<%
    if(Model!=null){
        if (Request.IsAuthenticated && Model.IsGalleryItem)
        {
            using (Html.BeginForm("AddGallery", "Admin"))
            {
              %>
    <%=Html.Hidden("parentId", Model.Id)%>
    <%=Html.Hidden("contentId", Model.ContentId)%>
    <h2>�������</h2>
    <div id="addMore">
        <p>
            �������� ���:</p>
        <table>
            <tr>
                <td>
                    �������� �������:
                </td>
                <td>
                    <%=Html.TextBox("galleryName")%>
                </td>
            </tr>
        </table>
        <input type="submit" value="��������" />
    </div>
    <%}
        }
 }%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
 AvenueGreen<% if (Model != null) Response.Write(" - " + Model.Title); %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">

   

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentTitle" runat="server">
<% if (Model != null) Response.Write(Model.Title); %>
</asp:Content>