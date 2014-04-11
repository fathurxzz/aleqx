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
    



    $('a').address();


    $.address.change(function (event) {
        //alert(event.value);
        $("#ajaxUpdatePanel").load(event.value);
    });

    //$('a').address(function () {
    //    return $(this).attr('href').replace(/^#/, '');
    //});

});


function changeMenuStateBegin(obj) {
    //$(obj).parent().addClass("current");

    //obj).removeClass("current");
    $(".current-bg").fadeOut(100);
    //$("#productsContainer").fadeOut(200);
    //$("#ajaxUpdatePanel").animate();

}

function changeMenuStateEnd(obj) {
    
    var id = "#" + $(obj).attr("id");
    //alert(id);

    $(".menuitemclass").removeClass("selected");
    //$(".menuitemclass").remove("&laquo;");

    $(id + " > div.current-bg-container > div.current-bg").fadeIn();
    
    $(".menu-container").removeClass("product-details");
    //$("#ajaxUpdatePanel").fadeIn();
    //$("#ajaxUpdatePanel").animate();
}


function showProduct(contentname) {
    var menuid = "#menuitem_" + contentname;
    //alert(param);
    $(".menu-container").addClass("product-details");

    $(menuid).addClass("selected");
        //.append("&laquo;");
    
}