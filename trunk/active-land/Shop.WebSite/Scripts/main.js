$(function () {

    $(".categories-menu-link").mousemove(function() {
        $(".category-menu").css("visibility", "visible");
    });

    var obj = $("#category-menu");
    if (obj[0] != undefined) {
        var width = obj[0].offsetWidth;

        $("html").click(function() {
            $(".category-menu").css("visibility", "hidden");
        });


        $(".sub-menu").each(function() {
            $(this).css("left", width + 1 + "px");
        });

        $("#category-menu").find("li.main").mouseenter(function() {

            $(".sub-menu").each(function() {
                $(this).removeClass("show");
            });

            $(this).find(".sub-menu")
                    .addClass("show")
                //.css("margin-top", + "-20px")
                ;
        });


//    $("#category-menu").find("li").mouseleave(function () {
//        //setTimeout(function(){
//            $("#category-menu").find("li").find("ul").css("display", "none");
//        //},2000);
//
//    });

    }
});