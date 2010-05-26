<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AvenueGreen.Models.Content>" %>
<%@ Import Namespace="AvenueGreen.Helpers"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import Namespace="AvenueGreen.Models"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%  if (Model != null)
    {

        if (Request.IsAuthenticated)
        {
         %>   
            <%=Html.ActionLink("[�������������]", "EditContentItem", "Admin", new { id = Model.Id, contentLevel=Model.ContentLevel /*parentId = mparentId, isGalleryItem = Model.IsGalleryItem*/ }, new { @class = "adminLink" })%>
            
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


<%if(Model.IsGalleryItem)
 {
      %>
      <ul id="mycarousel" class="jcarousel-skin-tango">
      <%
     using (var context = new ContentStorage())
     {
         var gallery = context.Gallery.Select(g => g).Where(g => g.Content.Id == Model.Id).ToList();
         foreach (var item in gallery)
         {
             %>
              <li>
                <%= Html.Image(GraphicsHelper.GetCachedImage("~/Content/GalleryImages", item.ImageSource, "thumbnail2"))%>
             </li>
             <%
         }
     }
      %>
      </ul>
<%
 }%>




<%if(Request.IsAuthenticated&&Model.IsGalleryItem)
 {
     using (Html.BeginForm("AddGalleryItem", "Admin", FormMethod.Post, new { enctype = "multipart/form-data" }))
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
                    ����:
                </td>
                <td>
                    <input type="file" name="image" />
                </td>
            </tr>
        </table>
        <input type="submit" value="��������" />
    </div>
    <%}
 }%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
 AvenueGreen<% if (Model != null) Response.Write(" - " + Model.Title); %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">


<script type="text/javascript">

    jQuery(document).ready(function() {
        jQuery('#mycarousel').jcarousel();
    });

</script>


</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentTitle" runat="server">
<% if (Model != null) Response.Write(Model.Title); %>
</asp:Content>