var ProjectCatalogue = {
    initialize: function () {
        $(function () {
            $(".fancyPopUp").fancybox({ showCloseButton: true, padding: 0 });
            $(".fancyImage").fancybox({ showCloseButton: false, padding: 0,hideOnContentClick:true });
        });
    },

    setFirstImage: function () {
        var fileName = $(".productPreviews").find("img").attr("alt");
        var productId = $("#firstImageId").val();
        $(".productPreviews").find(".selectedArea").first().addClass("active");
        ProjectCatalogue.changeImage(fileName, productId, null);
    },

    _updateImageContainer: function (fileName, productId) {
        $("#pictureContainer").attr("src", "/ImageCache/product/" + fileName);
        //$("#pictureLink").attr("href", "/Home/ProductDetails/" + productId);
        $("#zoomPictureLink").attr("href", "/Content/Images/" + fileName);
    },

    changeImage: function (fileName, productId, obj) {
        if (obj != null) {
            $(".selectedArea").each(function () {
                $(this).removeClass("active");
            });
            $(obj).find(".selectedArea").addClass("active");
        }
        
        $.post("/Catalogue/UpdateProjectImage?productId=" + productId, function (data) {
            ProjectCatalogue._updateImageContainer(fileName, productId);
            $(".description").html(data);
        });
    }
};