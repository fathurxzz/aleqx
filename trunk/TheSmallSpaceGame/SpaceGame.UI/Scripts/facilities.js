var FacilityController = function () {
    this._initialize();
};


FacilityController.prototype = {

    _timeOnUpdatingPage: 0,
    _timeToFinish:0,

    _initialize: function () {
        var obj = $("#remainingTimeSeconds");
        if (obj.val() != undefined) {
            _timeToFinish = obj.val();
            _timeOnUpdatingPage = Date.now();
            this._calculateUpdateTime();
            setInterval(this._calculateUpdateTime, 1000);
        }
    },
    
    _calculateUpdateTime: function () {
        
        var currentTime = Date.now();
        
        var result = Math.floor((_timeToFinish - (currentTime - _timeOnUpdatingPage)) / 1000);
        
        if (result >= 0) {
           
            var x = result;
            var seconds = Math.floor(x % 60);
            x /= 60;
            var minutes = Math.floor(x % 60);
            x /= 60;
            var hours = Math.floor(x % 24);
            x /= 24;
            var days = Math.floor(x);

            result = "";
            if (days != 0)
                result += days + "d ";

            if (hours != 0)
                result += hours + "h ";
            if (minutes != 0) {
                result += minutes + "m ";
            }
            if (seconds != null) {
                result += seconds + "s";
            }
            //result = days + "d " + hours + "h " + minutes + "m " + seconds + "s";

            document.getElementById("remainingTime").innerHTML = result;
            
        } else {
            window.location.reload();
        }
    },

   
    
    _convertToHumanReadableFormat: function (milliseconds) {
        var x = milliseconds;
        var seconds = Math.floor(x % 60);
        x /= 60;
        var minutes = Math.floor(x % 60);
        x /= 60;
        var hours = Math.floor(x % 24);
        x /= 24;
        var days = Math.floor(x);
        return days + "d " + hours + "h " + minutes + "m " + seconds + "s";
    }
};

var FacilityControllerInstance = null;

$(function () {
    FacilityControllerInstance = new FacilityController();
});