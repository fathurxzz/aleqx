$(function () {

    //alert("aaa");

    $(".image").first().css("display", "block");


    $("#em-link").click(function () { location.href = "http://eugene-miller.com"; });
    if (!window.isHomePage) {
        $("#logo").css("cursor", "pointer").click(function() { location.href = "/"; });
    }

    $(".imageLink").click(function () {


        $(".image").each(function () {
            $(this).css("display", "none");
        });

        var groupId= ".group" + $(this).attr("id");
        var objects = $(groupId);

        //for (var i = 0; i < objects.length; i++) {
            //$(objects[i]).css("display", "none");
        //}

        var max = objects.length-1;
        var min = 0;
        var rnd = Math.floor(Math.floor(Math.random() * (max - min + 1)) + min);

        //alert(rnd);
        $(objects[rnd]).css("display", "block");

    });

});

