var HomeController = function() {
    this._initialize();
};

HomeController.prototype = {
    _initialize: function() {

        if (!window.isHomePage) {
            $("#logo").css("cursor", "pointer").click(function () { location.href = "/"; });
        }

        $(".fancy").fancybox({ hideOnContentClick: false, showCloseButton: false, cyclic: true, showNavArrows: true, padding: 0, margin: 0, centerOnScroll: true });

        

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


        $("#phone").inputmask("(999)9999999", {
            oncomplete: function () {
                $(".phone a").removeAttr("disabled");
            },
            onclear: function () {
                $(".phone a").attr("disabled", "disabled");
            },
            onincomplete: function () {
                $(".phone a").attr("disabled", "disabled");
            },

            clearIncomplete: true
        });
        

        $(".phone a").click(function () {
            if ($(".phone a").attr("disabled") != null) {
                return;
            }

            $(".phone a").hide(0);
            $(".phone input").hide(0);

            //var url = window.location.href;
            var phone = escape(document.getElementById("phone").value);

            var url = "http://maika.1gb.ua/content/images/" + document.getElementById("previewImageFileName").value;

            //alert(url);
            //alert(phone);


            $.ajax({
                url: "/api/Service/NotifyMiller?url=" + url + "&phone=" + phone,
                    contentType: "application/json",
                    accepts: "application/json",
                    type: "POST",
                    success: function () {
                        $(".phone span").show(0);
                        //alert("ok");
                    },
                    error: function (err) {
                        //alert("Введите номер телефона.");
                        $(".phone a").show(0);
                        $(".phone span").hide(0);
                        //alert("error");
                    }
            });

            //$.ajax({
            //    url: "/api/Items/NotifyMiller?url=" + url + "&phone=" + phone,
            //    contentType: "application/json",
            //    accepts: "application/json",
            //    type: "POST",
            //    success: function () {
            //        $(".phone span").show(0);
            //    },
            //    error: function (err) {
            //        alert("Введите номер телефона.");
            //        $(".phone a").show(0);
            //        $(".phone span").hide(0);
            //    }
            //})
            //  .always(function () {
            //      $(".phone a").css("visibility", "visible");
            //  });
        });


    }
};

var HomeControllerInstance = null;

$(function () {
    HomeControllerInstance = new HomeController();
});
