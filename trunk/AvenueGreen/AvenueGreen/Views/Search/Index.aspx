<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<AvenueGreen.Models.Content>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Результаты поиска</h2>
    
    <% foreach (var item in Model)
       {
           %>
           
           <div class="searchResult">
           <a href="/<%=item.ContentId%>"><%=item.Title%></a><br />
           <%
             if(item.Text.Length<=200)  
             {
                 Response.Write(item.Text);
             }
           else
             {
                 Response.Write(item.Text.Substring(0,199));
             }
               
           %>
           </div>
           <%
       } 
   %>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentTitle" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="GalleryContent" runat="server">
</asp:Content>
