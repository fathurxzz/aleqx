var AdminBasePageExtender = {
    initialize: function AdminBasePageExtender_initialize() {
        $(function () {
            $(".galleryItem, .subCategoryItem, .categoryItem, .mainMenuItem").hover(function () {
                $(this).children(".adminLinksContainer").css("display", "block");
            });

            $(".galleryItem, .subCategoryItem, .categoryItem, .mainMenuItem").mouseleave(function () {
                $(".adminLinksContainer").css("display", "none");
            });
        });
    }
};