var Validator = {

    initialize: function () {
        $(function () {

            $("#formNewTender").validate({
                rules: {
                    DocSum: { required: true, number: true },
                    Commission: { required: true, number: true },
                    drNls: { required: true, number: true },
                    drCurrency: { required: true },
                    Purpose: {
                        required: function () { return $("#drOper").val() == 1; }
                    }
                },
                messages: {
                    DocSum: { required: "Введіть суму заявки!", number: "Тільки цифри!" },
                    Commission: { required: "Введіть комісію!", number: "Тільки цифри!" },
                    drNls: { required: "Необхідно ввести рахунок", number: "Тільки цифри!" },
                    drCurrency: { required: "Необхідно ввести валюту" },
                    Purpose: { required: "Необхідно ввести мету купівлі" }
                }
            });

            $("#drOper, #drOperSign").change(function () {
                Validator.renderFields();
            });

            Validator.renderFields();
        });
    },

    renderFields: function () {
        var operId = $("#drOper").val();
        if (operId == 1) {
            $("#fSellType").css("display", "none");
            $("#fPurpose").css("display", "block");

        } else {
            $("#fSellType").css("display", "block");
            $("#fPurpose").css("display", "none");
        }

        var operSign = $("#drOperSign").val();
        if (operSign == 0) {
            $("#searchClientPanel").css("display", "none");
        } else {
            $("#searchClientPanel").css("display", "block");
        }
    }

}