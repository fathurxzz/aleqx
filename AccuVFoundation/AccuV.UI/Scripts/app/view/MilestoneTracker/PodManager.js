define(["filterHelper"], function (filter) {


    /*******************************************/
    /*** SPREADSHEET MANAGEMENT - MAIN FLOW ****/
    /*******************************************/

    // TO_DO - log errors and do better exception handling
    // TO_DO - rename/reorganize the methods to better highlight the scope
    // TO_DO - keep the data in a separate model

    var _gridDataSource = null;
    var _gridDataSourceOptions = null;
    var _gridDataSourceSchema = null;
    var _gridColumns = null;
    var _gridData = null; // remember the array of items which populates the grid (the grid data provider)

    // maintain a copy of the modified records
    // this will be sent to the server when the Update button will be clicked
    var _gridModifiedRecords = {};

    // Remember the current filters for the Sites
    // Initially the filter will be empty, 
    // hence all the site information will be visible in the grid
    // This member will be sent as parameter when reading the site information
    var newSiteFilter = { Page:1, MarketManager:[], ConstructionManager:[], Region: [], Market: [], Site:[], Task:[] };
    var currentSiteFilter;





	/***************************************/
	/******* INTERNAL IMPLEMENTATION *******/
	/***************************************/
	
	var createMilestoneTrackerViewInternal = function() {
        
        // remove spreadsheet view elements
        destroyInternal();

        var podsArea = $("#podsArea");

        // Initialize the filters
        filter.createFilters(podsArea);
        filter.onFilterChange(function (e, filterSet) {
            newSiteFilter = filterSet;
            newSiteFilter.Page = 1;

            var grid = $("#spreadsheetView").data("kendoGrid");

            // In case we selected the Task filter, 
            // we should re-create the grid because we need to modify the columns
            // and  Kendo Grid cannot modify columns at runtime
            if (grid == null || e.sender.element.attr('id') == 'task') {
                recreateGridControl();
            } else {
                // for any other filter just refresh the data from the server
                refreshGridData();
            }
        });
        
        // Listen for a global click event
        
        $(window).bind('click',globalClickHandler);
        
    };
    
    
    var createCloseoutPackageViewInternal = function() {
        
        // remove spreadsheet view elements
        destroyInternal();

        var filters;
		if(userLevel == USER_LEVEL_GENERAL_CONTRACTOR)
		{
			filters = ["generalContractor","site"];
		}
		else
		{
			filters = null;
		}
		
		filter.createFilters($("#podsArea"), filters);        
    };





	/***************************************/
	/******* COMMON INTERNAL METHODS *******/
	/***************************************/





    var refreshGridData = function() {

        if (filterChanged()) {
            // request the data again because the page has changed
            $("#podsArea").showIndicator();
            gridDataAjax(function (result) {
                _gridData = result;

                synchFilters();
                _gridDataSource.page(currentSiteFilter.Page);

                _gridDataSourceOptions.success(_gridData);
                $("#podsArea").hideIndicator();
            },
            function (result) {
                $("#podsArea").hideIndicator();
                alert("Error while getting the list of Sites.");
            });

        } else {
            if (_gridData != null)
            {
                _gridDataSourceOptions.success(_gridData);
            }
        }
    };

    var gridDataAjax = function (successHandler, errorHandler) {
        $.ajax({
            url: API_URI + "/Site/Sites",
            type: "POST",
            dataType: "json",
            contentType: "application/json",
            cache: true,
            data: JSON.stringify(newSiteFilter)
		})
		.done(successHandler)
		.fail(errorHandler);
    }
    
    var CallAPI = function (controller,methodName,parameters,successHandler, errorHandler) {
        $.ajax({
            url: API_URI + "/" + controller + "/" + methodName,
            type: "POST",
            dataType: "json",
            contentType: "application/json",
            cache: true,
            data: JSON.stringify(parameters)
		})
		.done(successHandler)
		.fail(errorHandler);
    }

    var gridDataSource = function () {

        if (_gridDataSource == null) {
            _gridDataSource = new kendo.data.DataSource({
                autoSynch: true,
                transport: {
                    read: function (options) {
                        
                        if(newSiteFilter.Page != options.data.page)
                        {
                        	// the page has changed
                        	// hence we should recreate the grid
                        	// We need to do this because some sites from the next page
                        	// may have additional columns
                        	newSiteFilter.Page = options.data.page;
                        	_gridDataSource.page(newSiteFilter.Page);
                        	recreateGridControl();
                        }
                        else
                        {
	                        _gridDataSourceOptions = options;
	                        refreshGridData();
                        }
                        
                        /*newSiteFilter.Page = options.data.page;
                        _gridDataSourceOptions = options;
                        refreshGridData();*/
                    },
                    create: function (options) {

                    },
                    destroy: function (options) {

                    },
                    update: function (options) {

                        $.ajax({
                            url: API_URI + "/Site/UpdateSite",
                            type: "POST",
                            dataType: "json",
                            contentType: "application/json",
                            data: JSON.stringify({
                            						ModifiedRecords:_gridModifiedRecords,
                            						Project_ID: projectId,
                            						User_ID: userName
												}),
                            success: function (result) {
                                if (result != null) {
                                    var hasErrors = false;
                                    if (result.Messages != null && result.Messages.length > 0) {
                                        var warnings = "Warnings:";
                                        var errors = "Errors:"
                                        for (var i = 0; i < result.Messages.length; i++) {
                                            var m = result.Messages[i];
                                            if (m.Code == "UPDATE-0010")
                                                warnings += "\n- " + m.Description;
                                            else if (m.Code == "UPDATE-0100")
                                            {
                                                errors += "\n- " + m.Description;
                                                hasErrors = true;
                                            }
                                        }

                                        var alertMessage = "";
                                        if (hasErrors)
                                            alertMessage += errors;

                                        if (warnings.length > 10)
                                            alertMessage += (hasErrors) ? "\n\n" + warnings : warnings;

                                        alert(alertMessage);
                                    }

                                    if (result.ModifiedRecords != null && result.ModifiedRecords.length > 0 && !hasErrors) {
										
										// After successfully updating the modified records in the DB
										// we should also reflect this in the _gridData member
										// since it is the one responsible with populating the grid

										for(var site in _gridModifiedRecords)
										{
											// search for the matching record in the _gridData array
											// and then update it
											var modifiedItem = null;
											for(var i = 0; i < _gridData.Sites.length; i++)
											{
												if(_gridData.Sites[i].Site == site.toString())
												{
													modifiedItem = _gridData.Sites[i];
													break;
												}
											}
											// update the internal item from the _gridData
											if(modifiedItem != null)
											{
												for(var updatedProperty in _gridModifiedRecords[site])
												{
													modifiedItem[updatedProperty] = _gridModifiedRecords[site][updatedProperty];
												}
											}
										}
										
										// after we updated the internal _gridData item,
										// we notify the grid that the update was successful						
										
                                        _gridModifiedRecords = {};
                                        options.success();
                                    }

                                }
                            },
                            error: function (result) {
                                alert("update error");
                                options.error(result);
                            }
                        });

                    },
                },
                batch: true,
                schema: _gridDataSourceSchema,
                serverPaging: true,
                pageSize: PAGE_SIZE,
                page: newSiteFilter.Page
            });
        };

        return _gridDataSource;
    };

    var recreateGridControl = function () {

		var grid = $("#spreadsheetView").data("kendoGrid");

		if (grid != null)
			grid.destroy();
        _gridDataSource = null;
        $("#spreadsheetView").empty();
        $("#spreadsheetView").remove();

        $("#podsArea").append($DOM);
        $("#podsArea").showIndicator();

        // newSiteFilter.Page = 1; to_review
        synchFilters();

        // Before creating the control we first need to read the data from the DB
        // This way we will have more control on the column types and we can better define the Grid Columns (editors) and DataSource Schema
        // Afte the data is loaded, we maintain it in a global member which will automatically be returned when the actual grid data will be requested
        gridDataAjax(
            function (result) {

                // Step #1 - Remember the actual data result in a global member
                _gridData = result;

                var sites = (result.Sites != null) ? result.Sites : [];
                
                var fullDataProperties = [];
                var dataProperties = [];
                var dataTaskProperties = [];
                
                
                for(var i = 0; i < sites.length; i++)
                {
                	for(var property in sites[i])
                	{
                		if(property.indexOf("Task") == 0)
                		{
                			if(dataTaskProperties.indexOf(property) < 0)
                				dataTaskProperties.push(property);
                		}
                		else
                		{
                			if(dataProperties.indexOf(property) < 0)
                				dataProperties.push(property);
                		}
                	}
                }
                
                dataTaskProperties.sort();
                fullDataProperties = dataProperties.concat(dataTaskProperties);
                
                if (sites.length > 0) {

                    // Step #2 - Calculate _gridColumns and _gridDataSourceSchema
                    // global member, remember the grid columns configuration
                    _gridColumns = [];
                    // remember the fields of the schema model
                    var schemaFields = { };
                    for (var i = 0; i < fullDataProperties.length; i++) {
						
						var column = fullDataProperties[i];
                        // Create a list of column objects which will be used to initialize the grid
                        var col= {
                            field: column,
                            title: XMLDecode(column),
                            width: 180
                        };
                        
                        // Fetch the datasource schema fields
                        if (column.indexOf("Task") == 0) {
                            col.template = "#=getTaskValue(data,'" + column + "') #";

                            schemaFields[column] = {
                                type: "date",
                                validation: function(item) {
                                    return {
                                        required: true
                                    }
                                }
                            };
                        } else {
                            schemaFields[column] = { type: "string", editable: false };
                        }

                        _gridColumns.push(col);
                    }

                    _gridDataSourceSchema = {
                        model: {
                            id: "Site",
                            fields: schemaFields
                        },
                        data: "Sites",
                        total: "Total"
                    };

                    // Step #3 - Initialize the actual Kendo control using the objects that we previously created:
                    //    - _gridColumns - the actual grid columns configuration object
                    //    - _gridDataSourceSchema - the grid data source schema configuration object
                    //    - _gridData - the actual data to be included in the grid
                    $("#spreadsheetView").kendoGrid({
		                dataSource: gridDataSource(),
		                columns: _gridColumns,
		                sortable: true,
		                editable: true,
		                pageable: {
		                    refresh: false,
		                    pageSizes: false
		                },
		                toolbar: ["save", "cancel"],
		                save: function (e) {
		                    // append the modified record to the list of updated fields
		                    

		                    
							//DMAjax("ValidateActivitySetDates", params, function(data){
							//	switch(data.Code)
					    	//	{
					    	//		case 'DM-0000':
					    	//			var documentItems = JSON.parse(data.Result);
					    	//			options.success(documentItems);
					    	//			refreshGrid();
					    	//			break;
					    	//		default:
					    	//			alert(data.Description);
					    	//			options.error();
					    	//			break;
					    	//	}
							//});

		                    var editedObject = e.values;
		                    var editedSite = e.model.Site;
		                    if (_gridModifiedRecords[editedSite] == null)
		                        _gridModifiedRecords[editedSite] = {};
		
		                    for (var editedProperty in editedObject) {
		                        _gridModifiedRecords[editedSite][editedProperty] = noon(editedObject[editedProperty]);
		                    }
		                    
		                    if(projectId == NDPATT){
			                    var params = new Object();
			                    params.ProjectID = projectId;		                    
			                    params.TaskDetails = _gridModifiedRecords;		                    
			                    params.withWarning = true;
			                    
			                    CallAPI("Site","ValidateActivitySetDates",params,
	            				function (result) {      
	            				var _Message = JSON.parse(result);      				
	            				var warnings = "Warnings:";
					            var errors = "Errors:";
					            var hasErrors = false;
					            var hasWarnings = false;
					            
					            for (var i = 0; i < _Message.Messages.length; i++) {
					                var m = _Message.Messages[i];
					                if (m.Code == "UPDATE-0010")
					                {
					                    warnings += "\n- " + m.Description;
					                    hasWarnings = true;
					                }
					                else if (m.Code == "UPDATE-0100")
					                {
					                    errors += "\n- " + m.Description;
					                    hasErrors = true;
					                }
					            }
					
					            var alertMessage = "";
	                            if (hasErrors)
	                                alertMessage += errors;
	
	                            if (hasWarnings)
	                                alertMessage += (hasErrors) ? "\n\n" + warnings : warnings;
								
								if(hasErrors || hasWarnings)	
	                            	alert(alertMessage);
	            				});
            				}
		                }
		            });
		            
					var grid = $("#spreadsheetView").data("kendoGrid");
 					//grid.hideColumn(1);
 					
		            $(".k-grid-cancel-changes").click(function (e) {
		                _gridModifiedRecords = {};
		            });
		
					// also create the Export to Excel button
					var exportToExcel = $("<a class='k-button k-button-icontext' style='float:right;' href='#'><span class='k-icon k-i-folder-add'></span>Export to Excel</a>");
					$(".k-toolbar.k-grid-toolbar").append(exportToExcel);
					exportToExcel.click(function(e){
						if(buildMarketExportQuery().length < 1)
						{
							alert("Please narrow by Market before exporting to Excel. It will make the export faster.");
						}
						else
						{
							window.open(getExportLink(),'_self');
						}
					});
		                        
		            $(window).trigger("resize");

                }

                $("#podsArea").hideIndicator();
            },
            function (result) {
                alert("Error: " + result);
                $("#podsArea").hideIndicator();
            }
        );
    };



    /*******************************************/
    /**************** UTILITIES ****************/
    /*******************************************/

    var destroyInternal = function () {
        $(window).unbind('click', globalClickHandler);
        
        // clean the filters and the pods area
        filter.destroy();

		// destroy the grid
		var grid = $("#spreadsheetView").data("kendoGrid");
		if(grid != null)
			grid.destroy();

        // clean the PODS area
        var podsArea = $("#podsArea");
        podsArea.empty();
    };

    // getter to determine if the filters have changed
    var filterChanged = function () {
        return !(JSON.stringify(currentSiteFilter) === JSON.stringify(newSiteFilter));
    };

    var synchFilters = function () {
        var s = JSON.stringify(newSiteFilter);
        currentSiteFilter = JSON.parse(s);
    };

    // The grid will be included in this container
    var $DOM = function () {
        var podDOM = $("<div id='spreadsheetView'></div>");
        podDOM.addClass("pod");
        return podDOM;
    };

    // DRAFT METHODS (candidates to be deleted)

    var detailInit = function (e) {
        $("<div/>").appendTo(e.detailCell).kendoListView({
            dataSource: {
                data: [1,2,3,4,5]
            },
            template: kendo.template($("#taskTemplate").html())
        });
    };

    var managerEditor =  function (container, options) {
        $('<input required data-text-field="Manager" data-value-field="Manager" data-bind="value:' + options.field + '"/>')
            .appendTo(container)
            .kendoDropDownList({
                autoBind: false,
                dataSource: [
                    { Manager: "Phil Dudek" },
                    { Manager: "Rob Weiss"}
                ]
            });
    }
    
    // Here we are trying to build the Export to Excel URL
    // The format of the Export to excel is the following
    // E.g.: http://10.104.3.78/sites/NexiusRollout/_vti_bin/reportserver?http://10.104.3.78/sites/NexiusRollout/ATT%20Reports/Milestone%20Tracker.rdl&PROJ_ID=SRT&Market=Pittsburgh&Market=Buffalo&rs:Command=Render&rs:Format=EXCELOPENXML
    var getExportLink = function(){
    	/* 
    	The parameters are as follows:
	    	- {0} - the base url, e.g. '10.104.3.78' or 'accuv.nexius.com'
	    	- {1} - the folder which contains the .rdl file
	    	- {2} - the name of the .rdl file
	    	- {3} - project id, e.g. 'NDPATT' or 'SRT'
	    	- {4} - the concatenated parameters, e.g. 'Market=Pittsburgh&Market=Buffalo'
    	*/
    	
    	var currentPageUrl = "";
    	
    	var param_0 = '';
    	var param_1 = '';
    	var param_2 = '';
    	var param_3 = '';
    	var param_4 = '';
    	
		if (typeof this.href === "undefined") {
		    currentPageUrl = document.location.toString();
		}
		else {
		    currentPageUrl = this.href.toString();
		}
		
		var exportUrl = "";
		if(currentPageUrl.indexOf("/sites/NexiusRollout") >= 0)
			exportUrl = "http://{0}/sites/NexiusRollout/_vti_bin/reportserver?http://{0}/sites/NexiusRollout/{1}/{2}.rdl&PROJ_ID={3}{4}&rs:Command=Render&rs:Format=EXCELOPENXML";
		else
			exportUrl = "http://{0}/_vti_bin/reportserver?http://{0}/{1}/{2}.rdl&PROJ_ID={3}{4}&rs:Command=Render&rs:Format=EXCELOPENXML";
		
		try{
			var urlMatches = currentPageUrl.match(/http:\/\/(?:www\.)?(.[^/]+)(.*)/);
						
			var param_0 = urlMatches[1]; // expected format: '10.104.3.74' or 'accuv.nexius.com'
			var param_1 = "ATT%20Reports"; // (projectId == NDPATT) ? 'ATT%20Reports' : 'Documents';
			var param_2 = 'Milestone%20Tracker';
			var param_3 = projectId;
			var param_4 = buildMarketExportQuery();// 'Market=Pittsburgh&Market=Buffalo';
			
			exportUrl = exportUrl.format(param_0, param_1, param_2, param_3, param_4);
					
		}catch(err){
			alert("There was an error creating the Export to Excel URL");
			exportUrl = "";
		}
    	
    	return exportUrl;
    };
    
    var buildMarketExportQuery = function(){
    	var q = '';
    	for (var i = 0; i < newSiteFilter.Market.length; i++)
    	{
    		q += '&Market=' + newSiteFilter.Market[i];
    	}
    	return q;
    };
    
    
    // INITIALIZE THE "COMMENTS & DOCUMENTS" WINDOW
    
    var initializeCellPropertiesWindow = function(cellData){
    	
    	require(["docManagerReq","commentManager", "docManagerGlobals"],function(DocManagerReq, CommentManager, DocManagerGlobals){
    	
	    	if(cellData == null || cellData.site == null || cellData.task == null || cellData.commenton == null)
			{
				alert("The initialization of the Comments/Documents Window failed. Some mandatory attributes are missing.");
				return;
			}
			
			var tabViewContent = $("<div id='cellPropertiesTabstrip'></div>");
			var tabViewTitle = "Site {0} / Task {1}".format(cellData.site,cellData.task);
			
			tabViewContent.kendoWindow({
				width: 800,
				height: 500,
				modal: true,
				draggable: false,
				resizable: false,
				title: tabViewTitle,
				actions:["Close"],
				close: function(e){
					CommentManager.destroy();
					DocManagerReq.destroy();
					
					var tabViewWindowControl = $("#cellPropertiesTabstrip").data("kendoWindow");
					tabViewWindowControl.destroy();
				}
			});
			tabViewContent.data("kendoWindow").center();
			tabViewContent.data("kendoWindow").open();

			
			// Initialize the Tab Strip
			
			var tabStripItems = [{
                text: "Comments",
                content: CommentManager.generateCommentTabContent(cellData)
            }];
            
            if(projectId == NDPATT)
            {
            	tabStripItems.push({
	                text: "Documents",
	                content: DocManagerReq.generateDocumentTabContent(cellData)
	            });
            }	
		
			$("#cellPropertiesTabstrip").kendoTabStrip({
		        animation: false,
		        dataTextField: "text",
		        dataContentField: "content",
		        dataSource: tabStripItems,
		        activate: function(e){
		        	if($(e.item).text() == 'Documents')
		        	{
		        		DocManagerReq.refresh();
		        	}
		        }
		
		    }).data("kendoTabStrip").select(0);
		    
		    // after the DOM was added to the tabs, initialize the Tabbed contents
		    if(tabStripItems.length > 0) CommentManager.initCommentTab();
		    if(tabStripItems.length > 1) DocManagerReq.initDocumentTab();
			
		});	    	
	};
    
    
    // We need a global click handler to detect when the user clicks on the cell menu button [...]
    // We cannot assign directly a click handler on that button because the Kendo framework
    // is refreshing the list items and loosing the attached events
    var globalClickHandler = function(e){
    	if($(e.target).data("cellproperties") != null)
    	{
    		e.stopPropagation();
    		e.preventDefault();

    		initializeCellPropertiesWindow($(e.target).data());
    	}
    };
	
	var DMAjax = function (methodName, params, options, successHandler, requestType) {
        $("#docWindow").showIndicator();
        
        var dataParams = (params != null) ? JSON.stringify(params) : " ";
        
        var reqType = (requestType == null) ? "POST" : requestType;
        
        $.ajax({
            url: "{0}/Site/{1}".format(API_URI,"ValidateActivitySetDates"),
            type: reqType,
            dataType: "json",
            contentType: "application/json",
            cache: true,
            data: dataParams
		}).done(function(e){
	    	successHandler(e);
	    	var warnings = "Warnings:";
            var errors = "Errors:"
            for (var i = 0; i < result.Messages.length; i++) {
                var m = result.Messages[i];
                if (m.Code == "UPDATE-0010")
                    warnings += "\n- " + m.Description;
                else if (m.Code == "UPDATE-0100")
                {
                    errors += "\n- " + m.Description;
                    hasErrors = true;
                }
            }

            var alertMessage = "";
            if (hasErrors)
                alertMessage += errors;

	    	$("#docWindow").hideIndicator();
		}).fail(function(e){
        	if(options != null)
        	{
        		options.error();
        	}
        	$("#docWindow").hideIndicator();            	            	
        	alert("An internal server error occured while loading the required information.");
		});
    };



    /*******************************************/
    /************* PUBLIC MEMBERS **************/
    /*******************************************/

    return {

        createMilestoneTrackerView: createMilestoneTrackerViewInternal,
        createCloseoutPackageView: createCloseoutPackageViewInternal,
        destroy: destroyInternal

    };
});