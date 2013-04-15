var ProjectCatalogue = {
    initialize: function () {
        $(function () {

        });
    },

    setFirstImage: function () {
        var fileName = $("#productPreviews").find("img").attr("alt");
        var productId = $("#productId").val();
        var imageId = $("#firstImageId").val();
        ProjectCatalogue.changeImage(fileName, productId, imageId);
    },

    _updateImageContainer: function (fileName, productId, imageId) {
        $("#pictureContainer").attr("src", "/ImageCache/productPreview/" + fileName);
        //$("#pictureLink").attr("href", "/Content/Images/" + fileName);
        $("#pictureLink").attr("href", "/Home/ProductDetailsPopUp/" + productId + "/" + imageId);
        //$("#pictureLink").attr("href", "/Home/ProductDetailsPopUp/" + productId);
        //$("#pictureLink").attr("href", "/Home/ProductDetailsPopUp/1/141.jpg");

    },

    changeImage: function (fileName, productId, imageId) {
        $.post("/Catalogue/UpdateProjectImage?fileName=" + fileName, function () {
            ProjectCatalogue._updateImageContainer(fileName, productId, imageId);
        });
    }
};