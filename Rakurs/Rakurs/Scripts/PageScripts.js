var BasePageExtender = {
    initialize: function BasePageExtender_initialize() {
        $(function () {
            if (!window.isHomePage) {
                $("#logo").css("cursor", "pointer").click(function () { location.href = "/"; });
            }

            $("#customFancyCloseButton").click(function () {
                parent.$.fancybox.close();
            });
        });
    }
};