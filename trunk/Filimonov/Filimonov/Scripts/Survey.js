var SurveyService = {
    hiddenControls: true,
    obj1: null,
    obj2: null,

    initialize: function () {

        hiddenControls = true;

        $(function () {
            obj1 = $("#inputNumber");
            obj2 = $("#txtQuestion");


            $("#newLine").click(function (e) {
                if (hiddenControls) {
                    SurveyService.showControls();
                }

                e.stopPropagation();
                if (window.event) {
                    window.event.cancelBubbling = true;
                }
            });



            $("body").click(function () {
                if (!hiddenControls) {
                    SurveyService.hideControls();
                    SurveyService.saveChanges(obj1.val(), obj2.val());
                }
            });


        });
    },

    hideControls: function () {
        hiddenControls = true;
        obj1.addClass("hidden");
        obj2.addClass("hidden");
    },

    showControls: function () {
        hiddenControls = false;
        obj1.val("");
        obj1.removeClass("hidden");
        obj2.val("");
        obj2.removeClass("hidden");
    },

    saveChanges: function (number, question) {

        $.post("/presentation/survey/CreateSurveyItem?number=" + number + "&question=" + question, function (data) {
            //ProjectCatalogue._updateImageContainer(fileName);
            alert(data);
        });

        //alert(number);
        //alert(question);
        alert("saved!");



    }
};