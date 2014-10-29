var FeedbackForm =
{

  


    validateFields: function () {

        var validationSuccess = true;
        var hasEmail = false;
        var hasPhone = false;

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
            hasPhone = true;
        }

        if ($("#question").val() == "") {
            $("#question").addClass("validation-error");
            validationSuccess = false;
        } else {
            $("#question").removeClass("validation-error");
        }

        var email = $("#email").val();
        if (email != '') {
            var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (!regex.test(email)) {
                $("#email").addClass("validation-error");
                validationSuccess = false;
            } else {
                $("#email").removeClass("validation-error");
                hasEmail = true;
            }
        } else {
            $("#email").removeClass("validation-error");
        }


        if (!hasEmail && !hasPhone) {
            validationSuccess = false;
            $("#mobilePhone").addClass("validation-error");
            $("#email").addClass("validation-error");
        }

        return validationSuccess;
    }
}