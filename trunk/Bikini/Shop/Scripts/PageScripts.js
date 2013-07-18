var BasePageExtender = {
    favoritesCount: 0,
    initialize: function BasePageExtender_initialize() {
        $(function () {
            if (!window.isHomePage) {
                $("#logo").css("cursor", "pointer").click(function () { location.href = "/"; });
            }

            $(".fancybox").fancybox();

            $(".em-link").click(function () { location.href = "http://eugene-miller.com"; });


            


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


        });
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

