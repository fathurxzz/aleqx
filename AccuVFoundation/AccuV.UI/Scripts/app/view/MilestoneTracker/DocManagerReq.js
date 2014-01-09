define(["docManager"], function (DocManager) {

	/*
		Class is responsible for 2 worflows:
			- authenticate on box.net
			- implement the functionality of the "Documents" tab (of the cell Properties popup window)
	*/

	// Internal members
	
	var _authAccessToken = "";
	var _authRefreshToken = "";
		
	var _onAuthenticateComplete = null;
	var _onAuthenticateError = null;
	
	var _siteNumber = "";
	var _taskID = "";
	var _commentOn = ""; // can be either ACTUAL or FORECASTED
	
	/*******************************************/
    /************ INTERNAL METHODS *************/
    /*******************************************/
    	
	var generateDocumentTabContentInternal = function(data) {
		_siteNumber = data.site;
		_taskID = data.task;
		_commentOn = data.commenton;
		
		var windowContent = $("<div id='documentWindow'></div>");
		
		// Create the container of the comments list
		windowContent.append($("<div id='documentReqList'>"));
		
		// return the window content
		return $("<div></div>").append(windowContent).html();
	};
	
	var initDocumentTabInternal = function() {
	
		$("#documentReqList").kendoGrid({
            dataSource: {
                transport: {
                    read: function(options){
						$.ajax({
				            url: API_URI + "/Box/DMRequiredDocuments",
				            type: "POST",
				            dataType: "json",
				            contentType: "application/json",
				            data: JSON.stringify({SiteNumber:_siteNumber, TaskID: _taskID, CommentOn: _commentOn, ProjectID: projectId})
				        }).done(function(data){
			        		
			        		// The "data" parameter has the same format as the DMResponseDTO:
			        		// - Code
			        		// - Description
			        		// - Result (in this case in a JSON String)
			        		
			        		switch(data.Code)
			        		{
			        			case 'DM-0000':
			        				var dataObjects = JSON.parse(data.Result);
			        				options.success(dataObjects);

       								refreshInternal();
       				
			        				break;
			        			default:
			        				alert(data.Description);
			        				break;
			        		}			        		
				        }).fail(function(error){
				        	alert("Error - Could not extract the Required Documents from the DB :(");
				        	options.error();
				        });
					}
                }
			},
            sortable: false,
            pageable: false,
            height: 440,
            columns: [ {
                field: "doc_id",
                width: 60,
                title: "ID"
            	} , {
                field: "doc_name",
                title: "Name"
            	} , {
            	field: "type",
                width: 80,
                title: "Type"
            	} , {
                width: 250,
                title: "Files",
                template: "#=getDocReqFiles(data)#"
            	} , {
                width: 45,
                title: "",
                template: "#=getDocReqOptions(data) #"
            	} ]
        });
	};
	
	var destroy = function() {
		$(".viewfiles").unbind("click",onViewFilesClickHandler);
		$(".reqFileNameLink").unbind("click",onFileNameClickHandler);
	
		if($("#documentReqList").data("kendoGrid") != null)
			$("#documentReqList").data("kendoGrid").destroy();
	};
	
	var refreshInternal = function() {
		var grid = $("#documentReqList").data("kendoGrid");
        if (grid != null)
            grid.refresh();
        
        // re-bind the Click action for the "View Files" button which appear on the each row of the grid
        $(".viewfiles").unbind("click",onViewFilesClickHandler);
        $(".viewfiles").bind("click",onViewFilesClickHandler);
        
        // re-bind the Click action for the File Name Links
        $(".reqFileNameLink").unbind("click",onFileNameClickHandler);
        $(".reqFileNameLink").bind("click",onFileNameClickHandler);
	};
	
	
	/*******************************************/
    /***************** EVENTS ******************/
    /*******************************************/

	var onViewFilesClickHandler = function(e)
	{	
		// extract the data from the clicked element
		var docReqId = $(e.currentTarget).data("doc_req_id");
		var docId = $(e.currentTarget).data("doc_id");
		var docType = $(e.currentTarget).data("doc_type");
		
		DocManager.showDocumentManagementWindow(_siteNumber, _taskID, docReqId, docId, docType);
		DocManager.onClose(function(){
			$("#documentReqList").data("kendoGrid").dataSource.read();
		});
	}
	
	var onFileNameClickHandler = function(e)
	{
		var fileData = $(e.currentTarget).data();
		DocManager.downloadDocument(fileData);
	}
	
	/*******************************************/
    /************* PUBLIC MEMBERS **************/
    /*******************************************/

    return {

		generateDocumentTabContent: generateDocumentTabContentInternal,
		initDocumentTab:initDocumentTabInternal,
		destroy:destroy,
		refresh:refreshInternal
    };

});
