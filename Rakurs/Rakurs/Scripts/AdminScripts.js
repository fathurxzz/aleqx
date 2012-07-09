var AdminBasePageExtender = {
    initialize: function AdminBasePageExtender_initialize() {
        $(function () {
            $(".subCategoryItem, .categoryItem, .mainMenuItem").hover(function () {
                $(this).children(".adminLinksContainer").css("display", "block");
            });




            $(".subCategoryItem, .categoryItem, .mainMenuItem").mouseleave(function () {
                $(".adminLinksContainer").css("display", "none");
            });
        });
    }
};