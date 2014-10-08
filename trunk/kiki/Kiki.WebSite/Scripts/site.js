$(function () {


    if (!window.isHomePage) {
        $("#logo").css("cursor", "pointer").click(function () { location.href = "/"+window.lang; });
    }

    $("#search").focus();

    $(".fancy-close-btn").click(function() {
        $(".layout").hide();
    });


    

    $(".fancyMap").click(function() {
        $(".layout").show();
    });

    $(".fancy").fancybox({ hideOnContentClick: false, showCloseButton: false, cyclic: true, showNavArrows: true, padding: 0, margin: 0, centerOnScroll: true });

    $(".fancyPanel").fancybox({ hideOnContentClick: false, hideOnOverlayClick: true, showNavArrows: false, padding: 0, margin: 0, centerOnScroll: true, overlayShow: true, showCloseButton: true});

    $("#all-services-toggle-link").click(function() {
        $("#all-service-items").toggle("slow", function() {
            
        });
    });


    var query;

    $("#search").keyup(function () {
        var q = $(this).val();

        if (q.trim().length >= 3) {
            if (query != q) {
                query = q;

                $.ajax({
                    url: '/' + window.lang + '/search/' + q,
                    dataType: "json",
                    success: function (data) {

                        var arrFromJson = JSON.parse(data);

                        console.log(data);
                        console.log(arrFromJson);

                        if (arrFromJson != null && arrFromJson.length > 0) {

                            console.log("has result");

                            //$("#sResult").innerHTML="";

                            $("#sResult").empty();

                            $.each(arrFromJson, function (index, value) {
                                //alert(index + ": " + value.Title);
                                $("#sResult").append(
                                    $('<li>')
                                    .append($('<a>').attr('href', '/' + window.lang + '/services/' + value.Name + '?q=' + q).attr('class', 'service-parent-title').append(value.Title + ' &raquo;'))
                                    .append($('<span>').attr('class', 'price').append(value.Price))
                               );

                                $.each(value.Children, function(idx, val) {

                                    $("#sResult").append(
                                    $('<li>').attr('class', 'service-title')
                                    .append($('<a>').attr('href', '/' + window.lang + '/services/' + val.Name + '?q=' + val.Title).append(val.Title))
                                    .append($('<span>').attr('class', 'price').append(val.Price))
                                    );
                                });


                            });

                            $("#service-search-result").show();

                        } else {
                            console.log("hasn't result");
                            $("#sResult").empty();
                            $("#sResult").append(
                                $('<li>').append("Ничего не найдено :("));

                            $("#service-search-result").show();
                        }

                    }
                });
            }
        } else {
            $("#sResult").empty();
            $("#service-search-result").hide();
        }

    });


    $("#subscribe-btn").click(function() {
        var email = $("#email").val();

        if (IsEmail(email)) {

            $.ajax({
                url: '/subscribe/?email=' + email,
                dataType: "json",
                success: function (data) {

                    $("#subscribe-controls").hide();
                    $("#subscribed").show();
                }
            });
            
        } else {
            $("#email").css("border-color", "red");
        }

    });


    function IsEmail(email) {
        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        return regex.test(email);
    }

});