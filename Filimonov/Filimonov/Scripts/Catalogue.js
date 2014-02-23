var ProjectCatalogue = {
    initialize: function () {
        $(function () {
            //$(".fancy").fancybox({ hideOnContentClick: true, showCloseButton: false, cyclic: true, showNavArrows: false, padding: 0, margin: 0, centerOnScroll: true });
            $(".fancy").fancybox({ hideOnContentClick: false, showCloseButton: false, cyclic: true, showNavArrows: true, padding: 0, margin: 0, centerOnScroll: true });
            //$(".fancy").fancybox({ showNavArrows: true, showCloseButton: false, cyclic: true });

            $(".fancy, #fancybox-outer").bind("contextmenu", function (e) {
                //alert("aaa");
                return false;
                //$('.alert').fadeToggle();
            });
            /* fancybox-wrap */
//            $("#fancybox-outer").bind("contextmenu", function (e) {
//                //alert("aaa");
//                return false;
//                //$('.alert').fadeToggle();
//            });
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
    
        $(".pictureLink1").each(function () {
            $(this).css("z-index", "1");
        });
        $("#image" + fileName).css("z-index", "1001");

//        $.post("/Catalogue/UpdateProjectImage?fileName=" + fileName, function () {
//            ProjectCatalogue._updateImageContainer(fileName);
//        });
    }
};