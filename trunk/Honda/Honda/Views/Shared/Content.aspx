<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Honda.Models.Content>" %>
<%@ Import Namespace="Honda.Models" %>
<%@ Import Namespace="Honda.Helpers"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	СП «Биомедика-Сервис»<% if (Model != null) Response.Write(" - " + Model.Title); %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<% if (Model != null)
       {
           if (Request.IsAuthenticated)
           {
               int? mparentId = null;
               if (Model.Parent != null)
                   mparentId = Model.Parent.Id;
    %>
    <%//if (Model.Parent == null)
        //{ %>
    <%//=Html.ActionLink("[добавить пункт]", "AddContentItem", "Admin", new { parentId = Model.Id, isGalleryItem=Model.IsGalleryItem }, new { @class = "adminLink" })%>
    <%//} %>
    <%//if (!Model.IsGalleryItem)
      //{ %>
    <%=Html.ActionLink("[редактировать]", "EditContentItem", "Admin", new { id = Model.Id, parentId = mparentId, isGalleryItem = Model.IsGalleryItem }, new { @class = "adminLink" })%>
    <%//} %>
    <%
        if (Model.IsGalleryItem)
        {
            if (Model.Galleries.Count == 0)
            {%>
    <%=Html.ActionLink("[удалить]", "DeleteContentItem", "Admin", new { id = Model.Id }, new { @class = "adminLink", onclick = "return confirm('Удалить этот пункт?')" })%>
    <%}

        }
        else if (Model.Children.Count == 0)
        { %>
    <%=Html.ActionLink("[удалить]", "DeleteContentItem", "Admin", new { id = Model.Id }, new { @class = "adminLink", onclick = "return confirm('Удалить этот пункт?')" })%>
    <%} %>
    <%if (Model.Parent != null && !Model.Horisontal && !Model.IsGalleryItem)
      { %>
    <%=Html.ActionLink("[добавить пункт горизонтального меню]", "AddContentItem", "Admin", new { parentId = Model.Id, horisontal = true, isGalleryItem=Model.IsGalleryItem }, new { @class = "adminLink" })%>
    <%} %>
    <br />
    <%
        }
           Response.Write(Model.Text);
       } %>    

</asp:Content>

<asp:Content ContentPlaceHolderID="GalleryContent" runat="server">

  <%
      if (Model != null)
      {
          if (Model.Galleries.Count > 0)
          {
            
%>
        <div id="galleryItemsContainer">
            <div id="galleryPictureTarget">
                <img src="" alt="" id="pictureContainer" width="430" height="372" />
            </div>
            
            
            <div id="imageListContainer">
                  <%
                  using (var context = new ContentStorage())
                  {

                      var cnt = 0;
                      foreach (var item in Model.Galleries)
                      {
                          if (cnt == 0)
                          {
                              Response.Write(
                                  "<script type=\"text/javascript\">" +
                                  "$(\"#pictureContainer\").attr(\"src\", \"/Content/GalleryImages/" + item.ImageSource + "\");" +
                                  "</script>");
                          }
                          cnt++;
                          %>
                          <div class="imageListItem">
                            <%
                            if (Request.IsAuthenticated)
                                {%>
                                <%=Html.ActionLink("[удалить]", "DeleteGalleryItem", "Admin", new { id = item.Id, contentId = item.Content.ContentId }, new { @class = "adminLink", onclick = "return confirm('Удалить этот пункт?')" })%>
                                <%}%>
                                <div style="cursor:pointer" onclick="setImage('<%=item.ImageSource%>')">
                                <%=Html.Image(GraphicsHelper.GetCachedImage("~/Content/GalleryImages", item.ImageSource, "thumbnail4"))%>
                                </div>
                         </div>
                         <%
                        }
                        
                  }
                %>
     
            </div>
      </div>

<%
      }
          if (Model.IsGalleryItem && Request.IsAuthenticated)
          {
              
              %>
              <div style="margin-top:100px;">
              <%
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
              %></div><%
              
          }
      
      }%>

</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentBoxTitle" runat="server">
<% if (Model != null) Response.Write(Model.Title); %>
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="Includes" runat="server">

<script type="text/javascript">

    function setImage(path) {
        $("#pictureContainer").attr("src", "/Content/GalleryImages/" + path);
    }

</script>
</asp:Content>
