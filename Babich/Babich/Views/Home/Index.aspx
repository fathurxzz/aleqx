<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Babich.Models.Content>" %>

<%@ Import Namespace="Babich" %>
<%@ Import Namespace="Babich.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <%if (Model.ContentLevel == 0)
      {
    %>
    <div id="mainContent">
        <%
            if (Model.Id == 3)
            {
                Html.RenderPartial("Projects", Model.Children);
            }
            else if (Model.Id == 1)
            {
                using (var context = new ContentStorage())
                {
                    var content = context.Content.Include("Children").Where(c => c.Id == 3).First();
                    Html.RenderPartial("Projects", content.Children);
                }
            }
      
        %>
    </div>
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
    <%if (SiteSettings.Language == Language.En)
      {%>
    <%=Model.DescriptionEng%>
    <%
        }
      else
      {%>
    <%=Model.Description%>
    <%
        }%>
    <%Html.RenderPartial("SubMenu"); %>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentTitleContent" runat="server">
    <div id="titleLine">
        <%if (SiteSettings.Language == Language.En)
          {%>
        <%=Model.TitleEng%>
        <%
            }
          else
          {%>
        <%=Model.Title%>
        <%
            }%>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="FooterContent" runat="server">
    <%Html.RenderPartial("Paging", ViewData["Galleries"]); %>
</asp:Content>
<asp:Content ContentPlaceHolderID="LogoContainer" runat="server">
    <div id="logo">
        <%if (Model.Id != 1)
          {%>
        <a href="/" class="rootLink">
            <img src="../../Content/img/pixel.gif" alt="" /></a>
        <%
            }%>
    </div>
</asp:Content>
