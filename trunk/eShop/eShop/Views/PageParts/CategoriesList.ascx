<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="eShop.Helpers" %>

<script type="text/javascript">
    function categoryChanged() {
        var currentCategory = document.getElementById("currentCategory");
        var categoryId = currentCategory[currentCategory.selectedIndex].value;
    }
</script>

<%= Html.ResourceString("Categories") %><br />
<%= Html.DropDownList("currentCategory",(List<SelectListItem>)ViewData["categoriesList"]) %>
<br /><br />
<%= Html.ResourceString("SubCategories") %><br />
<%= Html.DropDownList("currentSubCategory",(List<SelectListItem>)ViewData["subCategoriesList"]) %>
