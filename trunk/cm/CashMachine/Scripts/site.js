var AuthController = function () {
    this._initialize();
};

AuthController.prototype = {
    

    onCardNumberError: function(error) {
        if (error.status == 403) {
            location.replace(Config.baseUrl + "Error/InvalidPin");
        }
    },
    
    onSuccess: function() {
        location.replace(Config.baseUrl);
    },

    _initialize: function () {
        $(".button.ok").click(function () {
            $("#inputCardNumberForm").submit();
        });


        $(".enter-card-number .digits .button").click(this._onDigitClick.bind(this));

        $(".enter-card-number .button.clear").click(function () {
            $("#cardNumber").val("");
        });

        $(".enter-card-pin .button.clear").click(function () {
            $("#Pin").val("");
        });


        $(".enter-card-pin .digits .button").click(function () {

            var card = $("#Pin").val();
            if (card.length >= 4) {
                return;
            }
            var value = this.innerHTML.trim();
            var result = card + value;
            $("#Pin").val(result);
        });
    },
    
    _onDigitClick: function(evt) {
        var card = $("#cardNumber").val();

        if (card.length >= 19) {
            return;
        }

        var value = evt.currentTarget.innerHTML.trim();
        var result = this._getFormattedString(card, value);
        $("#cardNumber").val(result);
    },

    _getFormattedString: function (card, value) {
        var v = (card + value).replace(/[^\d]/g, '').match(/.{1,4}/g);
        return v ? v.join('-') : '';
    }
};

var AuthControllerInstance = null;

$(function () {
    AuthControllerInstance = new AuthController();
});