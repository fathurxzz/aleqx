<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ViaCon.Models.Content>" %>

<%@ Import Namespace="Microsoft.Web.Mvc" %>
<%@ Import Namespace="ViaCon.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ViaCon<% if (Model != null) Response.Write(" - " + Model.Title); %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContainerTitle" runat="server">
    <% if (Model != null) Response.Write(Model.Title); %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% if (Model != null)
       {
           bool hasCollapsibleItem = false;
           if (Request.IsAuthenticated)
           {
               int? mparentId = null;
               if (Model.Parent != null)
                   mparentId = Model.Parent.Id;
    %>
    <%if (Model.Parent == null)
      { %>
    <%=Html.ActionLink("[добавить подпункт]", "AddContentItem", "Admin", new { parentId = Model.Id, isGalleryItem=Model.IsGalleryItem }, new { @class = "adminLink" })%>
    <%} %>
    <%if (Model.Children.Count > 0)
          foreach (var item in Model.Children)
          {
              if (item.Collapsible)
                  hasCollapsibleItem = true;
          }
    %>
    <%if (!hasCollapsibleItem && !Model.IsGalleryItem)
      { %>
    <%=Html.ActionLink("[добавить \"выпадающую\" часть страницы]", "AddContentItem", "Admin", new { parentId = Model.Id, collapsible = true }, new { @class = "adminLink" })%>
    <%} %>
    <%if (!Model.IsGalleryItem)
      { %>
    <%=Html.ActionLink("[редактировать]", "EditContentItem", "Admin", new { id = Model.Id, parentId = mparentId, isGalleryItem = Model.IsGalleryItem }, new { @class = "adminLink" })%>
    <%} %>
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
    <%=Html.ActionLink("[добавить пункт горизонтального меню]", "AddContentItem", "Admin", new { parentId = Model.Id, horisontal = true }, new { @class = "adminLink" })%>
    <%} %>
    <br />
    <%
        }
           Response.Write(Model.Text);
 } %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="GalleryContent" runat="server">
    <%
        if (Model != null)
        {

            if (Model.Galleries.Count > 0)
            {
    %>
    <div id="galleryContainer">
        <%
                
            List<ViaCon.Models.Gallery> galleryList = Model.Galleries.Select(c => c).OrderBy(c => c.SortOrder).ToList();
            foreach (var item in galleryList)
            {%>
        <div class="galleryItem">
            <%= Html.Image(GraphicsHelper.GetCachedImage("~/Content/GalleryImages", item.ImageSource, "thumbnail1"))%>
            <div class="galleryItemSign">
                <b>Локация:</b>
                <%=Html.Encode(item.Location)%>
                <br />
                <b>Материал:</b> <a href="<%=item.MaterialUrl%>">
                    <%=item.MaterialText%></a>
            </div>
            <%if (Request.IsAuthenticated)
              {
                  
                   %>
            порядок:<%=item.SortOrder %>
            <br />
            <%= Html.ActionLink("[редактировать]", "EditImageAttributes", "Admin", new { id = item.Id, contentId=item.Content.ContentId }, new { @class = "adminLink" })%>
            <%= Html.ActionLink("[удалить]", "DeleteImage", "Admin", new { id = item.Id }, new { onclick = "return confirm('Вы уверены?')", @class = "adminLink" })%>
            <%} %>
        </div>
        <%}%>
        <div style="clear: both">
        </div>
    </div>
    <%
        }
            if(Request.IsAuthenticated)
            if (Model.IsGalleryItem)
            {
    %>
    <%using (Html.BeginForm("AddImageToGallery", "Admin", FormMethod.Post, new { enctype = "multipart/form-data" }))
      { %>
    
        <%=Html.Hidden("itemId", Model.Id)%>
        <%=Html.Hidden("contentId", Model.ContentId)%>
        
    <div id="addMore">
        <p>
            Добавить еще:</p>
        <table>
            <tr>
                <td>
                    Материал текст:
                </td>
                <td>
                    <%=Html.TextBox("materialText")%>
                </td>
            </tr>
            <tr>
                <td>
                    Материал URL:
                </td>
                <td>
                    <%=Html.TextBox("materialUrl")%>
                </td>
            </tr>
            <tr>
                <td>
                    Локация:
                </td>
                <td>
                    <%=Html.TextBox("location")%>
                </td>
            </tr>
            <tr>
                <td>Порядок отображения:</td>
                <td><%=Html.TextBox("sortOrder",0)%></td>
            </tr>
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
            }%>
    <%
        }
    %>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CollapsibleContent" runat="server">
    <%
        if (Model != null)
        {
            ViaCon.Models.Content collapsibleContentItem = null;
            if (Model.Children.Count > 0)
            {
                foreach (var item in Model.Children)
                {
                    if (item.Collapsible)
                    {
                        collapsibleContentItem = item;
                        break;
                    }
                }

                if (collapsibleContentItem != null)
                {
                
                
    %>
    <div id="collapsibleContentContainer">
        <hr />
        <table style="border: none; width: 100%">
            <tr>
                <td align="center">
                    <a href="#" id="collapsibleLink" onclick="showCollapsibleBox()">
                        <%=Html.Encode(collapsibleContentItem.Title)%></a>
                </td>
            </tr>
        </table>
        <div id="collapsibleContentBox">
            <%if (Request.IsAuthenticated)
              {%>
            <%=Html.ActionLink("[редактировать]", "EditContentItem", "Admin", new { id = collapsibleContentItem.Id, parentId = Model.Id, collapsible = true}, new { @class = "adminLink" })%>
            <%=Html.ActionLink("[удалить]", "DeleteContentItem", "Admin", new { id = collapsibleContentItem.Id }, new { @class = "adminLink", onclick = "return confirm('Удалить этот пункт?')" })%>
            <br />
            <%  
                } %>
            <%=Html.Encode(collapsibleContentItem.Text)%>
        </div>
    </div>
    <%
        }
        }
    }
    %>
</asp:Content>
