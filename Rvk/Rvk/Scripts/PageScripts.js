var BasePageExtender = {
    initialize: function BasePageExtender_initialize() {

        $(function () {
            $("#subscr").focus(function () {
                $("#subscr").val("");
            });





                        setTimeout(function() {
                            $("#banner-top").show("blind");
                            setTimeout(function() {
                                $("#banner-top01").css("visibility", "visible");
                                setTimeout(function() {
                                    $("#banner-top1").css("visibility", "visible");
                                    setTimeout(function() {
                                        $("#banner-top11").css("visibility", "visible");
                                        setTimeout(function() {
                                            $("#banner-top2").css("visibility", "visible");
                                            setTimeout(function() {
                                                $("#banner-top3").css("visibility", "visible");
                                                setTimeout(function () {

                                                    $("#banner-bottom").css("visibility", "visible");
                                                    $("#banner-bottom").show("clip");
                                                    $("#max").css("display", "block");
                                                    
                                                }, 1000);
                                            }, 1000);
                                        }, 1000);
                                    }, 1000);
                                }, 1000);
                            }, 1000);
                        }, 1000);



            //$("#banner-bottom").css("visibility", "visible");
            //$("#banner-bottom").animate({ "top": "-=275px" }, 1000);
            //$("#max").css("display", "block");
            //$("#banner-bottom").show("clip");

            

        });
    }
};
