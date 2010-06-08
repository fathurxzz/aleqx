<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AvenueGreen.Models.Gallery>" %>
<%@ Import Namespace="AvenueGreen.Helpers"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import Namespace="AvenueGreen.Models"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2></h2>
    
    
    
    <%
    if(Model!=null)
            if(Model.GalleryItems.Count>0)
        {
            
%>
        <div id="galleryItemsContainer">
            <div style="width:600px;"></div>
            <div id="galleryPictureTarget">
                <img src="" alt="" id="pictureContainer" width="540" height="390" />
            </div>
            <div id="carouselContainer">
      <ul id="mycarousel" class="jcarousel-skin-tango">
      <%
            using (var context = new ContentStorage())
            {
                
                var cnt = 0;
                foreach (var item in Model.GalleryItems)
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
                    <%=Html.ActionLink("[удалить]", "DeleteGalleryItem", "Admin", new {id = item.Id, contentId = Model.Content.ContentId}, new { @class = "adminLink", onclick = "return confirm('Удалить этот пункт?')" })%>
                    <%}%>
                    <div style="cursor:pointer" onclick="setImage('<%=item.ImageSource%>')">
                    <%=Html.Image(GraphicsHelper.GetCachedImage("~/Content/GalleryImages", item.ImageSource, "thumbnail4"))%>
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
        if (Request.IsAuthenticated)
        {
            using (Html.BeginForm("AddGalleryItem", "Admin", FormMethod.Post, new { enctype = "multipart/form-data" }))
            {
              %>
    <%=Html.Hidden("parentId", Model.Id)%>
    <%=Html.Hidden("contentId", Model.Content.ContentId)%>
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
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="GalleryContent" runat="server">
</asp:Content>
