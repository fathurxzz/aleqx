var scene = function (elem) {
    var t = this;
    t.element = elem;
    t.id = t.element.attr('id');
    var conteinerWidth = elem.width();
    var conteinerHeight = elem.height();

    var image = $("#container1 > div");
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
    //var sc = new scene($("#container1"));

    //$("#container1").ready(new scene(this));

    $(".container").each(function () {
        new scene($(this));
    });

    //.mousemove(scene());





});