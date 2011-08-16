var ExchangeScripts = {
    initialize: function () {
        $(function () {
            $("#checkAll").click(function () {
                ExchangeScripts.selectDeselectAll(this);
            });

            $("#tRecipient").keyup(function () {
                var obj = $("#recipientList");
                ExchangeScripts.getRecepient($(this).val(), obj);
            });
        });
    },

    selectDeselectAll: function (obj) {
        if ($(obj).attr('checked'))
            $("input:checkbox.item").attr("checked", "checked");
        else
            $("input:checkbox.item").removeAttr("checked");

    },

    processQuery: function (url, obj) {
        /*
        $.get("https://mira.proj.pib.com.ua/exchange.mvc/" + url, function(data) {
        $(obj).html(data);
        });
        */

        $.get(url, function (data) {
            $(obj).html(data);
        });
    },

    getRecepient: function (mask, obj) {
        $.get("/Conversations/GetRecipientsByMask/" + mask, function (data) {
            $(obj).html(data);
        });

    }
}