var BasePageExtender = {
    initialize: function BasePageExtender_initialize() {
        $(function () {
            if (!window.isHomePage) {
                $("#logo").css("cursor", "pointer").click(function () { location.href = "/"; });
            }


//            $("#catalogueLink").click(function () {
//                $(this).addClass("selected");
//            });

           
        });
    }
};