﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Excursions.Models.Content>" %>
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
    

    

    

    <fieldset class="feedBack"><legend>Оставить сообщение:</legend> 
    

    <% using (Html.BeginForm("FeedBack", "Contacts", FormMethod.Post))
       {
       %>
    <div class="addFeedbackAuthor">Имя:</div>
    <%=Html.TextBox("author") %>
    <div class="addFeedbackEmail">Email:</div>
    <%=Html.TextBox("email") %>
    <div class="addFeedbackText">Текст сообщения:</div>
    <%=Html.TextArea("feedbackText","",10,50,null)%>
    
    <div id="capchaContainer">
    <%= Html.CaptchaImage(50, 160)%><br />
    <%= Html.TextBox("captcha", "")%>
    </div>
    <input type="submit" value="Отправить" />
    <%
       }%>
       </fieldset>
    

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>
