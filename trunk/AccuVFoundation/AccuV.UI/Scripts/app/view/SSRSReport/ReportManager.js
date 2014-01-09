define(["jQuery","kendo","podManager"], function ($, kendo, podManager) {

    var $DOM = function (url) {
        var reportDOM = $("<div></div>");
        reportDOM.append("<iframe src='" + url + "' style='width:100%;height:100%;border:0px;'></iframe>");
        reportDOM.addClass("report");
        return reportDOM;
    };

    return {
        createReportingView: function (url) {
            // wipe the content area
            var podsArea = $("#podsArea");
            podsArea.empty();

            // add the completed tasks Pod
            var $spreadsheetPod = $DOM(url);
            $spreadsheetPod.attr("id", "spreadsheetPod");
            $spreadsheetPod.css("width", "auto");
            $("#podsArea").append($spreadsheetPod);
        }
    }

});