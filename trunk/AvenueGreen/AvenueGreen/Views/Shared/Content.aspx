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
            <%=Html.ActionLink("[редактировать]", "EditContentItem", "Admin", new { id = Model.Id, contentLevel = Model.ContentLevel, isGalleryItem = Model.IsGalleryItem, parentId = mparentId }, new { @class = "adminLink" })%>
            
           <% 
            
               if (Model.ContentLevel == 1)
               {%>
                   <%=Html.ActionLink("[добавить пункт горизонтального меню]", "AddContentItem", "Admin", new { parentId = Model.Id, contentLevel=2,isGalleryItem = Model.IsGalleryItem}, new { @class = "adminLink"})%> 
               <%}
            if(Model.ContentLevel!=0)
          %>
          <%=Html.ActionLink("[удалить]", "DeleteContentItem", "Admin", new { id = Model.Id }, new { @class = "adminLink", onclick = "return confirm('Удалить этот пункт?')" })%>
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
            <div id="galleryPictureTarget">
                <img src="" alt="" id="pictureContainer" width="540" height="390" />
            </div>
            <div id="carouselContainer">
      <ul id="mycarousel" class="jcarousel-skin-tango">
      <%
            using (var context = new ContentStorage())
            {
                var gallery = context.Gallery.Select(g => g).Where(g => g.Content.Id == Model.Id).ToList();
                var cnt = 0;
                foreach (var item in gallery)
                {
                    if(cnt==0)
                    {
                        Response.Write(
                            "<script type=\"text/javascript\">"+
                            "$(\"#pictureContainer\").attr(\"src\", \"/Content/GalleryImages/" + item.ImageSource + "\");"+
                            "</script>");
                    }
                    cnt++;
    %>
              <li>
                <%
                    if (Request.IsAuthenticated)
                    {%>
                    <%=Html.ActionLink("[удалить]", "DeleteGalleryItem", "Admin", new {id = item.Id, contentId = Model.ContentId}, new { @class = "adminLink", onclick = "return confirm('Удалить этот пункт?')" })%>
                    <%}%>
                    <div style="cursor:pointer" onclick="setImage('<%=item.ImageSource%>')">
                    <%=Html.Image(GraphicsHelper.GetCachedImage("~/ag/Content/GalleryImages", item.ImageSource, "thumbnail4"))%>
                </div>
             </li>
             <%
                }
            }
%>
      </ul>
      </div>
      </div>

<%
        }
    %>




<%
    if(Model!=null){
        if (Request.IsAuthenticated && Model.IsGalleryItem)
        {
            using (Html.BeginForm("AddGalleryItem", "Admin", FormMethod.Post, new { enctype = "multipart/form-data" }))
            {
              %>
    <%=Html.Hidden("parentId", Model.Id)%>
    <%=Html.Hidden("contentId", Model.ContentId)%>
    <h2>Галерея</h2>
    <div id="addMore">
        <p>
            Добавить еще:</p>
        <table>
            <tr>
                <td>
                    Файл:
                </td>
                <td>
                    <input type="file" name="image" />
                </td>
            </tr>
        </table>
        <input type="submit" value="Добавить" />
    </div>
    <%}
        }
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


    

    function setImage(path) {

        //$("#galleryPictureTarget").css("background-image", "url('/Content/GalleryImages/" + path + "')");
        $("#pictureContainer").attr("src","/Content/GalleryImages/" + path);
        //alert($('#targetPictureContainer').src);
    }

</script>


</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentTitle" runat="server">
<% if (Model != null) Response.Write(Model.Title); %>
</asp:Content>