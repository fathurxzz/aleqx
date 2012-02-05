<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


<%--<iframe width="640" height="480" frameborder="0" scrolling="no" marginheight="0" marginwidth="0" src="http://maps.google.com/maps?f=q&amp;source=s_q&amp;hl=en&amp;geocode=&amp;q=%D0%B3%D0%BE%D1%80%D1%8C%D0%BA%D0%BE%D0%B3%D0%BE+91%2F14&amp;aq=&amp;sll=50.424809,30.515905&amp;sspn=0.002755,0.006968&amp;vpsrc=6&amp;ie=UTF8&amp;hq=&amp;hnear=Hor'koho+St,+91%2F14,+Kiev,+Kyiv+city,+Ukraine&amp;t=h&amp;ll=50.425308,30.515749&amp;spn=0.006562,0.013733&amp;z=16&amp;output=embed"></iframe>
<br />
<small><a target="_blank" href="http://maps.google.com/maps?f=q&amp;source=embed&amp;hl=en&amp;geocode=&amp;q=%D0%B3%D0%BE%D1%80%D1%8C%D0%BA%D0%BE%D0%B3%D0%BE+91%2F14&amp;aq=&amp;sll=50.424809,30.515905&amp;sspn=0.002755,0.006968&amp;vpsrc=6&amp;ie=UTF8&amp;hq=&amp;hnear=Hor'koho+St,+91%2F14,+Kiev,+Kyiv+city,+Ukraine&amp;t=h&amp;ll=50.425308,30.515749&amp;spn=0.006562,0.013733&amp;z=16" style="color:#0000FF;text-align:left">Посмотреть на карте побольше</a></small>--%>


<!-- Этот блок кода нужно вставить в ту часть страницы, где вы хотите разместить карту  (начало) -->
<script src="http://api-maps.yandex.ru/1.1/?key=AFVKLk8BAAAATo4HIwIAJOCZ9sM2vCJxpGXyjDdM41esbUIAAAAAAAAAAADcGi1B_bUNDKr6deq8wxQvsmxoDw==&modules=pmap&wizard=constructor" type="text/javascript"></script>
<script type="text/javascript">
    YMaps.jQuery(window).load(function () {
        var map = new YMaps.Map(YMaps.jQuery("#YMapsID-2432")[0]);
        map.setCenter(new YMaps.GeoPoint(30.515354, 50.425449), 17, YMaps.MapType.MAP);
        map.addControl(new YMaps.Zoom());
        map.addControl(new YMaps.ToolBar());
        YMaps.MapType.PMAP.getName = function () { return "Народная"; };
        map.addControl(new YMaps.TypeControl([
            YMaps.MapType.MAP,
            YMaps.MapType.SATELLITE,
            YMaps.MapType.HYBRID,
            YMaps.MapType.PMAP
        ], [0, 1, 2, 3]));

        YMaps.Styles.add("constructor#pmlbmPlacemark", {
            iconStyle: {
                href: "http://api-maps.yandex.ru/i/0.3/placemarks/pmlbm.png",
                size: new YMaps.Point(28, 29),
                offset: new YMaps.Point(-8, -27)
            }
        });

        map.addOverlay(createObject("Placemark", new YMaps.GeoPoint(30.515891, 50.424983), "constructor#pmlbmPlacemark", "Творча архітектурна майстерня<br/>\"Бабич\"<br/>Горького 91/14"));

        function createObject(type, point, style, description) {
            var allowObjects = ["Placemark", "Polyline", "Polygon"],
                index = YMaps.jQuery.inArray(type, allowObjects),
                constructor = allowObjects[(index == -1) ? 0 : index];
            description = description || "";

            var object = new YMaps[constructor](point, { style: style, hasBalloon: !!description });
            object.description = description;

            return object;
        }
    });
</script>

<div id="YMapsID-2432" style="width:450px;height:350px"></div>
<!--<div style="width:450px;text-align:right;font-family:Arial"><a href="http://api.yandex.ru/maps/tools/constructor/" style="color:#1A3DC1">Создано с помощью инструментов Яндекс.Карт</a></div>
 Этот блок кода нужно вставить в ту часть страницы, где вы хотите разместить карту (конец) -->