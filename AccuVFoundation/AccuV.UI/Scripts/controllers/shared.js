$.ajaxSetup({
    contentType: "application/json",
    accept: "application/json",
    error: function(jqXhr, textStatus, error) {
        alert(error);
    }
});

$(document)
    .ajaxStart($.blockUI)
    .ajaxStop($.unblockUI);

if ($.blockUI) {
    $.blockUI.defaults.message = " ";
    $.blockUI.defaults.bindEvents = false;
    $.blockUI.defaults.css = {
        padding: 0,
        margin: 0,
        width: '30%',
        top: '40%',
        left: '35%',
        textAlign: 'center',
        color: '#000',
        border: '0',
        cursor: 'wait'
    };
}