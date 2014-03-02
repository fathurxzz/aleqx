var HomeController = function() {
    this._initialize();
};

HomeController.prototype = {
    _initialize: function() {

        if (!window.isHomePage) {
            $("#logo").css("cursor", "pointer").click(function () { location.href = "/"; });
        }

    }
};

var HomeControllerInstance = null;

$(function () {
    HomeControllerInstance = new HomeController();
});
