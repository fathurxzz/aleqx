var scene = function(elem) {
    var t = this;
    t.element = elem;
    t.id = t.element.attr('id');
    t.center = t.element[0].onclick();
    t.coof = {x: t.element.width() / 2 > t.center.x ? t.element.width() - t.center.x : t.center.x,
        y: t.element.height() / 2 > t.center.y ? t.element.height() - t.center.y : t.center.y};
    t.range = t.refreshRange();
    t.layers = Array();
    t.element.find('.layer').each(function(){
        t.layers.push(new t.layer({element:$(this), coof: t.coof, p: t}));
    });
    $(document).mousemove(function(e){
        if (t.range && e.pageX >= t.range.left && e.pageX <= t.range.right &&  e.pageY >= t.range.top && e.pageY <= t.range.bottom) {
            t.refreshLayers({x: e.pageX - t.range.left - t.center.x, y: e.pageY - t.range.top - t.center.y})
        }
        else return;
    });
    $(window).resize(function(){t.range = t.refreshRange();});
};

scene.prototype =  {
    layer: function(params) {
        var t = this;
        t.element = params.element;
        t.offset = {x: parseInt(t.element.css('left')), y: parseInt(t.element.css('top'))};
        t.coof = {x: params.coof.x / t.offset.x, y: params.coof.y / t.offset.y};
        if (t.element.is('.pointers')) {
            t.pointers = Array();
            t.element.find('.pointer').each(function(){
                t.pointers.push(new params.p.pointer({element: $(this), coof: t.coof}));
            });
        }
        t.move = function(params) {
            var left = params.x / t.coof.x + t.offset.x;
            var top = params.y / t.coof.y + t.offset.y;
            t.element.css({left: left, top: top});
            if (t.pointers) {
                for (var i=0; i<t.pointers.length; i++) {t.pointers[i].move(params);}
            }
        };
    },

    refreshRange : function() {
        var t = this;
        if (t.element.is(':visible')) return {
            top: t.element.offset().top,
            right: t.element.offset().left + t.element.width(),
            bottom: t.element.offset().top + t.element.height(),
            left: t.element.offset().left
        };
        else return false;
    },

    refreshLayers : function(params) {
        var t = this;
        for (var i=0; i<t.layers.length; i++) {t.layers[i].move(params);}
    }
};

$(document).ready(function() {
    $('.scene').each(function(){
        new scene($(this));
    });
});