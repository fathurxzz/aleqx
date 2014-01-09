var UserManagement = function () {
    this.initialize();
    ko.applyBindings(this.ViewModel);
};


UserManagement.prototype =
{
    _activities: null,
    _selectedUser: null,

    ViewModel: {
        DomainUser: ko.observable(),
        User: ko.observable(),
        AvailableActivities: ko.observableArray(),
        UserActivities: ko.observableArray(),
        NewUser: ko.observable(),

        assign: function (item, index) {
            var activity = {
                ActivityId: item.ActivityId,
                ActivityName: item.ActivityName,
                ActivityTypeId: item.ActivityTypeId,
                TypeName: item.TypeName,
                AccessAdmin: false,
                AccessApprove: false,
                AccessCreate: false,
                AccessDelete: false,
                AccessRead: false,
                AccessUpdate: false
            };
            this.UserActivities.push(activity);
            this.UserActivities.sort(function (left, right) {
                return left.ActivityName == right.ActivityName ? 0 : (left.ActivityName < right.ActivityName ? -1 : 1);
            });

            this.AvailableActivities.splice(index(), 1);
        },
        unassign: function (item, index) {
            var activity = {
                ActivityId: item.ActivityId,
                ActivityName: item.ActivityName,
                ActivityTypeId: item.ActivityTypeId,
                TypeName: item.TypeName,
            };
            this.AvailableActivities.push(activity);
            this.AvailableActivities.sort(function (left, right) {
                return left.ActivityName == right.ActivityName ? 0 : (left.ActivityName < right.ActivityName ? -1 : 1);
            });
            this.UserActivities.splice(index(), 1);
        },

        createUser: function (item) {
            var user = ko.mapping.toJS(item);
            $.ajax({
                url: Config.apiLocation + "Users",
                context: UserManagementController,
                type: "POST",
                data: JSON.stringify(user),
                success: UserManagementController.onUserCreated
            });
        },
        
        updateUser: function (item) {
            var user = ko.mapping.toJS(item);
            $.ajax({
                url: Config.apiLocation + "Users",
                type: "PUT",
                context: UserManagementController,
                data: JSON.stringify(user),
                success: UserManagementController.onUserUpdated,
            });
        },

        updateUserActivities: function () {
            var requestData =
            {
                UserId: this.User().Id,
                UserActivities: ko.mapping.toJS(this.UserActivities())
            };
            $.ajax({
                context: this,
                url: Config.apiLocation + "Users/Activities",
                type: "POST",
                data: JSON.stringify(requestData),
            });
        }
    },

    initialize: function () {
        $.ajax({
            url: Config.apiLocation + "Activities",
            type: "GET",
            success: this.onActivitiesReceived.bind(this)
        });

    },

    onUserCreated: function (data) {
        this.ViewModel.NewUser(null);
        this.ViewModel.User(ko.mapping.fromJS(data));
    },
    
    onUserUpdated: function() {
        this._selectedUser = ko.mapping.toJS(this.ViewModel.User());
    },

    onActivitiesReceived: function (data) {
        this._activities = data;
    },

    adUserSelected: function (e) {
        this._selectedUser = e.sender.dataItem(e.item.index());
        this.ViewModel.DomainUser(this._selectedUser);

        $.ajax({
            url: Config.apiLocation + "Users/?id=" + escape(this._selectedUser.UserName),
            type: "GET",
            context: this,
            success: this.onRealUserReceived
        });
    },

    onRealUserReceived: function (data) {
        if (data) {
            this.ViewModel.User(data.User);
            this.ViewModel.NewUser(null);
            this.ViewModel.UserActivities(ko.mapping.fromJS(data.UserActivities)());
            this._updateAvailableActivities(data.UserActivities);
            this.ViewModel.NewUser(null);
        } else {
            if (this.ViewModel.UserActivities) {
                this.ViewModel.UserActivities([]);
                this.ViewModel.AvailableActivities([]);
            }
            this.prepareNewUser();
        }

    },

    prepareNewUser: function () {
        var newUser = {
            Id: this._selectedUser.UserName,
            UserName: this._selectedUser.DisplayName,
            Email: this._selectedUser.Email
        };
        newUser.Company = "";
        this.ViewModel.NewUser(ko.mapping.fromJS(newUser));
    },

    _updateAvailableActivities: function (userActivities) {
        if (userActivities) {
            var availableActivities = $.grep(this._activities, function (a) {
                var userActivity = $.grep(userActivities, function (ua) {
                    return ua.ActivityId == a.ActivityId && ua.ActivityTypeId == a.ActivityTypeId;
                });
                return userActivity.length == 0;
            });
            this.ViewModel.AvailableActivities(ko.mapping.fromJS(availableActivities)());
        }
    },
};

var UserManagementController = null;

$(function () {
    UserManagementController = new UserManagement();
});
