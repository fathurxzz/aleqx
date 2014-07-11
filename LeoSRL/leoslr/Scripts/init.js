$(function () {
    $(".block").click(function () {
        var contentName = $(this).attr("id");
        $(".intro-container").fadeOut(1000, function () {
            $(".imagine").fadeIn(1000, function () {
                setTimeout(function () {
                    $(".imagine").fadeOut(1000, function () {
                        location.href = 'ru/'+contentName;
                    });
                }, 2000);
            });
        });
    });

    console.log(LEO.settings.specialContent);

    var i = 0;
    var delay = 5000;

    var changeImage = function () {
        $("#bg-layer").animate({opacity: 0}, 'slow', function () {
            if (i == LEO.settings.specialContent.items.length) {
                i = 0;
            }
            console.log(i);
            var item = LEO.settings.specialContent.items[i];
            console.log(item);
            var pagebg = 'url(' + LEO.settings.specialContent.imagePath + item.pageImageSource + ')';
            var contentbg = 'url(' + LEO.settings.specialContent.imagePath + item.contentImageSource + ')';
            i++;
            $(this).css({'background-image': pagebg }).animate({opacity: 1});
            $("#title").html(item.title);
            $("#text").html(item.text);
            $("#contentFrame").css("background-image", contentbg);

        });
    };

    var changeImageFadeInOnly = function () {
            if (i == LEO.settings.specialContent.items.length) {
                i = 0;
            }
            console.log(i);
            var item = LEO.settings.specialContent.items[i];
            console.log(item);
            var pagebg = 'url(' + LEO.settings.specialContent.imagePath + item.pageImageSource + ')';
            var contentbg = 'url(' + LEO.settings.specialContent.imagePath + item.contentImageSource + ')';
            i++;
            $("#bg-layer").css({ 'background-image': pagebg }).animate({ opacity: 1 }, 'slow', function () {
                $("#contentFrameWrapper").css("display","block");
                $("#title").html(item.title);
                $("#text").html(item.text);
                $("#contentFrame").css("background-image", contentbg);
            });
    };

    changeImageFadeInOnly();
    setInterval(changeImage, delay);
    

    $("#contentFrameWrapper").draggable(
        {
            // cursor: "move",
            // axis: "y",
            containment: "#page",
            scroll: false
        });

    $("#productFrameWrapper").draggable(
        {
            // cursor: "move",
            // axis: "y",
            containment: "#page",
            scroll: false
        });


});