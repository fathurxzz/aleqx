<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<CollectStickers.Models.StickerPresentation>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Coincidences
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Coincidences</h2>

   

    <%
        string oldUser = "";
        foreach (var item in Model) {
           string newUser = item.UserName;

            if (newUser != oldUser)
            {
                %>
                <br />
                <br />
                <%= Html.Encode(item.UserName) %>
                <hr />
                <%
            }
             %>
    
       <%= Html.Encode(item.Number) %>,
    
    <% 
        oldUser = newUser;
        } %>

 


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="leftMenuContent" runat="server">
</asp:Content>

