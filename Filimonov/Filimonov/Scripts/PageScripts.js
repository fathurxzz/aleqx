$(function () {

    //    $.scrollingParallax('content/img/site-bg.jpg');

//    $('div.main-page').scrollingParallax({
//        staticSpeed: .2
//    });

    $.scrollingParallax('content/img/site-bg.jpg', {
        staticSpeed: .2
    });

    $(".hideLink").click(function () {
        $(this).closest(".frame").animate({ height: 'toggle' }, function () {
            $(this).closest(".container").find(".menuItem").css("display", "block");
        });
    });

    $(".showLink").click(function () {
        collapseAll();
        $(this).parent().css("display", "none");
        $(this).closest(".container").find(".frame").animate({ height: 'toggle' });
    });



    $(".project").mouseover(function () {
        $(this).find(".layer").css("display", "table-cell");
    });

    $(".project").mouseout(function () {
        $(this).find(".layer").css("display", "none");
    });


    function collapseAll() {
        $(".frame").css("display", "none");
        $(".menuItem").css("display", "block");
    }


    $(".hide").scrollFollow({
        offset: 0
    });


});