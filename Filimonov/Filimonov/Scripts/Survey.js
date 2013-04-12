var SurveyService = {
    hiddenControls: true,
    updateQuestion: false,
    obj1: null,
    obj2: null,
    editedRow: null,


    initialize: function () {

        hiddenControls = true;
        updateQuestion = false;

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


            $("td.admin").click(function (e) {
                e.stopPropagation();
                if (window.event) {
                    window.event.cancelBubbling = true;
                }
            });


            $(".quest").click(function (e) {

                if ($("#isAdmin").val() == "1") {
                    $(this).find(".taq").removeClass("hidden");
                    $(this).find(".tbn").removeClass("hidden");
                    $(this).find(".questText").html("");
                    $(this).find(".numberText").html("");
                    editedRow = $(this);

                    updateQuestion = true;

                    e.stopPropagation();
                    if (window.event) {
                        window.event.cancelBubbling = true;
                    }
                }

                //alert(id);
            });


            $("body").click(function () {

                if (updateQuestion) {


                    var obj = editedRow.find(".taq");


                    var objn = editedRow.find(".tbn");
                    var objtext = editedRow.find(".questText");
                    var objNumber = editedRow.find(".numberText");
                    var qId = obj.attr("id").split("_")[1];

                    //                    alert(obj.html());

                    //                    alert(obj.html());

                    SurveyService.updateChanges(obj, qId, objtext, objn, objNumber);
                    updateQuestion = false;
                }


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
                $.post("/platform/survey/SaveAnswer", $("#surveyForm").serialize(), function (result) {
                    if (result == "True") {
                        alert("Данные сохранены");
                    } else {
                        alert("Ошибка при сохранении данных");
                    }
                });
            });


            //$('#aside').prepend('<a class="print-preview">Отправить на печать / сгенерировать в pdf</a>');
            $('#aside').html('<a class="print-preview">Отправить на печать / сгенерировать в pdf</a>');
            $('a.print-preview').printPreview();

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
        $.post("/platform/survey/SaveSurvey", $("#surveyForm").serialize(), function (result) {
            if (result != null) {
                $('#surveyTable tr:last').before(result);
                SurveyService.initialize();
            }
        });


    },

    updateChanges: function (obj, id, objtext, objn, objNumber) {
        $.post("/platform/survey/UpdateSurvey?id=" + id, $("#surveyForm").serialize(), function (result) {
            if (result != null) {
                //$('#surveyTable tr:last').before(result);
                obj.addClass("hidden");
                objn.addClass("hidden");
                //alert(result);
                var number = result.split("###")[0];
                var question = result.split("###")[1];
                objtext.html(question);
                objNumber.html(number);
                //alert(result);
            }
        });
    }
};