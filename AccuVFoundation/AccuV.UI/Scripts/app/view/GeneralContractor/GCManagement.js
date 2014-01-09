define(["GCGlobals"], function (GCGlobals) {


	var _dataSource = null;
	
	
	/*******************************************/
    /******** INTERNAL IMPLEMENTATION **********/
    /*******************************************/


	var createGCManagementViewInternal = function(){
		createPopupWindow();
	};
		
	var createPopupWindow = function(){
		var podsArea = $("#podsArea");
		
		var GCManagementWindow = $("<div id='GCManagementWindow'></div>");
		
		GCManagementWindow.kendoWindow({
			width: 600,
			height: 500,
			modal: true,
			draggable: false,
			resizable: false,
			title: "General Contractor Management",
			actions:["Close"],
			close: function(e){
				var windowControl = $("#GCManagementWindow").data("kendoWindow");
				windowControl.destroy();
			}
		});
		GCManagementWindow.data("kendoWindow").center();
		GCManagementWindow.data("kendoWindow").open();
		
		getGCTypes();	
	};
	
	// First we Make an Ajax call to get the list of GC types
	var getGCTypes = function(){
		GCAjax("GCTypes",null,function(e){
			
			switch(e.Code)
			{
				case "GC-0000":
					_gcTypes = e.Result;
					createGrid();
					break;
				default:
					alert(e.Description);
					break;
			}

		});
	};
	
	// Create and initialize the grid control
	var createGrid = function()
	{
		$("#GCManagementWindow").append("<div id='GCManagementGrid'></div>");
		
		_dataSource = new kendo.data.DataSource({
	        transport: {
	            read:  function (options) {
					GCAjax("AllGCs",null,function(e){
						switch(e.Code)
						{
							case "GC-0000":
								options.success(e.Result);
								break;
							default:
								alert(e.Description);
								break;
						}
					});
				},
	            update: function (options) {
	            
	            	// only one item can be updated at a time
	            	// so we extract the first model object
	            	var gcItem = options.data.models[0];
                    GCAjax("UpdateGC",gcItem,function(e){
						switch(e.Code)
						{
							case "GC-0000":
								options.success(gcItem);
								break;
							default:
								alert(e.Description);
								break;
						}
					});
	            
				},
	            destroy: function (options) {
					var gcItem = options.data.models[0];
					
					GCAjax("DeleteGC",gcItem,function(e){
						switch(e.Code)
						{
							case "GC-0000":
								options.success(gcItem.gc_type_id);
								break;
							default:
								alert(e.Description);
								break;
						}
					});

				},
	            create: function (options) {
		            var gcItem = options.data.models[0];
					
                    GCAjax("AddGC",gcItem,function(e){
						switch(e.Code)
						{
							case "GC-0000":
								gcItem.gc_id = e.Result;
								options.success(gcItem);
								break;
							default:
								alert(e.Description);
								break;
						}
					});
	            
				}
	        },
	        batch: true,
	        schema: {
	            model: {
	                id: "gc_id",
	                fields: {
	                    gc_username: { validation: {required: true } },
	                    gc_name: { validation: { required: true } },
	                    gc_type_id: { validation: { required: true } }
	                }
	            }
	        }
	    });
		
		
		$("#GCManagementGrid").kendoGrid({
			dataSource: _dataSource,
            pageable: false,
            height: 430,
            toolbar: ["create"],
            columns: [
                { 
                	field: "gc_username", 
                	title: "User Name"
				},
                { 
                	field: "gc_name", 
                	title:"Name"
				},
                { 
                	field: "gc_type_id",
                	title: "Type",
                	template: "#=getGCType(data,this.gcTypes) #",
                	editor: GCTypeEditor
				},
                { 
                	command: ["edit", "destroy"], 
                	title: "&nbsp;", 
                	width: "172px" 
				}],
            editable: "inline"
		});
	};
	
	var GCTypeEditor = function(container, options)
	{
	
		var activeIndex = 0;
		for(var i = 0; i < _gcTypes.length; i++)
		{
			if(options.model.gc_type_id == _gcTypes[i].gc_type_id)
			{
				activeIndex = i;
				break;
			}
		}
	
		$("<input required data-bind='value:gc_type_id'/>")
	        .appendTo(container)
	        .kendoDropDownList({
	        	dataValueField: 'gc_type_id',
	        	dataTextField: 'gc_type_desc',
	            dataSource: {
	                data: _gcTypes
	            },
	            index: activeIndex,
	            text: _gcTypes[activeIndex].gc_type_desc
	        });
	};
	
	var GCAjax = function (methodName, params, successHandler) {
        $("#GCManagementWindow").showIndicator();
        var dataParams = (params != null) ? JSON.stringify(params) : " ";
        $.ajax({
            url: API_URI + "/GeneralContractor/" + methodName,
            type: "POST",
            dataType: "json",
            contentType: "application/json",
            cache: true,
            data: dataParams,
            success: function(e){
            	successHandler(e);
            	$("#GCManagementWindow").hideIndicator();
            },
            error: function(e){
            	alert("An internal server error occured while loading the information for the General Contractors.");
            	$("#GCManagementWindow").hideIndicator();
            }
        });
    };
    



	/*******************************************/
    /************* PUBLIC MEMBERS **************/
    /*******************************************/

	return {
		createGCManagementView:createGCManagementViewInternal
	}

});