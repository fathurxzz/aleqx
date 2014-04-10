$(function () {

    $(window).scroll(function () {
        if ($(this).scrollTop() > 262) {
            //$('a.topLink').fadeIn();
            $("#menu").addClass("fixed");

        } else {
            $("#menu").removeClass("fixed");
        }
    });
    

    if (!window.isHomePage) {
        $("#logo").css("cursor", "pointer").click(function () { location.href = "/"; });
    }

    $(".current-bg.current").css("display", "block");

});


function beginreq(obj) {
    //$(obj).parent().addClass("current");

    //obj).removeClass("current");
    $(".current-bg").fadeOut();
    //$("#productsContainer").fadeOut(200);
}

function endreq(obj) {
    var id = "#" + $(obj).attr("id");
    $(id + " > div.current-bg-container > div.current-bg").fadeIn();
    //$("#productsContainer").fadeIn(200);
}