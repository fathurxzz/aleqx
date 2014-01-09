define(["filterHelper"], function (FilterHelper) {

	var MODE_EDIT = "medit";
	var MODE_VIEW = "mview";
	
	var _mode;
	var _grid;
	
	var Dom = "<div id='GCSiteAssignmentsFilters'></div> \
			   <div id='GCSiteAssignmentsGrid'></div>";

	/*******************************************/
    /******** INTERNAL IMPLEMENTATION **********/
    /*******************************************/

	var createGCSiteAssignmentViewInternal = function(){
		$("#podsArea").append(Dom);
		
		// First determine if the user can associate Sites with GC Contractors (Edit Mode)
		// Of if the user is a GC and can only see the sites associated with him (View Mode)
		extractMode();
		
		// Create the filters
		// Reuse the existing 
		FilterHelper.createFilters($("#GCSiteAssignmentsFilters"),
									["manager","constructionManager","region","market","site"]);
		
		FilterHelper.onFilterChange(function (e, filterSet) {
            filterSet.Page = 1;
            
            // Create the DataGrid
			// Althoug the grid is very similar with the grid from Milestone Tracker
			// We create it separately, because the logic of the MT Grid is very complex and coupled
            refreshGridData(filterSet);
        });		
	};
		
	var extractMode = function(){
		_mode = MODE_EDIT;
	};
	
	var refreshGridData = function(){
		if(_grid == null)
		{
			createGrid();
		}
		
	};
	
	var createGrid = function(){
		var columns = [
			{
				
			}
		];
	}
	
	
	/*******************************************/
    /*************** AJAX UTILS ****************/
    /*******************************************/

	
	
	var GCAjax = function(methodName, params, successHandler)
	{
		GCAjaxInternal("GeneralContractor",methodName, params, successHandler);
	}
	
	var GCSiteAjax = function(methodName, params, successHandler)
	{
		GCAjaxInternal("Site",methodName, params, successHandler);
	}
	
	var GCAjaxInternal = function (controllerName, methodName, params, successHandler) {
        $("#podsArea").showIndicator();
        var dataParams = (params != null) ? JSON.stringify(params) : " ";
        $.ajax({
            url: "{0}/{1}/{2}".format(API_URI,controllerName,methodName),
            type: "POST",
            dataType: "json",
            contentType: "application/json",
            cache: true,
            data: dataParams,
            success: function(e){
            	successHandler(e);
            	$("#podsArea").hideIndicator();
            },
            error: function(e){
            	alert("An internal server error occured while loading the information for the General Contractors.");
            	$("#podsArea").hideIndicator();
            }
        });
    };


	/*******************************************/
    /************* PUBLIC MEMBERS **************/
    /*******************************************/

	return {
		createGCSiteAssignmentView:createGCSiteAssignmentViewInternal
	}

});