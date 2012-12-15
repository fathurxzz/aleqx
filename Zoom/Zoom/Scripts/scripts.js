var scene = function (elem) {
    var t = this;
    t.element = $("#container1");
    t.id = t.element.attr('id');


    var conteinerWidth = $("#container1").width();
    var conteinerHeight = $("#container1").height();



    var image = $("#image1");
    var imageWidth = image.width();
    var imageHeight = image.height();




    $(document).mousemove(function (e) {
        var width = conteinerWidth;
        var height = conteinerHeight;
        var left = e.pageX - t.element.offset().left;
        var top = e.pageY - t.element.offset().top;



        var hOffsetLength = imageWidth - conteinerWidth;
        var vOffsetLength = imageHeight - conteinerHeight;
        var hRatio = hOffsetLength / conteinerWidth;
        var vRatio = vOffsetLength / conteinerHeight;

        if (left > 0 && left < width && top > 0 && top < height) {
            image.css({ left: -left * hRatio, top: -top * vRatio });
        }

    });

};


scene.prototype = {

    aaa: function () {
        return {
            top: 1,
            right: 2
        };
    }

    //    refreshRange: function () {
    //        var t = this;
    //        if (t.element.is(':visible')) return {
    //            top: t.element.offset().top,
    //            right: t.element.offset().left + t.element.width(),
    //            bottom: t.element.offset().top + t.element.height(),
    //            left: t.element.offset().left
    //        };
    //        else return false;
    //    }

};


var obj = new Object();

obj.function1 = function () {
    alert("111");
};

$(function () {

    $(".container").each(function () {
        new scene($(this));
    });



    $(".container").mouseover(function () {
        $("#thumb").css("display", "none");
        $("#image1").css("display", "block");
    });

    $(".container").mouseout(function () {
        $("#image1").css("display", "none");
        $("#thumb").css("display", "block");
        $("#thumb").css("left", "0");
        $("#thumb").css("top", "0");
    });


});