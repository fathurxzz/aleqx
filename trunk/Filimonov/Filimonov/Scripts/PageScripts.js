$(function () {

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

    function collapseAll() {
        $(".frame").css("display", "none");
        $(".menuItem").css("display", "block");
    }

});