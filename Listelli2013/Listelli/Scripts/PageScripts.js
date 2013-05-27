var BasePageExtender = {
    favoritesCount: 0,
    initialize: function BasePageExtender_initialize() {
        $(function () {
            if (!window.isHomePage) {
                $("#logo").css("cursor", "pointer").click(function () { location.href = "/"; });
            }

            $(".em-link").click(function () { location.href = "http://eugene-miller.com"; });


            $(".button").click(function () {

                var obj = $(this);

                $(".fullContent").each(function () {
                    if ($(this).attr("id") != obj.parent().find(".fullContent").attr("id")) {
                        if ($(this).css("display") == "block") {
                            $(this).css("display", "none");
                            $(this).parent().parent().find(".button").removeClass("up");
                            $(this).parent().parent().find(".button").addClass("down");
                        }
                    }
                });
                

                if ($(this).hasClass("down")) {
                    $(this).parent().find(".fullContent").css("display", "block");
                    $(this).removeClass("down");
                    $(this).addClass("up");
                } else {
                    $(this).parent().find(".fullContent").css("display", "none");
                    $(this).removeClass("up");
                    $(this).addClass("down");
                }
                
            });

        });
    }

};

