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

        var x = location.href.split("/");
        while (x.length > 6) {
            x.pop();
        }

        var urlWithoutFilters = x.join("/");
        var urlHasParams = location.href.indexOf("?") > -1;


        // if uncheck last checked checkbox
        if (filterValue == "") {

            //var urlHasParams = location.href.indexOf("?") > -1;
            //if (urlHasParams) {
            //    location.href = location.href.replace(location.href.substring(location.href.lastIndexOf("/"), location.href.indexOf("?")), "");
            //} else {
            //    location.href = location.href.replace(location.href.substring(location.href.lastIndexOf("/"), location.href.length), "");
            //}

            location.href = urlWithoutFilters;

        } else {

            //location.href = location.href + "/" + filterValue+"/";

           

            location.href = urlWithoutFilters + "/" + filterValue;

            //if (location.href.split("/").length == 6 ) {
            //    location.href = location.href + "/" + filterValue;
            //} else {
            //    location.href = filterValue;
            //}
        }
    });



});
