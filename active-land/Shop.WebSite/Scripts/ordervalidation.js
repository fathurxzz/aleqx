var Order =
{
    validateFields: function () {

        var validationSuccess = true;

        if ($("#customerName").val() == "") {
            $("#customerName").addClass("validation-error");
            validationSuccess = false;
        } else {
            $("#customerName").removeClass("validation-error");
        }

        if ($("#mobilePhone").val() == "") {
            $("#mobilePhone").addClass("validation-error");
            validationSuccess = false;
        } else {
            $("#mobilePhone").removeClass("validation-error");
        }

        if ($("#email").val() == "") {
            $("#email").addClass("validation-error");
            validationSuccess = false;
        } else {
            $("#email").removeClass("validation-error");

        }

        var delivery = $("input[type='radio'][name='delivery-method']:checked");

        if (delivery.length == 0) {
            $("#delivery-method-list").addClass("validation-error");
            validationSuccess = false;
        }

        var payment = $("input[type='radio'][name='payment-method']:checked");
        if (payment.length == 0) {
            $("#payment-method-list").addClass("validation-error");
            validationSuccess = false;
        }
        
        if ($("#city").val() == "") {
            $("#city").addClass("validation-error");
            validationSuccess = false;
        } else {
            $("#city").removeClass("validation-error");
        }
          
        if ($("#street").val() == "") {
            $("#street").addClass("validation-error");
            validationSuccess = false;
        } else {
            $("#street").removeClass("validation-error");
        }
          
        if ($("#office").val() == "") {
            $("#office").addClass("validation-error");
            validationSuccess = false;
        } else {
            $("#office").removeClass("validation-error");
        }
        

        return validationSuccess;
    }
}