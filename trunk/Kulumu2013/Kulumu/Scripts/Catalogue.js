var ProjectCatalogue = {
    initialize: function () {
        $(function () {

        });
    },

    setFirstImage: function () {
        var fileName = $("#productPreviews").find("img").attr("alt");
        //ProjectCatalogue.changeImage(fileName);
        ProjectCatalogue._updateImageContainer(fileName);
    },

    _updateImageContainer: function (fileName) {
        $("#pictureContainer").attr("src", "/ImageCache/productPreview/" + fileName);
        $("#pictureLink").attr("href", "/Content/Images/" + fileName);
    },

    changeImage: function (fileName) {
        $.post("/Catalogue/UpdateProjectImage?fileName=" + fileName, function () {
            ProjectCatalogue._updateImageContainer(fileName);
        });
    }
};