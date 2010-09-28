<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Excursions.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Contacts</h2>

    <% using (Html.BeginForm("FeedBack", "Contacts", FormMethod.Post))
       {
       %>

    <%=Html.TextBox("Author") %>
    <br />
    <%=Html.TextArea("FeedbackText","",10,50,null)%>
    <br />
    <div id="capchaContainer">
    <%= Html.CaptchaImage(50, 160)%><br />
    <%= Html.TextBox("captcha", "")%>
    </div>
    <input type="submit" value="Отправить" />
    <%
       }%>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>
