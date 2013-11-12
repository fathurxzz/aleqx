$(function () {
    $(".enter-card-number .digits .button").click(function () {
        var card = $("#cardNumber").val();
        
        if (card.length >= 19) {
            return;
        }
        
        var value = this.innerHTML.trim();
        var result = getFormattedString(card, value);
        $("#cardNumber").val(result);
    });

    $(".enter-card-number .button.clear").click(function () {
        $("#cardNumber").val("");
    });

    $(".enter-card-pin .button.clear").click(function () {
        $("#cardPin").val("");
    });


    $(".enter-card-pin .digits .button").click(function () {
        
        var card = $("#cardPin").val();
        if (card.length >= 4) {
            return;
        }
        var value = this.innerHTML.trim();
        var result = card + value;
        $("#cardPin").val(result);
    });

});


function getFormattedString(card, value) {
    var v = (card + value).replace(/[^\d]/g, '').match(/.{1,4}/g);
    return v ? v.join('-') : '';
}