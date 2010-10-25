<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Excursions.Models.Excursion>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	AddEdit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <h2 class="editContentTitle">Edit content</h2>
    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm("AddEdit","Excursions",FormMethod.Post,new { area="Admin", enctype = "multipart/form-data" }))
       {%>
        <%= Html.ValidationSummary(true) %>
         
        <div class="adminEditContentContainer">
        
        <table class="adminEditContentTable">
        <tr>
            <td>Type:</td>
            <td>
                <select id="excursionType" name="excursionType">
                <option value="1">Excursion</option>
                <option value="2">Sight</option>
                <option value="0">Both</option>
                </select>
            </td>
        </tr>
        <tr>
            <td>Picture:</td>
            <td><input type="file" name="image" /></td>
        </tr>
        <tr>
            <td>
                Name
            </td>
            <td>
                <%= Html.TextBoxFor(model => model.Name, new { style = "width:300px;" })%>
                <%= Html.ValidationMessageFor(model => model.Name)%>
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
                Short Description <%= Html.ValidationMessageFor(model => model.ShortDescription)%><br />
                <%= Html.TextAreaFor(model => model.ShortDescription)%>
            </td>
        </tr>

        <tr>
            <td colspan="2">
                Content <%= Html.ValidationMessageFor(model => model.Text)%> <br />
                <%= Html.TextAreaFor(model => model.Text)%>
            </td>
        </tr>
        <tr>
            <td>Popular</td>
            <td><%=Html.CheckBoxFor(model=>model.Popular) %></td>
        </tr>
        <tr>
            <td>
                Price
            </td>
            <td>
                <%=Html.TextBoxFor(model=>model.Price) %>
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
                        Keywords<!--<br />
                        <span style="font-size:10px;">(вводятся через запятую)</span>-->
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
            $("#Text").fck({ height: 400, width: 500 });
            $("#ShortDescription").fck({ height: 200, width: 500 });
        });
    </script>

    <script type="text/javascript" src="/Scripts/MicrosoftAjax.js"></script>
    <script type="text/javascript" src="/Scripts/MicrosoftMvcValidation.js"></script>
</asp:Content>



