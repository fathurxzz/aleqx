var BasePageExtender = {
    favoritesCount: 0,
    initialize: function BasePageExtender_initialize() {
        $(function () {
            if (!window.isHomePage) {
                $("#logo").css("cursor", "pointer").click(function () { location.href = "/"; });
            }

            $(".fancybox").fancybox();

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


            $("#logonbtn").click(function () {
                return BasePageExtender.checkEmailAndPassword();
            });


            $("#Email").keyup(function () {
                BasePageExtender.validateFields();
            }
            );

            $("#Password").keyup(function () {
                BasePageExtender.validateFields();
            }
            );


            $(".subscribe-overlay").click(function () {
                BasePageExtender.hideSubscribeOverlay();

            });


            $(".subscribe-link").click(function() {
                BasePageExtender.showSubscribeOverlay();
            });


        });
    },


    showSubscribeOverlay: function () {
        
        $(".subscribe-overlay").addClass("subscribe-overlay-fixed");
        $(".subscribe-cloud").addClass("show");
        $(".subscribe-button-container").css("z-index", "8001");
        $("#inputEmail").focus();

    },

    hideSubscribeOverlay: function () {
        $(".subscribe-cloud").removeClass("show");
        $(".subscribe-overlay").removeClass("subscribe-overlay-fixed");
    },
    

    checkEmailAndPassword: function () {

        var email = $("#Email").val();
        var regexEmail = /^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;

        var password = $("#Password").val();
        var regexPassword = /^[a-zA-Z0-9]+$/;

        if (regexEmail.test(email) && regexPassword.test(password)) {
            return true;
        } else {
            return false;
        }
    },

    validateFields: function () {
        if (BasePageExtender.checkEmailAndPassword()) {
            $(".sbmbtn").addClass("active");
        } else {
            $(".sbmbtn").removeClass("active");
        }
    }
};

