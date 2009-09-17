<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="eShop.Helpers" %>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Html.ResourceString("Categories") %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    var enables = {};
    var names = {};

    function updateEnables(check, id) {
        if (check.checked) {
            enables[id] = true;
        }
        else {
            enables[id] = false;
        }
    }

    function updateNames(value, id) {
        names[id] = value;
    }
    
    function collectCategoryChanges() {
        var enablities = $get("enablities");
        enablities.value = Sys.Serialization.JavaScriptSerializer.serialize(enables);

        var updates = $get("updates");
        updates.value = Sys.Serialization.JavaScriptSerializer.serialize(names);
        return true;
    }
    
    function insertCategory(link, parentId) {
        var pos = $(link).offset();
        if (parentId >= 0) {
            $("#parentId").val(parentId);
            pos.left = pos.left - $("#insertCategory").width();
        }
        else {
            pos.left = pos.left + $(link).width();
        }
        $("#insertCategory").css("top", pos.top).css("left", pos.left).slideDown("fast");
        window.setTimeout(bindBodyClick, 50);
    }

    function bindBodyClick() {
        $("body").click(bodyClick);
    }

    function bodyClick() {
        $("#insertCategory").slideUp("fast");
        $("body").unbind("click", bodyClick);
    }
    $(function() {
        $("#insertCategory").click(function(e) { e.stopPropagation(); if (window.event) { window.event.cancelBubbling = true; } });
    }
    );
        
    
</script>
    <h2><%=Html.ResourceString("Categories") %></h2>
    
    <% using (Html.BeginForm("UpdateCategories", "Admin", FormMethod.Post)){ %>
    <% Html.RenderAction<eShop.Controllers.AdminController>(a => a.CategoriesList(null, 0)); %>
    <%= Html.Hidden("enablities") %>
    <%= Html.Hidden("updates") %>
    <input type="submit" value="<%= Html.ResourceString("Save") %>" onclick="return collectCategoryChanges();" />
    <%} %>


    <a href="#" onclick="insertCategory(this, <%= int.MinValue %>)">
        <%= Html.ResourceString("AddCategory") %>
    </a>
    
    
    <div id="insertCategory" class="greyBorderBox popUpBox">
        <% using (Html.BeginForm("InsertCategory", "Admin")){ %>
        <table class="adminTable">
            <tr>
                <th>
                    <%= Html.ResourceString("Name") %>
                </th>
                <th>
                    <%= Html.ResourceString("Show") %>
                </th>
            </tr>
            <tr>
                <td>
                    <%= Html.TextBox("categoryName")%>
                </td>
                <td align="center">
                    <%= Html.CheckBox("categoryEnabled", true) %>
                </td>
            </tr>
        </table>
        <%= Html.Hidden("parentId", int.MinValue) %>
        <input type="submit" value="<%= Html.ResourceString("Add") %>" />
        <%} %>
    </div>

</asp:Content>
