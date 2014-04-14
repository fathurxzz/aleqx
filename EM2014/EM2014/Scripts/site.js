$(function () {

    $(window).scroll(function () {
        if ($(this).scrollTop() > 262) {
            $("#menu").addClass("fixed").addClass("small");

        } else {
            $("#menu").removeClass("fixed").removeClass("small");
        }

        var scrollValue = $(this).scrollTop() - 263;
        
        if (scrollValue > 0 && scrollValue < 50) {
            var calcScrollValue = scrollValue / 11;
            var calculatedFontSize = 13 - Math.round(calcScrollValue);
            $("#menu").css("font-size", calculatedFontSize);
            var divHeight = 104 - scrollValue;
            $("#menu").css("height", divHeight);
            var liWidth = 80 - Math.round(scrollValue * 0.6);
            $("#menu > li").css("width", liWidth);
        } else {
            if (scrollValue < 0) {
                $("#menu").css("font-size", "13px");
                $("#menu").css("height", "102px");
                $("#menu > li").css("width", "50px;");
            } else if (scrollValue > 50) {
                $("#menu").css("font-size", "9px");
                $("#menu").css("height", "55px");
                $("#menu > li").css("width", "80px;");
            }
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