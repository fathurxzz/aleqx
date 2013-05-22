var BasePageExtender = {
    favoritesCount: 0,
    initialize: function BasePageExtender_initialize() {
        $(function () {
            if (!window.isHomePage) {
                $("#logo").css("cursor", "pointer").click(function () { location.href = "/"; });
            }

            $(".em-link").click(function () { location.href = "http://eugene-miller.com"; });

          

        });
    }

};

