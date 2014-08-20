$(function () {

    var obj = $("#category-menu");

    $(".categories-menu-link").mousemove(function () {
        if (obj[0] != undefined) {
            var width = obj[0].offsetWidth;
            $(".sub-menu").each(function () {
                $(this).css("left", width + 10 + "px");
            });
            }

        $(".category-menu").css("visibility", "visible");
    });

    
    if (obj[0] != undefined) {
        var width = obj[0].offsetWidth;

        $("html").click(function() {
            $(".category-menu").css("visibility", "hidden");
        });


        $(".sub-menu").each(function() {
            $(this).css("left", width + 10 + "px");
        });

        $("#category-menu").find("li.main").mouseenter(function() {

            $(".sub-menu").each(function() {
                $(this).removeClass("show");
            });

            $(this).find(".sub-menu")
                    .addClass("show")
                ;
        });


    }

    $(".attribute-checkbox").change(function() {
        var filterValue = $(this).attr("filter");
        
        if (filterValue == "") {
            location.href = location.href.replace(location.href.substring(location.href.lastIndexOf("/"), location.href.length), "");
        } else {
            if (location.href.split("/").length == 7) {
                location.href = location.href+ "/" + filterValue;
            } else {
                location.href =  filterValue;
            }
        }
    });


});
