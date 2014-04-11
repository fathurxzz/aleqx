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


function changeMenuStateBegin(obj) {
    //$(obj).parent().addClass("current");

    //obj).removeClass("current");
    $(".current-bg").fadeOut();
    //$("#productsContainer").fadeOut(200);
}

function changeMenuStateEnd(obj) {
    
    var id = "#" + $(obj).attr("id");
    //alert(id);

    $(".menuitemclass").removeClass("selected");
    //$(".menuitemclass").remove("&laquo;");

    $(id + " > div.current-bg-container > div.current-bg").fadeIn();
}


function showProduct(contentname) {
    var menuid = "#menuitem_" + contentname;
    //alert(param);
    $(".menu-container").addClass("product-details");

    $(menuid).addClass("selected");
        //.append("&laquo;");
    
}