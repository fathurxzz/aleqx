<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Klafs.Models.Content>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
<%=Model.PageTitle%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    
    <%if (!string.IsNullOrEmpty(Model.Title) || !string.IsNullOrEmpty(Model.Text)||Model.GalleryItems.Count>0)
      {%>

    <div id="main">
            <div id="titleContainer">
                <%=Model.Title%>
            </div>
            <div id="contentContainer">
                <div id="contentTop">
                </div>
                <div id="contentMiddle">
                    <% Html.RenderPartial("Gallery",Model.GalleryItems); %>
                    
                    <div class="mainText">
                    <%=Model.Text%></div>
                </div>
                <div id="contentBottom">
                </div>
            </div>
        </div>
    <%
      }%>
</asp:Content>
<asp:Content ContentPlaceHolderID="HeaderTitleContent" runat="server">
    <%=Model.Sign %>
</asp:Content>
<asp:Content ContentPlaceHolderID="HeaderTitleSignContent" runat="server">
    <%=Model.Sign2%>

    <%if (Request.IsAuthenticated&&(Model.ContentType==2||Model.ContentType==3 ))
      {%>
        <%=Html.ActionLink("редактировать", "EditContent", "Content",new {area = "Admin", id = Model.Id}, new {@class = "adminLink"})%>
        
        <%if (Model.ContentType != 0)
{%>
        
        <br />

        <%=Html.ActionLink("Добавить подраздел", "AddContent", "Content", new { area = "Admin", id=ViewData["id"].ToString() }, new { @class = "adminLink" })%>
        
        <%
}
      }%>
</asp:Content>
<asp:Content ContentPlaceHolderID="SeoContent" runat="server">
    <%if (!string.IsNullOrEmpty(Model.SeoText))
      {%>
    <div id="seoDescription">
        <div id="seoContentTitle">
            Официальная информация</div>
        <div id="seoContentContainer">
            <div id="seoContentTop">
            </div>
            <div id="seoContentMiddle">
                <%=Model.SeoText%>
            </div>
            <div id="seoContentBottom">
            </div>
        </div>
    </div>
    <%
        }%>
</asp:Content>
