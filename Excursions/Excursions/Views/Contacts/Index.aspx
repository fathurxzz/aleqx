<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Excursions.Models.Content>" %>
<%@ Import Namespace="Excursions.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%
    if(Request.IsAuthenticated)
    {
    %>
    
    <%=Html.ActionLink("редактировать станицу", "Update", "Contacts", new { area = "Admin" }, new { @class = "adminLink" })%>
    <%    
        
    } %>

    <%=Model.Text %>
    
    <div class="feedBack">
    Оставить сообщение:<br />

    <% using (Html.BeginForm("FeedBack", "Contacts", FormMethod.Post))
       {
       %>

    Имя:<%=Html.TextBox("author") %>
    <br />
    Email:<%=Html.TextBox("email") %>
    <br />
    Текст сообщения<br />
    <%=Html.TextArea("feedbackText","",10,50,null)%>
    
    <div id="capchaContainer">
    <%= Html.CaptchaImage(50, 160)%><br />
    <%= Html.TextBox("captcha", "")%>
    </div>
    <input type="submit" value="Отправить" />
    <%
       }%>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>
