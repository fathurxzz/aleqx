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
    <%=Html.ActionLink("[�������� ��������]", "AddContentItem", "Admin", new { parentId = Model.Id, isGalleryItem=Model.IsGalleryItem }, new { @class = "adminLink" })%>
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
    <%=Html.ActionLink("[�������� \"����������\" ����� ��������]", "AddContentItem", "Admin", new { parentId = Model.Id, collapsible = true }, new { @class = "adminLink" })%>
    <%} %>
    <%if (!Model.IsGalleryItem)
      { %>
    <%=Html.ActionLink("[�������������]", "EditContentItem", "Admin", new { id = Model.Id, parentId = mparentId, isGalleryItem = Model.IsGalleryItem }, new { @class = "adminLink" })%>
    <%} %>
    <%
        if (Model.IsGalleryItem)
        {
            if (Model.Galleries.Count == 0)
            {%>
    <%=Html.ActionLink("[�������]", "DeleteContentItem", "Admin", new { id = Model.Id }, new { @class = "adminLink", onclick = "return confirm('������� ���� �����?')" })%>
    <%}

                   }
                   else if (Model.Children.Count == 0)
                   { %>
    <%=Html.ActionLink("[�������]", "DeleteContentItem", "Admin", new { id = Model.Id }, new { @class = "adminLink", onclick = "return confirm('������� ���� �����?')" })%>
    <%} %>
    <%if (Model.Parent != null && !Model.Horisontal && !Model.IsGalleryItem)
      { %>
    <%=Html.ActionLink("[�������� ����� ��������������� ����]", "AddContentItem", "Admin", new { parentId = Model.Id, horisontal = true }, new { @class = "adminLink" })%>
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
                <b>�������:</b>
                <%=Html.Encode(item.Location)%>
                <br />
                <b>��������:</b> <a href="<%=item.MaterialUrl%>">
                    <%=item.MaterialText%></a>
            </div>
            <%if (Request.IsAuthenticated)
              {
                  
                   %>
            �������:<%=item.SortOrder %>
            <br />
            <%= Html.ActionLink("[�������������]", "EditImageAttributes", "Admin", new { id = item.Id, contentId=item.Content.ContentId }, new { @class = "adminLink" })%>
            <%= Html.ActionLink("[�������]", "DeleteImage", "Admin", new { id = item.Id }, new { onclick = "return confirm('�� �������?')", @class = "adminLink" })%>
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
            �������� ���:</p>
        <table>
            <tr>
                <td>
                    �������� �����:
                </td>
                <td>
                    <%=Html.TextBox("materialText")%>
                </td>
            </tr>
            <tr>
                <td>
                    �������� URL:
                </td>
                <td>
                    <%=Html.TextBox("materialUrl")%>
                </td>
            </tr>
            <tr>
                <td>
                    �������:
                </td>
                <td>
                    <%=Html.TextBox("location")%>
                </td>
            </tr>
            <tr>
                <td>������� �����������:</td>
                <td><%=Html.TextBox("sortOrder",0)%></td>
            </tr>
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
            <%=Html.ActionLink("[�������������]", "EditContentItem", "Admin", new { id = collapsibleContentItem.Id, parentId = Model.Id, collapsible = true}, new { @class = "adminLink" })%>
            <%=Html.ActionLink("[�������]", "DeleteContentItem", "Admin", new { id = collapsibleContentItem.Id }, new { @class = "adminLink", onclick = "return confirm('������� ���� �����?')" })%>
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
