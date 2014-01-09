require.config({ 
    paths: {
        //jQuery: "libs/jquery",
        jQueryFileDownload: "libs/jquery.fileDownload",
        //kendo: "libs/kendo/kendo.all.min",
        plupload: "libs/plupload/plupload.full",
        jQueryProgress: "libs/plupload/jquery-progressbar.min",
        podManager: "app/view/MilestoneTracker/PodManager",        
        commentManager: "app/view/MilestoneTracker/CommentManager",
        filterHelper: "app/view/MilestoneTracker/FilterHelper",
        docManager: "app/view/MilestoneTracker/DocManagerBox",
        docManagerReq: "app/view/MilestoneTracker/DocManagerReq",
        docManagerGlobals: "app/view/MilestoneTracker/DocManagerGlobals",
        
        GCManagement: "app/view/GeneralContractor/GCManagement",
        GCSiteAssignment: "app/view/GeneralContractor/GCSiteAssignment",
        GCGlobals: "app/view/GeneralContractor/GCGlobals",
        
        closeoutPackageView: "app/view/CloseoutPackageView/CloseoutPackageView",
        closeoutPackageViewGlobals: "app/view/CloseoutPackageView/CloseoutPackageViewGlobals",
        
        utils: "app/utilities/GlobalUtils"
    },
    shim: {
        

    },
    waitSeconds: 60
});


var NDPATT = "NDPATT";
var SRT = "SRT";
var projectId = '';

// Expose the app module to the global scope so Kendo can access it.
var app;

var userGroups = [];
// This member will be replaced with the user Id of the Manager
// in case the user is an alias for a manager (set in the PERMISSIONS_ALIAS table)
var userId = '';
// Since the user can be a manager alias
// we will always remember in this member the actual logged in user ID
var loggedInUserId = '';
// This is always the logged in user name
var userName = '';

// The user level is used in the Milestone Tracker and can have one of the following values:
// - admin - the user is administrator and have access to all the featured of the app
// - market - the user is a market manager
// - construction - the user is a construction manager
// - generalContractor - the user is a general contractor and has limited menu options
var userLevel = '';
var USER_LEVEL_ADMIN = "admin";
var USER_LEVEL_MARKET_MANAGER = "marketManager";
var USER_LEVEL_CONSTRUCTION_MANAGER = "constructionManager";
var USER_LEVEL_GENERAL_CONTRACTOR = "generalContractor";

require(["app/app"], function (application) {
    
    jQuery.fn.doesExist = function () {
        return jQuery(this).length > 0;
    };

    // LOADING INDICATOR EXTENSION

    jQuery.fn.showIndicator = function () {
        jQuery(this).hideIndicator();
        jQuery(this).append($("<div id='customLoadingIndicator' class='k-loading-mask' style='position:absolute;left:0px;right:0px;top:0px;bottom:0px;z-index:100000'><span class='k-loading-text'>Loading...</span><div class='k-loading-image'/><div class='k-loading-color'/></div>"));
    };

    jQuery.fn.hideIndicator = function () {
        jQuery(this).find("#customLoadingIndicator").remove();
    };
    
    // JAVASCRIPT STRING FORMAT EXTENSION
    
    // First, checks if it isn't implemented yet.
	if (!String.prototype.format) {
	  String.prototype.format = function() {
	    var args = arguments;
	    return this.replace(/{(\d+)}/g, function(match, number) { 
	      return typeof args[number] != 'undefined'
	        ? args[number]
	        : match
	      ;
	    });
	  };
	}
        
    // RESIZE
    // resize the content when the browser is resizing
    $(window).resize(function () {
        var wHeight = $(window).height();
        var wWidth = $(window).width();

        var hHeight = $("#menu").height();

        var podsAreaHeigt = wHeight - hHeight - 75;

		// Resize the Milestone Tracker in case it exists
        if ($("#spreadsheetView").doesExist()) {
            $("#spreadsheetView").height(podsAreaHeigt - $("#podsArea_selector").height());
            $("#spreadsheetView").width(wWidth);
            var grid = $("#spreadsheetView").data("kendoGrid");
            if (grid != null)
                grid.refresh();
        }
        
        if ($(".report").doesExist) {
            $(".report").height(podsAreaHeigt);
        }
    });

    
    
    /* IMPORTANT: We should delegate all the SharePoint related data to the SPUtil frame
                  That is because if we include both MicrosoftAjax.cs and kendo grid libraries in the same document
                  some conflicts will be occurring due to global obtrusive code */
    // SHAREPOINT Data
    //$("iframe#SPutil").ready(function(){
	//    $(document.body).showIndicator();
	//    document.getElementById('SPUtil').contentWindow.GetSPData(
	//        function (SPData) {
	//            app = application;
	//            initializeApplicationInternal(SPData);
	//        },
	//        function (message) {
	//            alert(message);
	//        }
	//    );
    //});
    
    var initializeApplicationInternal = function(SPData)
    {
    	userName = SPData.userName;
        userId = SPData.userId;
        loggedInUserId = SPData.userId;
        userGroups = SPData.userGroups;
		
        if (allowed(GROUP_ADMINISTRATORS) || allowed(GROUP_DATA)) {
            userLevel = USER_LEVEL_ADMIN;
            
            // APPLICATION INIT
	        app.init();
	        $(document.body).hideIndicator();
        }
        else
        {
			MainAjax("CloseoutPackage","GetUserLevel",userId,function(e){
				
				switch(e.Code)
				{
					case "CP-0000":
						userLevel = e.Result;
						// APPLICATION INIT
				        app.init();
				        $(document.body).hideIndicator();
						break;
					default:
						alert(e.Description);
						break;
				}
			});
        }
    }
    
    var MainAjax = function (controllerName ,methodName, params, successHandler) {
        $(document.body).showIndicator();
        var dataParams = (params != null) ? JSON.stringify(params) : " ";
        $.ajax({
            url: "{0}/{1}/{2}".format(API_URI, controllerName, methodName),
            type: "POST",
            dataType: "json",
            contentType: "application/json",
            cache: true,
            data: dataParams,
            success: function(e){
            	successHandler(e);
            	$(document.body).hideIndicator();
            },
            error: function(e){
            	alert("An internal server error occured while loading the information. [{0}] - {1}".format(controllerName, methodName));
            	$(document.body).hideIndicator();
            }
        });
    };

    
    //userName = SPData.userName;
    //userId = SPData.userId;
    //loggedInUserId = SPData.userId;
    //userGroups = SPData.userGroups;
    var SPData = new Object();
    SPData.userName = 'nicolas.khoury';
    SPData.userId = 'nicolas.khoury';
    SPData.userGroups = 'NDPAdministrators';
    app = application;
    initializeApplicationInternal(SPData);
});

