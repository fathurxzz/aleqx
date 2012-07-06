var AdminBasePageExtender = {
    initialize: function AdminBasePageExtender_initialize() {
        $(function () {

            $(".mainMenuItem").hover(function () {
                $(this).children(".adminLinksContainer").css("display", "block");
            });

            $(".mainMenuItem").mouseleave(function () {
                $(".adminLinksContainer").css("display", "none");
            });

        });
    }
};