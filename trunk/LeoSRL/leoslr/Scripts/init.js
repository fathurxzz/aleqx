$(function () {

    $(".block").click(function () {
        var contentName = $(this).attr("id");
        $(".intro-container").fadeOut(1000, function () {
            $(".imagine").fadeIn(2000, function () {
                $(".imaginelogo").fadeIn(1000, function () {
                    $(".imagine").fadeOut(1000, function () {
                        $(".hand-container").css("display", "block");
                        $(".hand-wrapper").animate({ left: "+=260" }, function () {
                            setTimeout(function () {
                                $(".hand").animate({ left: "-=260" }, function () {
                                    setTimeout(function () {
                                        location.href = 'ru/' + contentName+"?fadein=1";
                                    }, 1000);
                                });
                            }, 300);
                        });
                    });
                });
            });
        });
    });


    $("#page").fadeIn(1000, function () {

    });

    //$(".hand-wrapper").animate({ left: "+=250" }, function () {
    //    setTimeout(function () {
    //        $(".hand").animate({ left: "-=250" }, function () {

    //        });
    //    }, 300);
    //});




    if (LEO.settings != null) {
        if ('randomImageFromProductImages' in LEO.settings) {
            //console.log(LEO.settings.randomImageFromProductImages);
            var randombg = 'url(/content/images/' + LEO.settings.randomImageFromProductImages + ')';
            $("#bg-layer").css('background-image', randombg);
            $("#bg-layer").css('opacity', '1');
        }


        //console.log(LEO.settings.specialContent);
        //console.log(LEO.settings.randomImageFromProductImages);

        if (LEO.settings.randomImageFromProductImages == undefined) {

            var i = 0;
            var delay = 5000;


            var changeImage = function() {
                $("#bg-layer").animate({ opacity: 0 }, 'slow', function() {
                    if (LEO.settings.specialContent != undefined) {
                        if (i == LEO.settings.specialContent.items.length) {
                            i = 0;
                        }
                        //console.log(i);
                        var item = LEO.settings.specialContent.items[i];
                        //console.log(item);
                        var pagebg = 'url(' + LEO.settings.specialContent.imagePath + item.pageImageSource + ')';
                        var contentbg = 'url(' + LEO.settings.specialContent.imagePath + item.contentImageSource + ')';
                        i++;
                        $(this).css({ 'background-image': pagebg }).animate({ opacity: 1 });
                        $("#title").html(item.title);
                        $("#text").html(item.text);
                        $("#contentFrame").css("background-image", contentbg);
                    }
                });
            };

            var changeImageFadeInOnly = function() {
                if (LEO.settings.specialContent != undefined) {
                    if (i == LEO.settings.specialContent.items.length) {
                        i = 0;
                    }
                    //console.log(i);
                    var item = LEO.settings.specialContent.items[i];
                    //console.log(item);
                    var pagebg = 'url(' + LEO.settings.specialContent.imagePath + item.pageImageSource + ')';
                    var contentbg = 'url(' + LEO.settings.specialContent.imagePath + item.contentImageSource + ')';
                    i++;
                    $("#bg-layer").css({ 'background-image': pagebg }).animate({ opacity: 1 }, 'slow', function() {
                        $("#contentFrameWrapper").css("display", "block");
                        $("#title").html(item.title);
                        $("#text").html(item.text);
                        $("#contentFrame").css("background-image", contentbg);
                    });
                }
            };

            changeImageFadeInOnly();
            setInterval(changeImage, delay);
        }

    }


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


    $("#productSelector").change(function () {
        window.location.href = '/' + LEO.settings.language + '/' + LEO.settings.category + '/' + LEO.settings.subcategory + '/' + this.value;
    });


});