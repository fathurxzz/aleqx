var PresentationPageExtender = {
    favoritesCount: 0,
    enables: {},
    initialize: function PresentationPageExtender_initialize() {
        $(function () {

            $(function () {
                $(".fancyImage").fancybox({ hideOnContentClick: true, showCloseButton: true, cyclic: true, showNavArrows: true, padding: 0, margin: 0, centerOnScroll: true });
            });

            $(".em-link").click(function () { location.href = "http://eugene-miller.com"; });

            if (!window.isHomePage) {
                $("#logo").css("cursor", "pointer").click(function () { location.href = "/presentation"; });
            }

            $(".addToProductSetPanel").scrollFollow({
                offset: 200
            });



//            $(".product").click(function () {

//                var id = $(this).attr("id");

//                var obj = $(this).find(".selectedMarker");
//                if (obj.hasClass("selected")) {
//                    obj.removeClass("selected");
//                    PresentationPageExtender.enables[id] = 0;
//                } else {
//                    obj.addClass("selected");
//                    PresentationPageExtender.enables[id] = 1;
//                }

//            });



            $(".bgMarker").click(function () {

                var id = $(this).attr("id");

                var obj = $(this).parent().find(".selectedMarker");
                if (obj.hasClass("selected")) {
                    obj.removeClass("selected");
                    PresentationPageExtender.enables[id] = 0;
                } else {
                    obj.addClass("selected");
                    PresentationPageExtender.enables[id] = 1;
                }

            });



            $(".product").hover(function () {
                var obj = $(this).find(".bgMarker");
                obj.addClass("selected");
            }, function () {
                var obj = $(this).find(".bgMarker");
                obj.removeClass("selected");
            });



            $("#set").change(function () {
                var form = $("#f1");
                form.submit();
            });

            $(".copy-button").click(function () {
                PresentationPageExtender.collectChanges();
                var form = $("#f2");
                form.submit();
            });



        });
    },

    collectChanges: function PresentationPageExtender_collectChanges() {
        var enablities = $("#enablities");
        enablities.val(Sys.Serialization.JavaScriptSerializer.serialize(PresentationPageExtender.enables));
        return true;
    }
};
