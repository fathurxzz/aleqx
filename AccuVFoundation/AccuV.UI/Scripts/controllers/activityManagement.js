var ActivityManagement = function () {
    this.initialize();
    ko.applyBindings(this.ViewModel);
};

ActivityManagement.prototype = {
    ViewModel: {
        Activities: ko.observableArray(),
        Activity: ko.observable(),
        NewActivity: ko.observable(),
        Modules: ko.observableArray(),
        ActivityTypes: ko.observableArray()
    },

    initialize: function () {
    }
};

var ActivityManagementController = null;

$(function () {
    ActivityManagementController = new ActivityManagement();
});