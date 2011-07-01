<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Babich.Models.Content>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<%if (Model.ContentLevel == 0)
  {
      %>
      <div id="mainContent"></div>
      <%
  }%>
</asp:Content>
<asp:Content ContentPlaceHolderID="GalleryContent" runat="server">
    
    <%if (Model.ContentLevel == 1)
      {%>
    <div id="gContentOuter">
        <div id="gContent">
            <%
          Html.RenderPartial("Gallery", ViewData["Galleries"]);%>
            <%
          Html.RenderPartial("ViewGallery", ViewData["Gallery"]);%>
        </div>
    </div>
    <%
      }%>
</asp:Content>
<asp:Content ContentPlaceHolderID="SubMenuContent" runat="server">
    <%Html.RenderPartial("SubMenu"); %>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentTitleContent" runat="server">
    <div id="titleLine">
        <%=Model.Title %>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="FooterContent" runat="server">
    <%Html.RenderPartial("Paging", ViewData["Galleries"]); %>
</asp:Content>
