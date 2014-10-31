$(function () {

    var obj = $("#category-menu");
    var hideMenu = true;


    $(".categories-menu-link").mousemove(function () {
        if (obj[0] != undefined) {
            var width = obj[0].offsetWidth;
            $(".sub-menu").each(function () {
                $(this).css("left", width + 10 + "px");
            });
        }

        $(".category-menu").css("visibility", "visible");
        hideMenu = false;
    });


    $(".category-menu").mouseout(function () {
        hideMenu = true;
        setTimeout(function () {
            if (hideMenu) {
                $(".category-menu").css("visibility", "hidden");
            }
        }, 2000);
    });


    if (obj[0] != undefined) {
        var width = obj[0].offsetWidth;

        $("html").click(function () {
            $(".category-menu").css("visibility", "hidden");
        });


        $(".sub-menu").each(function () {
            $(this).css("left", width + 10 + "px");
        });

        $("#category-menu").find("li.main").mouseenter(function () {

            $(".sub-menu").each(function () {
                $(this).removeClass("show");
            });

            $(this).find(".sub-menu")
                    .addClass("show")
            ;
        });
    }

    $(".attribute-checkbox").change(function () {
        var filterValue = $(this).attr("filter");




        //x.reduce(function (previousStr, currentItem, i) {
        //    alert(i);
        //    var str = previousStr + "/" + currentItem;
        //    alert(str); // a, ab, abc, abcd
        //    return str;
        //}, "");

        var url = location.href;

        if (location.href.indexOf("?") > -1) {
            var tmp = url.split("?");
            url = tmp[0];
        }

        var x = url.split("/");
        while (x.length > 6) {
            x.pop();
        }

        var urlWithoutFilters = x.join("/");
        var urlHasParams = location.href.indexOf("?") > -1;
        var urlParams = location.href.substring(location.href.indexOf("?"), location.href.length);


        // if uncheck last checked checkbox
        if (filterValue == "") {
            location.href = urlWithoutFilters + urlParams;
        } else {
            if (urlHasParams) {
                location.href = urlWithoutFilters + "/" + filterValue + "/" + urlParams;
            } else {
                location.href = urlWithoutFilters + "/" + filterValue;
            }
            //} else {
            //    location.href = filterValue;
            //}
        }
    });



});
