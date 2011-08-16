var AccountsViewer = {
    initialize: function () {
        $(function () {
            AccountsViewer.fetchAccounts();
            $("#drOffice").change(function () {
                AccountsViewer.fetchAccounts();
            });
        });
    },

    fetchAccounts: function () {
        var element = document.getElementById("drOffice");
        var id = element.options[element.selectedIndex].value;
        $.get("/Accounts/GetAccountsByOffice/" + id, function (data) {
            $("#acccountsContent").html(data);
        });
    }
};


var AccountsSelector = {
    initialize: function () {
        $(function () {
            AccountsSelector.fetchAccounts();
            
            $("#drCurrency").change(function () {
                AccountsSelector.fetchAccounts();
            });

            $("#drOper").change(function () {
                AccountsSelector.fetchAccounts();
            });

            $("#drOperSign").change(function () {
                AccountsSelector.fetchAccounts();
            });
        });
    },

    fetchAccounts: function () {

        var officeId = document.getElementById("hOfficeId").value;

        var elementCur = document.getElementById("drCurrency");
        var curId = elementCur.options[elementCur.selectedIndex].value;

        var elementOperSign = document.getElementById("drOperSign");
        var operSign = elementOperSign.options[elementOperSign.selectedIndex].value;

        var elementOperId = document.getElementById("drOper");
        var operId = elementOperId.options[elementOperId.selectedIndex].value;

        $.get("/Accounts/GetAccounts/" + officeId + "/" + operId + "/" + curId + "/" + operSign, function (data) {
            $("#drNls").html(data);
        });
    }
}