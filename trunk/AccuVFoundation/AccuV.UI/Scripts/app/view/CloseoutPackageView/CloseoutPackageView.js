define(["filterHelper","docManager","closeoutPackageViewGlobals"], function (FilterHelper, DocManager, CPVGlobals) {

	/*
		We used the prefix CPV (Closeout Package View)
	*/

	var _grid;
	var _dataSource;
	var _currentFilterSet;
	
	var Dom = "<div id='CPVFilters'></div> \
			   <div id='CPVGrid'></div>";

	/*******************************************/
    /******** INTERNAL IMPLEMENTATION **********/
    /*******************************************/

	var createCloseoutPackageViewInternal = function(){
		destroyInternal();
		
		$("#podsArea").append(Dom);
		
		// Create the filters
		// Only the General Contractor and Site filter will be available 
		
		var filters;
		if(userLevel == USER_LEVEL_GENERAL_CONTRACTOR)
		{
			filters = ["generalContractor","site","task","documentId","documentType"];
		}
		else if(userLevel == USER_LEVEL_CONSTRUCTION_MANAGER)
		{
			filters = ["constructionManager","site","task","documentId","documentType"]
		}
		else if(userLevel == USER_LEVEL_MARKET_MANAGER)
		{
			filters = ["marketManager","constructionManager","region","market","site","task","documentId","documentType"];
		}
		else if(userLevel == USER_LEVEL_ADMIN)
		{
			filters = ["marketManager","constructionManager","notApplicable","region","market","site","task","documentId","documentType"];
		}
		
		FilterHelper.createFilters($("#CPVFilters"), filters);
		
		FilterHelper.onFilterChange(function (e, filterSet) {
            filterSet.Page = 1;
            _currentFilterSet = filterSet;
            
            // Create the DataGrid
			// Althoug the grid is very similar with the grid from Milestone Tracker
			// We create it separately, because the logic of the MT Grid is very complex and coupled
            refreshGridData();
        });		
	};
		
	// REFRESH THE GRID
	
	var refreshGridData = function(){
		if(_grid == null)
		{
			createGrid();
		}else{
			// Instead of calling _dataSource.read()
			// we simulate a page chage - it will eventually call the read() method
			_dataSource.page(_currentFilterSet.Page);
		}
	};
	
	// CREATE THE GRID
	
	var createGrid = function(){
		_dataSource = new kendo.data.DataSource({
			transport:{
				read: function(options){
					_currentFilterSet.Page = options.data.page;
					// inform the Stored procedure that the user is admin because the show only applicable filter should be taken
					// into consideration only if the user is admin
					_currentFilterSet.isAdmin = (userLevel == USER_LEVEL_ADMIN?1:0);			
					CPVAjax("GetCloseoutPackageViewData",_currentFilterSet,function(serverData){
						options.success(serverData);
						// trigger the resize event to align the grid to the window
				        $(window).trigger("resize");
				        
				        // _dataSource.page(_currentFilterSet.Page);
					});
				}
			},
			schema: {
				// model: { id: "rowsequence" },
				total: function(data){
						var propTotal = XMLEncode("Records Count");
						if(data != null && data.length > 0)
							return parseInt(data[0][propTotal]);
						return 0;
					}
			},
			serverPaging: true,
            pageSize: 25
		});
		
		$("#CPVGrid").kendoGrid({
			columns: getCPVColumns(),
            dataSource: _dataSource,
            width: 500,
            height: 500,
            sortable: false,
            editable: false,
			pageable: {
                refresh: false,
                pageSizes: false
            }

        });        
        _grid = $("#CPVGrid").data("kendoGrid");
        
        // re-bind the Window Resize action
        $(window).bind("resize", onWindowResizeInternal);
	}
	
	
	// COLUMNS
	
	var getCPVColumns = function(){
		var cols = [];
		
		var columnNames = [" ", "Market", "Construction Manager", "GC Tower Crew", "GC Civil Crew",
			"Site Number", "Site Name", "Task", "Date",
			"Document ID", "Document Name", "Document Type", "Files"];
		
		var columnSettings = [
			{columnName:" ", template:"#=getCPVOptions(data)#", width:83},
			{columnName:"Construction Manager", width:180},
			{columnName:"GC Tower Crew", width:110},
			{columnName:"Site Name", width:250},
			{columnName:"Site Number", width:200},
			{columnName:"Task", width:100},
			{columnName:"Date", template:"#=getCPVDateColumnValue(data)#"},
			{columnName:"Document ID", title:"ID", width:60},
			{columnName:"Document Name", width:250},
			{columnName:"Document Type", title:"Type", width:80},
			{columnName:"Files", template:"#=getCPVFilesAndStatus(data)#", width:320}
		];
		
		for(var i = 0; i < columnNames.length; i++)
		{
			var title = "";
			var template = "";
			var width = 100;
			for(var t = 0; t < columnSettings.length; t++)
			{
				if(columnNames[i] === columnSettings[t].columnName)
				{
					if(columnSettings[t].hasOwnProperty("title"))
						title = columnSettings[t].title;
					if(columnSettings[t].hasOwnProperty("template"))
						template = columnSettings[t].template;
					if(columnSettings[t].hasOwnProperty("width"))
						width = columnSettings[t].width;
					break;
				}
			}
			var col = {
				field:XMLEncode(columnNames[i]),
				title:columnNames[i]
			};
			
			if(title.length > 0)
				col.title = title;
			if(template.length > 0)
				col.template = template;
			if(width > 0)
				col.width = width;
				
			cols.push(col);
		}
		return cols;
	};
	
	// DESTROY THE GRID
	var destroyInternal = function(){
		// destroy the filters
		FilterHelper.destroy();

		// destroy the grid
		$(".CPViewFiles").unbind("click");
		$(".CPVToggleApplicable").unbind("click");
		$(".CPVFileNameLink").unbind("click");
		
		$(window).unbind("resize", onWindowResizeInternal);
		
		if(_grid != null){
			_grid.destroy();
			_dataSource = null;
			_grid = null;
			_currentFilterSet = null;
		}
		
        // clean the PODS area
        var podsArea = $("#podsArea");
        podsArea.empty();
	};
	
	
	
	
	/*******************************************/
    /************* EVENT HANDLERS **************/
    /*******************************************/

	var onViewFilesClickHandler = function(e){
		// extract the data from the clicked element
		var docReqId = $(e.currentTarget).data("doc_req_id");
		var docId = $(e.currentTarget).data("doc_id");
		var docType = $(e.currentTarget).data("doc_type");
		var site = $(e.currentTarget).data("site");
		var task = $(e.currentTarget).data("task");
		var docName = $(e.currentTarget).data("doc_name");
		DocManager.showDocumentManagementWindow(site, task, docReqId, docId, docType,docName);
		DocManager.onClose(function(){
			_dataSource.read();
		});
	};
		
	var onFileNameClickHandler = function(e){
		var fileData = $(e.currentTarget).data();
		DocManager.downloadDocument(fileData);
	};
	
	var onToggleApplicableClickHandler = function(e){
		var fileData = $(e.currentTarget).data();
		var currentNotApplicable = fileData["not_applicable"];
		var params = {docReqId:fileData["doc_req_id"], notApplicable:!currentNotApplicable};
		
		$("#podsArea").showIndicator();
		CPVAjax("SetNotApplicable", params, function(data){
			_dataSource.read();
		});
	};

	
	var onWindowResizeInternal = function() {
	
		var wHeight = $(window).height();
        var wWidth = $(window).width();
        var hHeight = $("#menu").height();
        var podsAreaHeigt = wHeight - hHeight - 75;
	
		if ($("#CPVGrid").doesExist()) {
            $("#CPVGrid").height(podsAreaHeigt - $("#CPVFilters_selector").height());
            $("#CPVGrid").width(wWidth);
            if (_grid != null)
                _grid.refresh();
        }
	
		// re-bind the Click action for the "View Files" button which appear on the each row of the grid
        $(".CPViewFiles").unbind("click");
        $(".CPViewFiles").bind("click",onViewFilesClickHandler);
        
        $(".CPVToggleApplicable").unbind("click");
        $(".CPVToggleApplicable").bind("click",onToggleApplicableClickHandler);
        
        // re-bind the Click action for the File Name Links
        $(".CPVFileNameLink").unbind("click");
        $(".CPVFileNameLink").bind("click",onFileNameClickHandler);
	};
	
	
	
	/*******************************************/
    /*************** AJAX UTILS ****************/
    /*******************************************/

	
	var CPVAjax = function (methodName, params, successHandler) {
        var dataParams = (params != null) ? JSON.stringify(params) : " ";
        $.ajax({
            url: "{0}/CloseoutPackage/{1}".format(API_URI,methodName),
            type: "POST",
            dataType: "json",
            contentType: "application/json",
            cache: true,
            data: dataParams
		}).done(function(e){
            	successHandler(e);
            	$("#podsArea").hideIndicator();
		}).fail(function(e){
            	alert("An internal server error occured while loading the information for the Closeout Package View.");
            	$("#podsArea").hideIndicator();
		})
    };


	/*******************************************/
    /************* PUBLIC MEMBERS **************/
    /*******************************************/

	return {
		createCloseoutPackageView:createCloseoutPackageViewInternal,
		destroy:destroyInternal
	}

});