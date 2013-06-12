var ProjectCatalogue = {
    initialize: function () {
        $(function () {
            $(".fancyPopUp").fancybox({ showCloseButton: true, padding: 0 });
        });
    },

    setFirstImage: function () {
        var fileName = $(".productPreviews").find("img").attr("alt");
        //var productId = $("#productId").val();
        var imageId = $("#firstImageId").val();
        var description = $("#firstImageDescription").val();

        $(".productPreviews").find(".selectedArea").first().addClass("active");



        ProjectCatalogue.changeImage(fileName, /* productId,*/imageId, description, null);
    },

    _updateImageContainer: function (fileName, /*productId,*/imageId, description) {

        $("#pictureContainer").attr("src", "/ImageCache/product/" + fileName);


        $("#pictureLink").attr("href", "/Home/ProductDetails/" + imageId);
        //alert(description);

        $(".description").html("Все майки - 100% коттон, индивидуальный пошив и шелкографическое нанесение. " + description);
    },

    changeImage: function (fileName, /* productId, */imageId, description, obj) {

        if (obj != null) {

            $(".selectedArea").each(function () {
                $(this).removeClass("active");
            });

            $(obj).find(".selectedArea").addClass("active");
        }

        $.post("/Catalogue/UpdateProjectImage?fileName=" + fileName, function () {
            ProjectCatalogue._updateImageContainer(fileName, /* productId,*/imageId, description);
        });
    }
};