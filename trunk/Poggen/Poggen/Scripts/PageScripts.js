var BasePageExtender = {
    initialize: function BasePageExtender_initialize() {
        $(function () {
            if (!window.isHomePage) {
                $("#logo").css("cursor", "pointer").click(function () { location.href = "/"; });
            }

            $("#category").change(function () {
                window.location.href = '/catalogue/' + this.value;
            });

            $("#subcategory").change(function () {
                var category = $("#category").val();
                window.location.href = '/catalogue/' + category + '/' + this.value;
            });
        });
    }
};
