define(function () {

    // TO_DO keep filter data in a separate model
    // TO_DO rename the file to FilterManager instead of FilterHelper

    var filtersWrapperId = "";
    var filtersWrapper = $("<table style='width:100%'> \
                                <tr> \
                                    <td id='filterContainerLeft' valign='top'></td> \
                                    <td id='filterContainerMiddle' valign='top'></td> \
                                    <td id='filterContainerRight' valign='top'></td> \
                                    <td id='filterContainerRightMost' valign='top'></td> \
                                </tr> \
                            </table>");

    var _filtersInitializedHandler;
    var _filterChangedHandler;
    var _filtersToInitialize = 0;

    /*******************************************/
    /************* EVENT HANDLERS **************/
    /*******************************************/

    var onFilterChangeInternal = function (e) {
        // read all the values of all the filters and dispatch an event to reload the data for the grid
        var managersMultiSelect = $("#manager").data("kendoMultiSelect");
        var constructionManagersMultiSelect = $("#constructionManager").data("kendoMultiSelect");
        var regionsMultiSelect = $("#region").data("kendoMultiSelect");
        var marketMultiSelect = $("#market").data("kendoMultiSelect");
        var siteMultiSelect = $("#site").data("kendoMultiSelect");
        var taskMultiSelect = $("#task").data("kendoMultiSelect");
        var gcMultiSelect = $("#gc").data("kendoMultiSelect");
        var documentIdMultiSelect = $("#documentId").data("kendoMultiSelect");
        var documentTypeMultiSelect = $("#documentType").data("kendoMultiSelect");
        var notApplicableDropDown = $("#notApplicable").data("kendoDropDownList");

        var filterSet = {
            ProjectId: projectId,
            MarketManager: (managersMultiSelect != null) ? managersMultiSelect.dataItems() : [],
            ConstructionManager: (constructionManagersMultiSelect != null) ? constructionManagersMultiSelect.dataItems() : [],
            Region: (regionsMultiSelect != null) ? regionsMultiSelect.dataItems() : [],
            Market: (marketMultiSelect != null) ? marketMultiSelect.dataItems() : [],
            Site: (siteMultiSelect != null) ? siteMultiSelect.dataItems() : [],
            Task: (taskMultiSelect != null) ? taskMultiSelect.dataItems() : [],
            GeneralContractor: (gcMultiSelect != null) ? gcMultiSelect.dataItems()[0] : "",
            DocumentId: (documentIdMultiSelect != null) ? documentIdMultiSelect.dataItems() : [],
            DocumentType: (documentTypeMultiSelect != null) ? documentTypeMultiSelect.dataItems() : [],
            NotApplicable: (notApplicableDropDown != null) ? notApplicableDropDown.dataItem().value : ""
        };

        $(window).trigger("resize");

        if (_filterChangedHandler != null)
            _filterChangedHandler(e, filterSet);
    };

    /*******************************************/
    /*********** INTERNAL MEMBERS **************/
    /*******************************************/
        
    var createFiltersInternal = function (container, reqFilters) {
    
		var buildMarketManager = true;
		var buildConstructionManager = true;
		var buildRegion = true;
		var buildMarket = true;
		var buildSite = true;
		var buildTask = true;
		
		var buildGeneralContractor = false;
		var buildDocumentId = false;
		var buildDocumentType = false;
		var buildNotApplicable = false;
		
		if(reqFilters != null)
		{
			buildMarketManager = (reqFilters.indexOf("marketManager") >= 0);
			buildConstructionManager = (reqFilters.indexOf("constructionManager") >= 0);
			buildRegion = (reqFilters.indexOf("region") >= 0);
			buildMarket = (reqFilters.indexOf("market") >= 0);
			buildSite = (reqFilters.indexOf("site") >= 0);
			buildTask = (reqFilters.indexOf("task") >= 0);
			buildGeneralContractor = (reqFilters.indexOf("generalContractor") >= 0);
			buildDocumentId = (reqFilters.indexOf("documentId") >= 0);
			buildDocumentType = (reqFilters.indexOf("documentType") >= 0);
			buildNotApplicable = (reqFilters.indexOf("notApplicable") >= 0);
		}

        filtersWrapperId = container.attr("id") + "_selector";
        container.prepend(filtersWrapper);

        filtersWrapper.attr("id", filtersWrapperId);
        
        // Filter MARKET MANAGER
        
        if(buildMarketManager)
        {
	        $("#filterContainerLeft").append("<div id='managerContainer'>Market Manager: <select id='manager'/><br/></div>");
	        $("#manager").css("width", "300px");
	        $("#manager").kendoMultiSelect(
	            {
	                dataSource: getFilterDataSource("GetMarketManagers",
	                    function (options, result) {
	                        // This handler is called when the construction managers response has arrived from server
	                        
	                        if (result.Properties.hasOwnProperty("userAliasManager"))
	                        	userId = result.Properties.userAliasManager;
	
	                        var managerFilter = $("#manager").data("kendoMultiSelect");
	                        managerFilter.enable(result.Properties.enabled);
	
	                        if(userLevel == USER_LEVEL_ADMIN)
	                        {
	                            options.success(result.Data);
	                        }
	                        else if(userLevel == USER_LEVEL_MARKET_MANAGER)
	                        {
	                            options.success(result.Data);
	                            managerFilter.value(result.Data[0]);
	                        }
	                        else if(userLevel == USER_LEVEL_CONSTRUCTION_MANAGER)
	                        {
	                            options.success([]);
	                            $("#managerContainer").css("display", "none");
	                        }
	                    }
	                ),
	                highlightFirst: false,
	                enable: false,
	                change: onFilterChangeInternal
	            }
	        );
        }
        
        // Filter CONSTRUCTION MANAGER
		
		if(buildConstructionManager)
		{
	        $("#filterContainerLeft").append("Construction Manager: <select id='constructionManager'/><br/>&nbsp;");
	        $("#constructionManager").css("width", "300px");
	        $("#constructionManager").kendoMultiSelect(
	            {
	                dataSource: getFilterDataSource("GetConstructionManagers",
	                    function (options, result){
	
	                        var managerFilter = $("#constructionManager").data("kendoMultiSelect");
	
	                        // This handler is called when the construction managers response has arrived from server
	                        if (result.Code == "FILTER-0000") {
	                        
	                        	if (result.Properties.hasOwnProperty("userAliasManager"))
		                        	userId = result.Properties.userAliasManager;
	                        
	                            managerFilter.enable(result.Properties.enabled);
	                            options.success(result.Data);
	
	                            if (userLevel == USER_LEVEL_CONSTRUCTION_MANAGER) {
	                                managerFilter.value(result.Data[0]);
	                            }	
	                        }
	                        else {
	                            // An error has occurred
	                            managerFilter.enable(false);
	                            options.success([]);
	                            alert(result.Description);
	                        }
	                    }
	                ),
	                highlightFirst: false,
	                enable: false,
	                change: onFilterChangeInternal
	            }
	        );
        }
        
        // Filter "NOT APPLICABLE"
        
        if(buildNotApplicable)
        {
        	$("#filterContainerLeft").append("<input id='notApplicable' value='0' /><br/>&nbsp;");
            $("#notApplicable").css("width", "300px");
            $("#notApplicable").kendoDropDownList(
                {
                	dataTextField: "text",
                    dataValueField: "value",
                    dataSource: {
                    	data: [
                    		{text: "Show all", value:""},
                    		{text: "Show only Not Applicable items", value:"1"},
                    		{text: "Show only Applicable items", value:"0"}
                    	]
                    },
                    index: 0,
                    highlightFirst: false,
                    change: onFilterChangeInternal
                }
            );
        }
        
             
         // Filter REGION
        
        if(buildRegion)
        {
            $("#filterContainerMiddle").append("Filter by Region: <select id='region'/><br/>&nbsp;");
            $("#region").css("width", "300px");
            $("#region").kendoMultiSelect(
                {
                    dataSource: getFilterDataSource("GetDistinctRegions"),
                    highlightFirst: false,
                    change: onFilterChangeInternal
                }
            );
        }

        // Filter MARKET
		
		if(buildMarket)
		{
            $("#filterContainerMiddle").append("Filter by Market: <select id='market'/>");
            $("#market").css("width", "300px");
            $("#market").kendoMultiSelect(
                {
                    dataSource: getFilterDataSource("GetDistinctMarkets"),
                    highlightFirst: false,
                    change: onFilterChangeInternal
                }
            );
        }

        // Filter SITE NUMBER
		
		if(buildSite)
		{
            $("#filterContainerRight").append("Filter by Site Number: <select id='site'/><br/>&nbsp;");
            $("#site").css("width", "300px");
            $("#site").kendoMultiSelect(
                {
                    dataSource: getFilterDataSource("GetDistinctSites"),
                    highlightFirst: false,
                    change: onFilterChangeInternal
                }
            );
        }

        // Filter TASK
		
		if(buildTask)
		{
            $("#filterContainerRight").append("Show only the following Tasks: <select id='task'/><br/>&nbsp;");
            $("#task").css("width", "300px");            
            
			// The task filter will have a special behavior in
            // case the user is a GENERAL CONTRACTOR
            // It will need to be disabled and show only task 126  
            if(userLevel == USER_LEVEL_GENERAL_CONTRACTOR && projectId == NDPATT)
            {
            	$("#task").kendoMultiSelect(
	                {
	                    dataSource: ["126"],
	                    highlightFirst: false,
	                    enable:false,
	                    value: "126"
	                }
	            );
            }
            else{
	            $("#task").kendoMultiSelect(
	                {
	                    dataSource: getFilterDataSource("GetDistinctTasks"),
	                    highlightFirst: false,
	                    change: onFilterChangeInternal
	                }
	            );
            }
        }
        
        // Filter GENERAL CONTRACTOR
        // This filter will always be disabled, it will be used only to display the name
        // of the general contractor who has logged in
		if(buildGeneralContractor)
		{
            $("#filterContainerLeft").append("General Contractor: <select id='gc'/><br/>&nbsp;");
            $("#gc").css("width", "300px");
            $("#gc").kendoMultiSelect(
	            {
	                dataSource: {data :[userName] },
	                highlightFirst: false,
	                enable: false
	            }
            );
            var gcFilter = $("#gc").data("kendoMultiSelect");
            gcFilter.value(userName);
        }
        
        // Filter DOCUMENT ID
        
        if(buildDocumentId)
        {
        	$("#filterContainerRightMost").append("Document ID: <select id='documentId'/><br/>&nbsp;");
            $("#documentId").css("width", "300px");
            $("#documentId").kendoMultiSelect(
                {
                    dataSource: getFilterDataSource("GetDistinctDocumentIDs"),
                    highlightFirst: false,
                    change: onFilterChangeInternal
                }
            );

        }
        
        // Filter DOCUMENT TYPE
        
        if(buildDocumentType)
        {
        	$("#filterContainerRightMost").append("Document Type: <select id='documentType'/><br/>&nbsp;");
            $("#documentType").css("width", "300px");
            $("#documentType").kendoMultiSelect(
                {
                    dataSource: getFilterDataSource("GetDistinctDocumentTypes"),
                    highlightFirst: false,
                    change: onFilterChangeInternal
                }
            );

        }

    };
    
    var getFilterDataSource = function (methodName, successHandler, additionalRequestParams) {

        // Calculate the data params that will be sent to the server in the GET request
        var requestParams = {};
        requestParams.userId = userId;
        requestParams.userLevel = userLevel;
        requestParams.projectId = projectId;

        if (additionalRequestParams != null) {
            for (var p in additionalRequestParams) {
                requestParams[p] = additionalRequestParams[p];
            }
        }
		
		_filtersToInitialize++;
        var dataSource = new kendo.data.DataSource({
            transport: {
                read: function (options) {

                    $.ajax({
                        url: API_URI + "/Site/" + methodName,
                        data: requestParams
					}).done(function (result) {							
                            if (successHandler != null) {
                                successHandler(options, result)
                            } else {
                                options.success(result);
                            }
                            
                            _filtersToInitialize--;
                            if(_filtersToInitialize <= 0)
                            {
                            	onFilterChangeInternal();
                            }
					}).fail(function (result) {
                            options.error(result);
                            alert("An error occurred while loading the filter data [" + methodName + "].");
                    });
                }
            }
        });

        return dataSource;
    };

    /*******************************************/
    /************* PUBLIC MEMBERS **************/
    /*******************************************/

    return {

		createFilters: function (container, reqFilters) {
            createFiltersInternal(container, reqFilters);
        },
        
        destroy: function () {
	        if ($("#{0}".format(filtersWrapperId)).doesExist()) {
	            $("#{0} td".format(filtersWrapperId)).empty();
	            $("#{0}".format(filtersWrapperId)).remove();
	        }
        },

        onFiltersReady: function (handler) {
            alert("Not implemented: FiltersHelper.onFiltersReady");
            _filtersInitializedHandler = handler;
        },

        onFilterChange: function (handler) {
            _filterChangedHandler = handler;
        }

    };

});