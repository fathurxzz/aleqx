var HomeController = function() {
    this._initialize();
};

HomeController.prototype = {
    _initialize: function() {

        if (!window.isHomePage) {
            $("#logo").css("cursor", "pointer").click(function () { location.href = "/"; });
        }

        $('.bxslider').bxSlider({
            mode: 'fade',
            captions: true,
            auto: true,
            pause: 5000,
            speed: 500,
            pager: false,
            nextSelector: '#slider-next',
            prevSelector: '#slider-prev',
            nextText: '',
            prevText: ''
        });

    }
};

var HomeControllerInstance = null;

$(function () {
    HomeControllerInstance = new HomeController();
});
