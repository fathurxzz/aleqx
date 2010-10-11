<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Excursions.Models.Content>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Update content
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <h2 class="editContentTitle">Update content</h2>

    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>
         
        <div class="adminEditContentContainer">
        
        <table class="adminEditContentTable">
        <tr>
            <td>
                PageName<br />
                <span style="font-size:14px; font-family:Tahoma;">(solid word without gaps)</span>
            </td>
            <td>
                  <%= Html.TextBoxFor(model => model.ContentId, new { style = "width:300px;" })%>
                  <%= Html.ValidationMessageFor(model => model.ContentId)%>
            </td>
        </tr>
        
        <tr>
            <td>
                Title
            </td>
            <td>
                
                <%= Html.TextBoxFor(model => model.Title, new { style = "width:300px;" })%>
                <%= Html.ValidationMessageFor(model => model.Title)%>
            </td>
        </tr>
      

        <tr>
            <td colspan="2">
                Page content <%= Html.ValidationMessageFor(model => model.Text)%><br />
                
                <%= Html.TextAreaFor(model => model.Text)%>
            </td>
        </tr>

    

        <tr>
            <td style="width:200px;">Order:</td>
            <td> 
            <%= Html.TextBoxFor(model => model.SortOrder, new { style = "width:20px;" })%>
            <%= Html.ValidationMessageFor(model => model.SortOrder)%>
            </td>
        </tr>
        
        </table>
        
         </div>
         
          <h2 class="editContentTitle">SEO</h2>
          
           <div class="adminEditContentContainer">
                <table class="adminEditContentTable">
                <tr>
                    <td>
                        Keywords
                    </td>
                    <td>
                        <%= Html.TextBoxFor(model => model.Keywords, new { style = "width:300px;" })%>
                    </td>
                </tr>
                <tr>
                    <td>
                       Description
                    </td>
                    <td>
                       <%= Html.TextAreaFor(model => model.Description, 5, 50, null)%>
                    </td>
                </tr>
                </table>
           
           </div>
           
           
         <div style="position:relative; margin:auto; width:110px;">
         <input  type="submit" value="Save" />
         </div>

      
    <% } %>

    <div>
        <%= Html.ActionLink("Back to list", "Index", "Excursions", new { area=""},null)%>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
<link href="/Content/Admin2.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/Scripts/jquery.FCKEditor.js"></script>
    <script type="text/javascript">
        $(function () {
            $.fck.config = { path: '<%= VirtualPathUtility.ToAbsolute("~/Controls/fckeditor/") %>', config: { DefaultLanguage: "ru", AutoDetectLanguage: false, SkinPath: "/Controls/fckeditor/editor/skins/office2003/"} };
            $("#Text").fck({ height: 500, width: 500 });
            $("#ShortDescription").fck({ height: 500, width: 500 });
        });
    </script>
</asp:Content>