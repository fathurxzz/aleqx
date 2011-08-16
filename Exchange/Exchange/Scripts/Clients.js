var ClientsSelector = {
    initialize: function () {
        $(function () {
            $("#clientSearchButton").click(function () {
                ClientsSelector.fetchClients();
            });
        });
    },

    fetchClients: function () {
        var podrid = document.getElementById("podrid").value;
        var okpo = document.getElementById("tOkpo").value;
        if (okpo == "")
            okpo = 0;
        var cntrCode = document.getElementById("tCntrCode").value;
        if (cntrCode == "")
            cntrCode = 0;

        if (cntrCode == 0 && okpo == 0) {
            alert("введіть параметри пошуку");
            return;
        }

        $("#notificationContainer").html("searching...");
        $("#clientSearchButton").css("display", "none");

        $.get("/Tenders/GetClients/" + podrid + "/" + okpo + "/" + cntrCode, function (data) {
            $("#drClientNames").html(data);

            ClientsSelector.selectClient();

            $("#drClientNames").change(function () {
                ClientsSelector.selectClient();
            });
        });
    },

    selectClient: function () {
        var element = document.getElementById("drClientNames");
        if (element.options[0] != null) {
            var subjId = element.options[element.selectedIndex].subjId;
            var cntrCode = element.options[element.selectedIndex].cntrCode;
            var clientName = element.options[element.selectedIndex].value;
            var okpo = element.options[element.selectedIndex].okpo;

            $("#sOkpo").html(okpo);
            $("#okpo").val(okpo);

            $("#sCntrCode").html(cntrCode);
            $("#cntrCode").val(cntrCode);

            $("#sClientName").html(clientName);
            $("#clientName").val(clientName);

            $("#subjId").val(subjId);
        } else {


            $("#sOkpo").html("");
            $("#okpo").val("");

            $("#cntrCode").val("");
            $("#sCntrCode").html("");

            $("#sClientName").html("");
            $("#clientName").val("");

            $("#subjId").val("");

            alert("client not found");
        }

        $("#notificationContainer").html("");
        $("#clientSearchButton").css("display", "block");

    }
}