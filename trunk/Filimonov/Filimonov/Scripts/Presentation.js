var PresentationPageExtender = {
    favoritesCount: 0,
    initialize: function PresentationPageExtender_initialize() {
        $(function () {
            if (!window.isHomePage) {
                $("#logo").css("cursor", "pointer").click(function () { location.href = "/presentation"; });
            }

            /*
            $("#selectAll").click(function () {


                if ($(this).attr('checked')) {

                    $(".cbx").each(function () {
                        $(this).attr("checked", "checked");
                    });
                } else {
                    $(".cbx").each(function () {
                        $(this).removeAttr("checked");
                    });
                }

            });
            */

        });
    }

};
