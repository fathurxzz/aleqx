var BasePageExtender = {
    favoritesCount: 0,
    initialize: function BasePageExtender_initialize() {
        $(function () {
            if (!window.isHomePage) {
                $("#logo").css("cursor", "pointer").click(function () { location.href = "/"; });
            }



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


            $(".orderLink").click(function () {
                $("#order").css("display", "block");
            });

        });
    }

};
