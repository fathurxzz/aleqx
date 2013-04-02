﻿var PresentationPageExtender = {
    favoritesCount: 0,
    enables: {},
    initialize: function PresentationPageExtender_initialize() {
        $(function () {
            if (!window.isHomePage) {
                $("#logo").css("cursor", "pointer").click(function () { location.href = "/presentation"; });
            }

            $(".addToProductSetPanel").scrollFollow({
                offset: 200
            });

            /*
            $("#selectAll").click(function () {


            if ($(this).attr('checked')) {

            $(".cbx").each(function () {
            $(this).attr("checked", "checked");
            });
            } else {
            $(".cbx").each(function () {
            $(this).removeAttr("checked");
            });
            }

            });
            */



            $(".product").click(function () {

                var id = $(this).attr("id");

                var obj = $(this).find(".selectedMarker");
                if (obj.hasClass("selected")) {
                    obj.removeClass("selected");
                    PresentationPageExtender.enables[id] = 0;
                } else {
                    obj.addClass("selected");
                    PresentationPageExtender.enables[id] = 1;
                }

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
