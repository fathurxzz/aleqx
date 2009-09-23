<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/Admin.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<eShop.Models.CategoryProperties>>" %>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<%@ Import Namespace="eShop.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Html.ResourceString("CategoryProperties")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">

    function insertCategoryProperty(link) {
        var pos = $(link).offset();
        pos.left = pos.left + $(link).width();
        $("#InsertCategoryProperty").css("top", pos.top).css("left", pos.left).slideDown("fast");
        window.setTimeout(bindBodyClick, 50);
    }

    function bindBodyClick() {
        $("body").click(bodyClick);
    }

    function bodyClick() {
        $("#InsertCategoryProperty").slideUp("fast");
        $("body").unbind("click", bodyClick);
    }

    $(function() {
    $("#InsertCategoryProperty").click(function(e) { e.stopPropagation(); if (window.event) { window.event.cancelBubbling = true; } });
    }
    );
    
</script>



    <h2><%=Html.ResourceString("CategoryProperties")%></h2>
    
    
    <% Html.RenderAction<eShop.Controllers.PagePartsController>(ppc => ppc.CategoriesList()); %>
    
    <%using (Html.BeginForm("UpdateCategoryProperties", "Admin", FormMethod.Post))
      { %>
    
    <%Html.RenderAction<eShop.Controllers.AdminController>(a=>a.CategoryPropertiesList(null)); %>
    
    <%= Html.Hidden("enablities") %>
    <%= Html.Hidden("updates") %>
    <input type="submit" value="<%= Html.ResourceString("Save") %>" onclick="return collectCategoryChanges();" />
    <%} %>
    

    <a href="#" onclick="insertCategoryProperty(this)">
        <%= Html.ResourceString("AddCategory") %>
    </a>
    
    
    <div id="InsertCategoryProperty" class="greyBorderBox popUpBox">
        <% using (Html.BeginForm("InsertCategoryProperty", "Admin"))
           { %>
        <table class="adminTable">
            <tr>
                <th>
                    <%= Html.ResourceString("Name") %>
                </th>
                <th>
                    Unit
                </th>
                <th>
                    IsMainProperty
                </th>
            </tr>
            <tr>
                <td>
                    <%= Html.TextBox("categoryPropertyName")%>
                </td>
                <td align="center">
                    <%= Html.TextBox("categoryUnitName")%>
                </td>
                <td>
                    <%= Html.CheckBox("isMainProperty",false)%>
                </td>
            </tr>
           
        </table>
        <input type="submit" value="<%= Html.ResourceString("Add") %>" />
        <%} %>
    </div>

    
</asp:Content>

