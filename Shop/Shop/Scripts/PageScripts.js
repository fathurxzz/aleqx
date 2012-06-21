var BasePageExtender = {
    favoritesCount: 0,
    initialize: function BasePageExtender_initialize() {
        $(function () {
            //            $("#ourCoords, .editContentLink").fancybox(
            //            {
            //                hideOnContentClick: false,
            //                hideOnOverlayClick: false,
            //                showCloseButton: true,
            //                titlePosition: "over",
            //                onComplete: function () {
            //                    $("#fancybox-content").get(0).children[0].style.zIndex = 100000000;
            //                }
            //            });




            if (!window.isHomePage) {
                $("#logo").css("cursor", "pointer").click(function () { location.href = "/"; });
            }

            if (!window.showAllBrandList) {
                $("#brandsList").css("height", "311px");
                $("#brandsList").css("overflow", "hidden");
            } else {
                $("#moreBrands").css("display", "none");
            }

            //            $("#searchField").watermark({ html: window.wordEnter, cls: "watermark small" });


            $("#moreBrands").click(function () {
                $("#brandsList").css("height", "auto");
                $("#moreBrands").css("display", "none");
            });


        });
    }

};
