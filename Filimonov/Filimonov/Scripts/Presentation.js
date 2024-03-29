﻿var PresentationPageExtender = {
    favoritesCount: 0,
    selectedProductsCnt: 0,
    enables: {},
    initialize: function PresentationPageExtender_initialize() {
        $(function () {

            $(function () {
                $(".fancyImage").fancybox({ hideOnContentClick: true, showCloseButton: true, cyclic: true, showNavArrows: true, padding: 0, margin: 0, centerOnScroll: true });
            });

            $(".em-link").click(function () { location.href = "http://eugene-miller.com"; });

            if (!window.isHomePage) {
                $("#logo").css("cursor", "pointer").click(function () { location.href = "/platform"; });
            }

            //            $(".addToProductSetPanel").scrollFollow({
            //                offset: 200
            //            });

            $(".addToProductSetPanelContainer").scrollFollow({
                offset: 200
            });



            selectedProductsCnt = $("#selectedProducts").val();

            //            if (selectedProductsCnt > 0) {
            //                $(".addToProductSetPanel").removeClass("hidden");
            //            }


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


            $(".addToProductSetPanelHidden").click(function () {
                $(".addToProductSetPanelHidden").addClass("hidden");
                $(".addToProductSetPanelContainer").removeClass("hidden1");
                $(".addToProductSetPanel").removeClass("hidden");
            });

            $("#checkPanelHide").click(function () {
                $(".addToProductSetPanelHidden").removeClass("hidden");
                $(".addToProductSetPanelContainer").addClass("hidden1");
                $(".addToProductSetPanel").addClass("hidden");
            });


            $(".bgMarker").click(function () {
                //$(".addToProductSetPanel").removeClass("hidden");
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

                var bar = $(this).find(".commentsBar");
                bar.addClass("selected");
                
            }, function () {
                var obj = $(this).find(".bgMarker");
                obj.removeClass("selected");

                var bar = $(this).find(".commentsBar");
                bar.removeClass("selected");
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
