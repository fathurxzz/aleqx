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
            {
                if (Model.Parent != null)%>
    <%=Html.ActionLink("[удалить]", "DeleteContentItem", "Admin", new { id = Model.Id }, new { @class = "adminLink", onclick = "return confirm('Удалить этот пункт?')" })%>
    <%}

        }
        else if (Model.Children.Count == 0)
        { 
            if(Model.Parent!=null)%>
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
                <a href="#" id="pictureLink">
                    <img src="" alt="" id="pictureContainer" width="540" />
                </a>
            </div>
            
            
            <div id="imageListContainer">
            
            <ul id="mycarousel" class="jcarousel-skin-tango">
      <%
            using (var context = new ContentStorage())
            {
                
                var cnt = 0;
                foreach (var item in Model.Galleries)
                {
                    if(cnt==0)
                    {
                        Response.Write(
                            "<script type=\"text/javascript\">"+
                            "$(\"#pictureContainer\").attr(\"src\", \"" + GraphicsHelper.GetCachedImage("~/Content/GalleryImages", item.ImageSource, "mainView") + "\");" +
                            "$(\"#pictureLink\").attr(\"href\", \"/Content/GalleryImages/" + item.ImageSource + "\");" +
                            "</script>");
                    }
                    cnt++;
    %>
              <li>
                <%
                    if (Request.IsAuthenticated)
                    {%>
                    <%=Html.ActionLink("[удалить]", "DeleteGalleryItem", "Admin", new { id = item.Id, contentId = item.Content.ContentId }, new { @class = "adminLink", onclick = "return confirm('Удалить это изображение?')" })%>
                    <%}%>
                    <div class="carouselItem" style="cursor:pointer" onclick="setImage('<%=item.ImageSource%>')">
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
<div class="separator"></div>
      
      
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

<asp:Content ContentPlaceHolderID="NewsContent" runat="server">

<%
    string contentId = (string)ViewData["contentId"];
    if (contentId.ToString().ToLower() == "news")
    {
        if (Model != null)
        {
            using (var context = new ContentStorage())
            {
                var news = context.Article.Select(a => a).OrderByDescending(a => a.Date).ToList();

                foreach (var item in news)
                {
                    %>
                    
            <div class="newsItem">
            <div class="date">
                <%= item.Date.ToString("dd.MM.yyyy") %>
            </div>
            <h3>
                <%= item.Title %>
            </h3>

                    
                    <% if (Request.IsAuthenticated){ %>
                <div>
                    <%= Html.ActionLink("Редактировать", "AddEditArticle", "Admin", new { id = item.Id }, new { @class = "adminLink" })%>
                </div>
                <div>
                    <%= Html.ActionLink("Удалить", "DeleteArticle", "Admin", new { id = item.Id }, new { @class = "adminLink", onclick = "return confirm('Удалить новость?')" })%>
                </div>
                <%} %>
                
                 <div class="text">
                <%= item.Text %>
                </div>
                </div>
                 <div class="line"></div>
            
                    
                    <%
                }
            }
%>



<%
    } %>


 <% 
     
     
    if (Request.IsAuthenticated)
        Response.Write(Html.ActionLink("Создать новость", "AddEditArticle", "Admin", null, new { @class = "adminLink" })); %>
<%} %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentBoxTitle" runat="server">
<% if (Model != null) Response.Write(Model.Title); %>
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="Includes" runat="server">

    <script type="text/javascript" src="../../Scripts/fancybox/jquery.fancybox-1.3.1.js"></script>
    <link rel="stylesheet" type="text/css" href="../../Scripts/fancybox/jquery.fancybox-1.3.1.css" media="screen" />

    

    <script type="text/javascript">


    




        function setImage(path) {
            $("#pictureContainer").attr("src", "/ImageCache/mainView/" + path);
            $("#pictureLink").attr("href", "/Content/GalleryImages/" + path);
        }



        function calculateDimensions() {
            $(".fadeImage").each(function() {
                var width = $(this).prev("img").width();
                var height = $(this).prev("img").height();

                //alert(height);

                this.style.width = width + "px";
                this.style.height = height + "px";
                this.style.position = "relative";

                var shift = 42 - width / 2;

                this.style.left = 0 + shift + "px";
                this.style.top = (-height-3) + "px";

                //$(this).fadeTo(0.3);

                this.style.background = "White";
                this.style.display = "none";

            });

            //$(".fadeImage").eq(0).fadeTo(0, 0.5);
        }


        jQuery(document).ready(function() {
            jQuery('#mycarousel').jcarousel({ 'scroll': 1 });
            $("a#pictureLink").fancybox({
                'titleShow': false,
                'transitionIn': 'none',
                'transitionOut': 'none',
                'hideOnOverlayClick': true,
                'hideOnContentClick': true,
                'enableEscapeButton': true,
                'showCloseButton': false
            });


            $(".devicePhoto img").click(function(ev, elem) {


                var src = this.src.substring(this.src.lastIndexOf("/"));

                var href = "/Content/GalleryImages" + src;
                src = "/Content/GalleryImages" + src;

                $("#pictureContainer").attr("src", src);
                $("#pictureLink").attr("href", href);

                $(".fadeImage").fadeOut(0);
                
                $(this).next("div").css("display", "block").fadeTo(0, 0.5);

            });

             window.setTimeout(calculateDimensions, 500);

        });


    function setImage(path) {
        $("#pictureContainer").attr("src", "/Content/GalleryImages/" + path);
        $("#pictureLink").attr("href", "/Content/GalleryImages/" + path);
    }
  
    
    

</script>
</asp:Content>
