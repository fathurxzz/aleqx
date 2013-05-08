var BasePageExtender = {
    favoritesCount: 0,
    initialize: function BasePageExtender_initialize() {
        $(function () {

            //$(".fancyImage").fancybox({ hideOnContentClick: true, showCloseButton: true, cyclic: true, showNavArrows: true, padding: 0, margin: 0, centerOnScroll: true });

            $(".fancybox").fancybox();
            
            //$(function () {
                //$(".fancyImage").fancybox({ hideOnContentClick: true, showCloseButton: true, cyclic: true, showNavArrows: true, padding: 0, margin: 0, centerOnScroll: true });
            //});


            if (!window.isHomePage) {
                $("#logo").css("cursor", "pointer").click(function () { location.href = "/"; });
            }
        });
    }

};