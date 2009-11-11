<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<CollectStickers.Models.StickerPresentation>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	StickersSummary
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>StickersSummary</h2>

    
    
    Необходимые стикеры:
<br />
      
    <% if(Model!=null)
        foreach (var item in Model) 
       if(item.isNeed)
       { 
           %>   
                <%= Html.Encode(item.Number) %>,
    <% } %>
<br />
<hr />
    Лишние стикеры:
<br />
      
    <% if (Model != null)
        foreach (var item in Model) 
       if(item.isFree)
       { 
           %>   
                <%= Html.Encode(item.Number) %>,
    <% } %>
<br />



    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="leftMenuContent" runat="server">
</asp:Content>

