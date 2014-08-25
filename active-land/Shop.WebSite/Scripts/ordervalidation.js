var Order =
{

    deliveryMethod:"",

    checkRadios: function (obj) {
        this.deliveryMethod = obj;
        if (obj === "takeout") {
            $(".delivery-field").css("display", "none");
        } else {
            $(".delivery-field").css("display", "block");
        }
    },


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


        if ($("#subscribe-radio").attr('checked')) {
            var email = $("#email").val();
            var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (!regex.test(email)) {
                $("#email").addClass("validation-error");
                validationSuccess = false;
            } else {
                $("#email").removeClass("validation-error");
            }
        } else {
            $("#email").removeClass("validation-error");
        }

        //if ($("#email").val() == "") {
        //    if (Order.validateEmail) {
        //        console.log("email not valid");
        //        $("#email").addClass("validation-error");
        //        validationSuccess = false;
        //    }
        //} else {
        //    $("#email").removeClass("validation-error");

        //}

        var delivery = $("input[type='radio'][name='delivery-method']:checked");

        if (delivery.length == 0) {
            $("#delivery-method-list").addClass("validation-error");
            validationSuccess = false;
        } else {
            $("#delivery-method-list").removeClass("validation-error");
        }

        var payment = $("input[type='radio'][name='payment-method']:checked");
        if (payment.length == 0) {
            $("#payment-method-list").addClass("validation-error");
            validationSuccess = false;
        } else {
            $("#payment-method-list").removeClass("validation-error");
        }

        if (this.deliveryMethod === "delivery") {

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
        }

        return validationSuccess;
    }
}