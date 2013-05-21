var scene = function (elem) {
    var t = this;
    t.element = elem;
    //t.id = t.element.attr('id');

    t.center = { x: 200, y: 200 };

    t.coof = { x: t.element.width() / 2 > t.center.x ? t.element.width() - t.center.x : t.center.x,
        y: t.element.height() / 2 > t.center.y ? t.element.height() - t.center.y : t.center.y
    };

    t.range = t.refreshRange();

    t.layers = Array();

    t.element.find('.layer').each(function () {
        t.layers.push(new t.layer({ element: $(this), coof: t.coof, p: t }));
    });

    $(document).mousemove(function (e) {

        if (t.range && e.pageX >= t.range.left && e.pageX <= t.range.right /*&& e.pageY >= t.range.top && e.pageY <= t.range.bottom*/) {

            t.refreshLayers({ x: ((e.pageX - t.range.left) / 20) - 25, y: 0});

        }

        else return;
    });
};

scene.prototype = {
    layer: function (params) {
        var t = this;
        t.element = params.element;

        t.offset = { x: parseInt(t.element.css('left')), y: parseInt(t.element.css('top')) };

        t.coof = { x: params.coof.x / t.offset.x, y: params.coof.y / t.offset.y };

        t.move = function (params) {


            //$(".toplayer").html(params.x);
            params.x = params.x * 0.5;
            
            

            var left = params.x;
            var top = params.y;
            //var left = params.x / t.coof.x + t.offset.x;
            //var top = params.y / t.coof.y + t.offset.y;

            t.element.css({ left: left, top: top });
        };
    },

    refreshRange: function () {
        var t = this;
        if (t.element.is(':visible')) return {
            top: t.element.offset().top,
            right: t.element.offset().left + t.element.width(),
            bottom: t.element.offset().top + t.element.height(),
            left: t.element.offset().left
        };
        else return false;
    },

    refreshLayers: function (params) {
        var t = this;
        for (var i = 0; i < t.layers.length; i++) { t.layers[i].move(params); }
    }
};
