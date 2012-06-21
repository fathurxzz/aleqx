var BasePageExtender = {
    initialize: function BasePageExtender_initialize() {
        $(function () {
            if (!window.isHomePage) {
                $("#logo").css("cursor", "pointer").click(function () { location.href = "/"; });
            }

            if (!window.showSubMenu) {
                $("#subMenu").addClass("hide");
            }

            $("#worksLink").click(function () {

                if ($("#subMenu").hasClass("hide")) {
                    $("#subMenu").removeClass("hide");
                    $("#worksLink").css("color", "#fff");
                    $("#worksLink").css("text-decoration", "none");
                    $("#subMenu").show("blind");
                }
            });
        });
    }
};
