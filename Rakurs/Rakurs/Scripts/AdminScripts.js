var AdminBasePageExtender = {
    initialize: function AdminBasePageExtender_initialize() {
        $(function () {
            $(".categotyItem, .mainMenuItem").hover(function () {
                $(this).children(".adminLinksContainer").css("display", "block");
            });

            $(".categotyItem, .mainMenuItem").mouseleave(function () {
                $(".adminLinksContainer").css("display", "none");
            });
        });
    }
};