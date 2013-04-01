var PresentationPageExtender = {
    favoritesCount: 0,
    enables: {},
    initialize: function PresentationPageExtender_initialize() {
        $(function () {
            if (!window.isHomePage) {
                $("#logo").css("cursor", "pointer").click(function () { location.href = "/presentation"; });
            }

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
                    PresentationPageExtender.enables[id] = 1;
                } else {
                    obj.addClass("selected");
                    PresentationPageExtender.enables[id] = 0;
                }

            });

        });
    },

    collectChanges: function PresentationPageExtender_collectChanges() {
        var enablities = $("#enablities");
        enablities.val(Sys.Serialization.JavaScriptSerializer.serialize(PresentationPageExtender.enables));
        return true;
    }
};
