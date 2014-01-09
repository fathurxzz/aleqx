define(["podManager", "utils"], function (PodManager, utils) {

    var init = function () {
        // Initialize the navigation menu
        initializeNavigation();
	};

    // HELPER METHODS
	
	var standAloneView = {
        text: "Closeout Package",
        cssClass: "NDCloseoutPackageView"
    };

	/********************************************/
    /***************** NAVIGATION ***************/
    /********************************************/
    
    // TO_DO - REFACTOR this long method
    // 		 - Read all the required info from the XML file
    var initializeNavigation = function () {
		
        /**************************************/
        /****** NAVIGATION CLICK EVENTS *******/
        /**************************************/

        $("#menu").find("[data-module='Milestone Tracker']").click(function (e) {
            extractProjectFromClickedElement(e.currentTarget);
            destroyCurrentView(function () {
                PodManager.createMilestoneTrackerView();
                $(window).trigger("resize");
            });
        })

        $("#menu").find("[data-module='Closeout Package']").click(function(e){
            extractProjectFromClickedElement(e.currentTarget);
            destroyCurrentView(function () {
                require(["closeoutPackageView"], function (CloseoutPackageView) {
                    CloseoutPackageView.createCloseoutPackageView();
                });
            });
        })
    }
    
    
    var destroyCurrentView = function(destroyHandler)
    {
    	if($("#spreadsheetView").doesExist())
    	{
    		// Clean the Milestone Tracker view (if exists)
    		PodManager.destroy();
    		destroyHandler();
		}
    	else if($("#CPVGrid").doesExist())
    	{
    		// Clean the closeout package view (if exists)
    		require(["closeoutPackageView"], function(CloseoutPackageView){
    			CloseoutPackageView.destroy();
    			destroyHandler();
    		});
    	}
    	else
    	{
    		destroyHandler();
    	}
    }
    
    var extractProjectFromClickedElement = function(clickedElement)
    {
        var trackerProject = $(clickedElement).data('project');
        if(trackerProject == null )
        {
            alert("Error: Unrecognized Project Id. The Milestone Tracker could not be initialized.");
            return;
        }
        projectId = trackerProject;
    }    
    
    return {
    
        init: init
    
    };
});