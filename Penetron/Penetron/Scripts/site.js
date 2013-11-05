$(function () {

    //alert("aaa");

    $("#em-link").click(function () { location.href = "http://eugene-miller.com"; });
    if (!window.isHomePage) {
        $("#logo").css("cursor", "pointer").click(function() { location.href = "/"; });
    }

});

