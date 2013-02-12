var ProjectCatalogue = {
    initialize: function () {
        $(function () {
            $(".fancy").fancybox({ hideOnContentClick: true, showCloseButton: false, cyclic: true, showNavArrows: false, padding: 0, margin: 0, centerOnScroll: true });
        });
    },

    setFirstImage: function () {
            var fileName = $(".carusel-previews").find("img").attr("alt");
            ProjectCatalogue._updateImageContainer(fileName);
    },

    _updateImageContainer: function (fileName) {
        $("#pictureContainer").attr("src", "/ImageCache/projectImage/" + fileName);
        $("#pictureLink").attr("href", "/Content/Images/" + fileName);
    },

    changeImage: function (fileName) {
        $.post("/Catalogue/UpdateProjectImage?fileName=" + fileName, function () {
            ProjectCatalogue._updateImageContainer(fileName);
        });
    }
};