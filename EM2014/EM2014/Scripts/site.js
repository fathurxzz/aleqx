$(function () {

    $(window).scroll(function () {
        if ($(this).scrollTop() > 262) {
            //$('a.topLink').fadeIn();
            $("#menu").addClass("fixed");

        } else {
            $("#menu").removeClass("fixed");
        }
    });

});