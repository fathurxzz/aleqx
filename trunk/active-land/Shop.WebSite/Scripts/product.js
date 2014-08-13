var ProductCatalogue = {
    initialize: function () {
        $(function () {
            $(".fancy").fancybox({ hideOnContentClick: false, showCloseButton: false, cyclic: true, showNavArrows: true, padding: 0, margin: 0, centerOnScroll: true });
        });
    },

    setFirstImage: function () {
        var fileName = $("#product-previews").find("img").attr("alt");
        $("#pictureLink").attr("href", "/Content/Images/" + fileName);
    },

    changeImage: function (fileName) {
        $(".pictureLink1").each(function () {
            $(this).css("z-index", "1");
        });
        $("#image" + fileName).css("z-index", "1001");
    }
};