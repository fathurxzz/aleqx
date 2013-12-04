var AuthController = function () {
    this._initialize();
};

AuthController.prototype = {
    
    _initialize: function () {
        //alert("aaa");

        $("#Email").focus();

        setInterval(this._displayRemainingTime(), 1000);
        //alert("aaa");

        //var today = new Date();
        //today.getDate();


        //alert("now:"+ today + "\r fin : " + finishDate);

        //var span = finishDate - today;

        //$("#remainingTime").html(today);

        //alert(span/1000/60);

        //setInterval(this._displayRemainingTime(), 1000);

        //setInterval(this._aaa(), 1000);


        //$("#inputCardNumberForm .button.ok").click(function () {
        //    $("#inputCardNumberForm").submit();
        //});

        //$("#inputCardPinForm .button.ok").click(function () {
        //    $("#inputCardPinForm").submit();
        //});

        //$("#inputAmountForm .button.ok").click(function () {
        //    $("#inputAmountForm").submit();
        //});


        //$(".enter-card-number .digits .button").click(this._onDigitClick.bind(this));


        //$(".enter-card-number .button.clear").click(function () {
        //    $("#cardNumber").val("");
        //});

        //$(".enter-card-pin .button.clear").click(function () {
        //    $("#Pin").val("");
        //});

        //$(".enter-amount .button.clear").click(function () {
        //    $("#Amount").val("");
        //});


        //$(".enter-amount  .digits .button").click(function () {
        //    var amount = $("#Amount").val();
        //    var value = this.innerHTML.trim();
        //    var result = amount + value;
        //    $("#Amount").val(result);
        //});
        


        //$(".enter-card-pin .digits .button").click(function () {

        //    var card = $("#Pin").val();
        //    if (card.length >= 4) {
        //        return;
        //    }
        //    var value = this.innerHTML.trim();
        //    var result = card + value;
        //    $("#Pin").val(result);
        //});
    },
    
    _aaa: function () {
        alert(Date.now());
    },

    _displayRemainingTime: function () {
        
        //var finishDate = new Date();
        //var finishDateValue = $("#updateFinish").val();
        //finishDate.setTime(Date.parse(finishDateValue));
        //var today = Date.now();
        //var span = finishDate - today;
        

        //$("#remainingTime").html(span);

        alert("aaa");

        //setInterval(_displayRemainingTime(), 1000);

    },

    _onDigitClick: function(evt) {
        //var card = $("#cardNumber").val();

        //if (card.length >= 19) {
        //    return;
        //}

        //var value = evt.currentTarget.innerHTML.trim();
        //var result = this._getFormattedString(card, value);
        //$("#cardNumber").val(result);
    },

    _getFormattedString: function (card, value) {
        //var v = (card + value).replace(/[^\d]/g, '').match(/.{1,4}/g);
        //return v ? v.join('-') : '';
    }
};


var AuthControllerInstance = null;

$(function () {
    AuthControllerInstance = new AuthController();
});