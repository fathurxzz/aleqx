<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="LeftContent" runat="server">

 <%
            string controllerName = ViewContext.RouteData.Values["controller"].ToString().ToUpperInvariant();
            if (controllerName == "SERVICES")
            {
                %>
                <% Html.RenderPartial("SubMenu");%>
                <%
            }
            %>
            

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentTitleContent" runat="server">
</asp:Content>
