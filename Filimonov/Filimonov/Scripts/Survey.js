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
                    if ((obj1.val() == "" && obj2.val() == "") || (obj1.val() != "" && obj2.val() != "")) {
                        SurveyService.hideControls();
                        SurveyService.saveChanges();
                    } else {
                        if (obj1.val() == "")
                            obj1.addClass("error");
                        else {
                            obj1.removeClass("error");
                        }
                        if (obj2.val() == "")
                            obj2.addClass("error");
                        else {
                            obj2.removeClass("error");
                        }
                    }
                }
            });


            $("#sendAnswer").click(function () {
                $.post("/presentation/survey/SaveAnswer", $("#surveyForm").serialize(), function (result) {
                    if (result == "True") {
                        alert("Данные сохранены");
                    } else {
                        alert("Ошибка при сохранении данных");
                    }
                });
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
        obj1.removeClass("error");
        obj2.removeClass("error");
        obj1.val("");
        obj1.removeClass("hidden");
        obj2.val("");
        obj2.removeClass("hidden");
    },

    saveChanges: function () {
        $.post("/presentation/survey/SaveSurvey", $("#surveyForm").serialize(), function (result) {
            if (result != null) {
                $('#surveyTable tr:last').before(result);
            }
        });
    }
};