define(["jQueryFileDownload","docManagerGlobals"], function (fd,globals) {

	/*
	
		Displays the Required Document Popup
		Performs the document upload/download
		Communicates with the NDPortalAPI instead of directly with Box.net API
	
	*/

	var _siteNumber = "";
	var _taskID = "";
	var _docReqId = "";
	var _docId = "";
	var _docType = "";
	var _docName = "";

	var pluploads = [];	
	
	// When this member will be used, the consumer will have to format it with 2 parameters
	// {0} - the id of the file - MUST be UNIQUE
	// {1} - the name of the file which will be displayed to the user
	// {2} - the class name of the Delete button (the consumer should set it as well as actions for the Delete button)
	var fileItemDomTemplate = "<div class='currentDocumentItemDiv' id='{0}'> \
							   	 <span class='filenameSpan'><a class='filename {1} blue' href='#'>{3}</a></span> \
							  	 <span> | <a class='{2} blue' href='#'>Delete</a></span> \
							   </div>";
	
	var _onCloseHandler = null;
	
	/*******************************************/
    /****** PUBLIC METHODS IMPLEMENTATION ******/
    /*******************************************/

	var showDocumentManagementWindowInternal = function(
			siteNumber, 
			taskID, 
			docReqId, 
			docId,
			docType,
			docName)
		{
		
		_siteNumber = siteNumber;
		_taskID = taskID;
		_docReqId = docReqId;
		_docId = docId;
		_docType = docType.toLowerCase();
		_docName = docName;
		// Create the Document Management Popup
		initializeWindowControl();
		
		// Dynamically load the PLUpload library
		// in case it was not previously loaded
		if(typeof plupload == 'undefined')
		{
			require(["plupload"], function(plup){
				initializeDocumentManagementUploader();
			});
		}else{
			initializeDocumentManagementUploader();
		}
		
		// Create the data grid where the current uploaded are displayed
		initializeCurrentUploadedFilesGrid();
	};
	
	
	// the mandatory fields which should be available in the "fileData" object are:
	// 	- [file_id]
	//  - [file_name]
	var downloadDocumentInternal = function(fileData)
	{
		
		if(!fileData.hasOwnProperty("file_name") 
			|| !fileData.hasOwnProperty("file_id"))
		{
			alert("The file download action parameter requires [file_name] and [file_id] properties.");
			return;
		}
				
		var url = encodeURL(API_URI + "/Box/DownloadStream", fileData);
		
		// show the loading indicator only if we are on the same domain
		// Since the fileDownload library is dealing with a cookie, the domains must match
		if(API_URI.indexOf("localhost") == -1)
			$(document.body).showIndicator();
		
		// Make a request to download the file
		$.fileDownload(url)
		    .done(function () { 
				$(document.body).hideIndicator();
			})
		    .fail(function () { 	
		    	alert('File download failed!'); 
		    	$(document.body).hideIndicator();
			});
	}
	
	
	/*******************************************/
    /************* PRIVATE MEMBERS *************/
    /*******************************************/
	
	
	var initializeWindowControl = function(){
		var windowContent = $("<div id='docWindow' style='overflow:hidden;'></div>");
		 	
		// Create, center and show the comments popup
		windowContent.kendoWindow({
			width: 1150,
			height: 400,
			modal: true,
			draggable: false,
			resiable: false,
			title: "Documents",
			actions:["Close"],
			close: function(e){
				
				if(_onCloseHandler != null)
					_onCloseHandler();
				
				// DESTROY EVERY CONTROL INCLUDING THE POPUP
				for(var i = 0; i < pluploads.length; i++)
				{
					var uploader = pluploads[i];
					uploader.files = [];
					uploader.unbindAll();
					
					var uploaderInputId = "#{0}_html5_container".format(uploader.id);
					$(uploaderInputId).remove();
					
					uploader.destroy();
					uploader = null;
				}
				
				pluploads = [];
				
				$(".uploadedDocDownloadLink").unbind("click");
				$(".uploadedDocApproveLink").unbind("click");
				$(".uploadedDocRejectLink").unbind("click");
				$(".uploadedDocModifyLink").unbind("click");
				$(".uploadedDocUploadToClientLink").unbind("click");
				$(".uploadedDocDeleteLink").unbind("click");

				var docsUploadedGrid = $("#currentfilelist").data("kendoGrid");
				docsUploadedGrid.destroy();
				
				var docWindowControl = $("#docWindow").data("kendoWindow");
				docWindowControl.destroy();
			}
		});
		windowContent.data("kendoWindow").center();
		windowContent.data("kendoWindow").open();

		windowContent.append(getDocumentManagementWindowContent());
	};

	var getDocumentManagementWindowContent = function(){	
		
		var leftTitle = "Add New Photos";
		var rightTitle = "Uploaded Photos";
		if(_docType != DOC_TYPE_PHOTO)
		{
			leftTitle = "Add New Document";
			rightTitle = "Current Document"
		}
		
		var content = $("<table> \
							<tr> \
								<td style='width:340px'><h3>" + leftTitle + "</h3></td> \
								<td><h3>" + rightTitle + "</h3></td> \
							</tr> \
							<tr> \
								<td> \
								<div class='upload-form' id='uploader'> \
										<div style='height:30px;'> \
											<a class='k-button' id='pickfiles'>Select</a> \
											<a class='k-button' id='uploadfiles' title='The files will be automatically renamed when uploading them on the server.'>Upload</a> \
										</div> \
										<!-- Upload File List --> \
										<div id='uploadfilelist'></div> \
									</div> \
								</td> \
								<td><div id='currentfilelist'></td> \
							</tr> \
						</table>");
		return content;
	};
	
	var initializeDocumentManagementUploader = function() {

		var uploadUrl = API_URI + "/Box/PostMultipartStream";
		
		// Settings ////////////////////////////////////////////////
        var uploader = new plupload.Uploader({
            runtimes: 'html5', // Set runtimes, here it will use HTML5, if not supported will use flash, etc.
            browse_button: 'pickfiles', // The id on the select files button
            multi_selection: false, // Allow to select one file each time
            container: 'uploader', // The id of the upload form container
            max_file_size: '10mb', // Maximum file size allowed
            url: uploadUrl // The url to the Upload API Controller including the required params
        });

        // RUNTIME
        uploader.bind('Init', function (up, params) {
            // $('#runtime').text(params.runtime);
        });

        // Start Upload ////////////////////////////////////////////
        // When the button with the id "#uploadfiles" is clicked the upload will start
        $('#uploadfiles').click(function (e) {
        	
        	if(uploader.files.length == 0)
        	{
        		alert("Use the Select button first to add files.");
        		return;
        	}
        	
        	if(matchFileSuffixes(uploader))
        	{
        		uploader.settings.multipart_params = {
	        		docReqId:_docReqId,
	        		userId:loggedInUserId,
	        		projectId:projectId,
	        		siteNumber:_siteNumber,
	        		taskId:_taskID,
	        		docId:_docId,
	        		docName:_docName,
	        		type:_docType
	        	};
	        	
	            uploader.start();
	            e.preventDefault();
	            $("#docWindow").showIndicator();
        	}        	
        });

        uploader.init(); // Initializes the Uploader instance and adds internal event listeners.

        // Selected Files //////////////////////////////////////////
        // When the user select a file it wiil append one div with the class "addedFile" and a unique id to the "#uploadfilelist" div.
        // This appended div will contain the file name and a remove button
        uploader.bind('FilesAdded', function (up, files) {
        
        	if(_docType != DOC_TYPE_PHOTO)
        	{
        		// clean up all the added files and also reset the uploader
        		$('#uploadfilelist').empty();
        		for(var f = uploader.files.length - 2; f >= 0; f--)
	        	{
	        		uploader.removeFile(uploader.files[f]);
	        	}
        	}
        
        	$.each(files, function (i, file) {
        		var fileToUploadDom = fileItemDomTemplate.format(file.id, file.id, "removeUploadFile", file.name);
                $('#uploadfilelist').append(fileToUploadDom);
                $('#uploadfilelist .filenameSpan').css("width","200px");
                $('#uploadfilelist .filenameSpan').css("max-width","200px");
                // Add an additional Input (the filename Suffix input)
                // for each document to upload
                // The suffix input will be available only for the document type "Photo"
				$("<input type='text' placeholder='suffix' maxlength='10' />").insertAfter('#{0} .filenameSpan'.format(file.id));
                	                
                // Resolve the Delete action for the Upload list
                $('#' + file.id + ' a.removeUploadFile').on('click', function (e) {
		            var clickedElement = $(e.currentTarget).closest('.currentDocumentItemDiv');
		            var clickedElementId = clickedElement.attr("id");
		            uploader.removeFile(uploader.getFile(clickedElementId));
		            clickedElement.remove();
		            e.preventDefault();
		        });
		        
		        if(_docType != DOC_TYPE_PHOTO)
		        {	
		        	$("#uploadfilelist input[type=text]").css("display","none");
		        	return false;// instead of break
		        }
            });
            up.refresh(); // Reposition Flash/Silverlight
        });

        // Error Alert /////////////////////////////////////////////
        // If an error occurs an alert window will popup with the error code and error message.
        // Ex: when a user adds a file with now allowed extension
        uploader.bind('Error', function (up, err) {
            alert("Error: " + err.code + ", Message: " + err.message + (err.file ? ", File: " + err.file.name : "") + "");
            $("#docWindow").hideIndicator();
        });

        // Progress bar ////////////////////////////////////////////
        // Add the progress bar when the upload starts
        // Append the tooltip with the current percentage
        uploader.bind('UploadProgress', function (up, file) {
            
        });
        
        // The FileUploaded event will be triggered before the UploadComplete
        // the parameters are
        //  - up - the instance of the uploader
        //  - file - the client object describing the uploaded file
        //  - response - the API response which contains information about the uploaded file
        uploader.bind('FileUploaded', function(up, file, result){
        	var uploadedFileResponse = JSON.parse(result.response);
        	switch(uploadedFileResponse.Code)
        	{
        		case 'DM-0000':
        			// everything was fine, the file was successfully uploaded
        			// Now move the file away from the "pending" list to the "current" list
        			var uploadedFileData = uploadedFileResponse.Result;
        		
        			// remove it from the pending list
        			var uploadedFileElement = getFileContainerByFileName(uploadedFileData.client_file_name, uploadedFileData.suffix);
        			if(uploadedFileElement != null)
        			{
	        			uploadedFileElement.remove();
	        		}
	        		
	        		// remove the internal reference
	        		for(var u = uploader.files.length - 1; u >= 0; u--)
	        		{
	        			if(uploader.files[u].suffix === uploadedFileData.suffix)
        				{
        					uploader.removeFile(uploader.getFile(uploader.files[u].id));
        				}
	        		}

					// append it to the list of current documents
					uploadedFileData.status_desc = STATUS_WAITING_APPROVAL;
					uploadedFileData.user_id = loggedInUserId;
					uploadedFileData.last_modified = null;
					
					_dataSource.add(uploadedFileData);
					refreshGrid();
					
 			        			
        			break;
        		
        		default:
        			// some errors appeared
        			var msg = uploadedFileResponse.Description;
        			if(msg == "Error: The specified suffix is already in use." && _docType != DOC_TYPE_PHOTO)
        			{
        				msg = 'The Document Type requires a single file only. Remove the current uploaded file and then try again.';
        			}
        			
        			alert(msg);
        			break;
        	}
        });

        // Close window after upload ///////////////////////////////
        // If checkbox is checked when the upload is complete it will close the window
        uploader.bind('UploadComplete', function () {
			$("#docWindow").hideIndicator();
        });
        
        pluploads.push(uploader);
        refreshDocumentModifierUploader();
	};
	
	
	
	var refreshDocumentModifierUploader = function() {		
		if(!this.hasOwnProperty("plupload"))
			return;
		
		// Remove all MODIFY Uploaders
		for(var i = pluploads.length - 1; i >= 0; i--)
		{
			var uploader = pluploads[i];
			if(uploader.settings.browse_button.indexOf("modify_") == 0)
			{			
				uploader.files = [];
				uploader.unbindAll();
				var uploaderInputId = "#{0}_html5_container".format(uploader.id);
				$(uploaderInputId).remove();
				uploader.destroy();
				uploader = null;
				pluploads.splice(i,1);
			}
		}
		
		
		// Recreate the MODIFY Uploaders
		for(var i = 0; i < _dataSource.data().length; i++)
		{
			var dataItem = _dataSource.at(i);
			var modifierId = "modify_{0}".format(dataItem.file_id);			
			var modifier = createUploaderForButton(modifierId, "DMModifyDocument");
			modifier.init();
			
			modifier.bind('FilesAdded', function (up, files) {
				$("#docWindow").showIndicator();
				
				var fileId = up.settings.browse_button.replace("modify_","");
				var itemData = getItemDataByFileId(fileId);
				
				up.settings.multipart_params = {
	        		userId:loggedInUserId,
	        		docUplId:itemData.doc_upl_id,
	        		projectId:projectId,
	        		siteNumber: _siteNumber,
	        		taskId: _taskID,
	        		docId:_docId,
	        		docName:_docName,
	        		type:_docType
	        	};
				up.start();
	        });
	        
	        modifier.bind('Error', function (up, err) {
	            alert("Error: " + err.code + ", Message: " + err.message + (err.file ? ", File: " + err.file.name : "") + "");
	            $("#docWindow").hideIndicator();
	        });
	        
	        modifier.bind('FileUploaded', function(up, file, result){
	        	var uploadedFileResponse = JSON.parse(result.response);
	        	switch(uploadedFileResponse.Code)
	        	{
	        		case 'DM-0000':
	        			var uploadedFileData = JSON.parse(uploadedFileResponse.Result);
	        			uploadedFileData.user_id = loggedInUserId;
	        			uploadedFileData.last_modified = null;
	        			
	        			var fileId = up.settings.browse_button.replace("modify_","");
	        			for(var i = 0; i < _dataSource.data().length; i++)
	        			{
	        				var dataItem = _dataSource.at(i);
	        				if(dataItem.file_id == fileId)
	        				{
	        					_dataSource._data[i] = uploadedFileData;
	        					refreshGrid();
	        					break;
	        				}
	        			}
	        			
	        			break;
	        		
	        		default:
	        			alert(uploadedFileResponse.Description);
	        			break;
	        	}
	        	
	        	$("#docWindow").hideIndicator();
	        });
	        
			pluploads.push(modifier);
		}
	};
	
	var createUploaderForButton = function(browseButtonId, methodName)
	{
		var uploadUrl = "{0}/Box/{1}".format(API_URI, methodName);
        var uploader = new plupload.Uploader({
            runtimes: 'html5',
            browse_button: browseButtonId,
            multi_selection: false,
            container: 'uploader',
            max_file_size: '10mb',
            url: uploadUrl
        });        
            
        return uploader;
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	var _dataSource = null;
	var initializeCurrentUploadedFilesGrid = function() {
		_dataSource = new kendo.data.DataSource({
			
			transport: {
				read: function(options){
					kendo.ui.progress($("#currentfilelist"), false);
					var params = {ProjectID: projectId, DocReqID: _docReqId}
					DMAjax("DMGetCurrentDocuments", params, options, function(data){
						switch(data.Code)
			    		{
			    			case 'DM-0000':
			    				var documentItems = JSON.parse(data.Result);
			    				options.success(documentItems);
			    				refreshGrid();
			    				break;
			    			default:
			    				alert(data.Description);
			    				options.error();
			    				break;
			    		}
					});
				}
			}
		});
		

		$("#currentfilelist").kendoGrid({
			columns: [
				{
					title: "File Name",
					template: "#=getCurrentUploadedDocFileName(data)#",
					width: 240
				},
				{
					title: "Status",
					template: "#=getCurrentUploadedDocStatus(data)#",
					width: 115
				},
				{
					title: "Options",
					template: "#=getCurrentUploadedDocOptions(data)#",
					width: 185
				},
				{
					title: "Comment",
					template: "#=getCurrentUploadedDocComment(data)#"
				}

			],
            dataSource: _dataSource,
            width: "100%",
            height: 350,
            sortable: false,
            editable: false,
			pageable: false
        });	
	};
	
	var refreshGrid = function(){
	
		$("#currentfilelist").data("kendoGrid").refresh();
	
		$(".uploadedDocDownloadLink").unbind("click");
		$(".uploadedDocApproveLink").unbind("click");
		$(".uploadedDocRejectLink").unbind("click");
		$(".uploadedDocModifyLink").unbind("click");
		$(".uploadedDocUploadToClientLink").unbind("click");
		$(".uploadedDocDeleteLink").unbind("click");
		
		$(".uploadedDocDownloadLink").bind("click", onCurrentFileClick);
		$(".uploadedDocApproveLink").bind("click", onCurrentFileApproveClick);
		$(".uploadedDocRejectLink").bind("click", onCurrentFileRejectClick);
		$(".uploadedDocModifyLink").bind("click", onCurrentFileModifyClick);
		$(".uploadedDocUploadToClientLink").bind("click", onCurrentFileUploadToClientClick);
		$(".uploadedDocDeleteLink").bind("click", onCurrentFileDeleteClick);
		
		refreshDocumentModifierUploader();
	};
	
	var getFileContainerByFileName = function(fileName, suffix)
	{
		var fileNameElement = null;
	
		$.each($('.currentDocumentItemDiv'), function(i, item){
			var filenameSpan = $(item).find('.filenameSpan');
			var suffixInput = $(item).find('input[type=text]');
			if(filenameSpan.text() == fileName && suffixInput.val() == suffix)
			{
				fileNameElement = $(item);
				return false;
			}
		});
		return fileNameElement;
	}
	
	var matchFileSuffixes = function(uploader){
		var result = true;
		try
    	{
    		var suffixes = [];
    		// clean up the suffix property for each of the file item
    		for(var u = 0; u < uploader.files.length; u++)
			{
				uploader.files[u].status = 1;
				uploader.files[u].name = uploader.files[u].name.replace(/\$\$\$.*/i,'');
				delete uploader.files[u].suffix;
			}
			
			// Do a quick validation of the uploaded files
        	// We need to check if any SUFFIX is duplicated
        	$.each($('.currentDocumentItemDiv input[type=text]'), function(i, item){
				for(var s = 0; s < suffixes.length; s++)
				{
					if($.trim($(item).val()) == suffixes[s])
					{
						throw new Error("The suffixes for all the added files must be different.");
					}
				}
				
				suffixes.push($.trim($(item).val()));
        	});
        	
        	// TO_DO
			// Try to avoind looping the uploader.files as many times
        	
        	// Now we need to attach the suffix to each uploader file item
        	$.each($('.currentDocumentItemDiv input[type=text]'), function(i, item){
				
				var fileName = $(item).closest('.currentDocumentItemDiv').find('.filenameSpan').text();
				for(var u = 0; u < uploader.files.length; u++)		        	
				{
					if(fileName === uploader.files[u].name
						&& !uploader.files[u].hasOwnProperty('suffix'))
					{
						uploader.files[u].suffix = suffixes[i];
						uploader.files[u].name = '{0}$$${1}'.format(uploader.files[u].name,suffixes[i]);
						break;
					}
				}										
        	});
        	
        	// make sure that the suffixes are available for all the files to be uploaded
			for(var u = 0; u < uploader.files.length; u++)		        	
			{
				if(!uploader.files[u].hasOwnProperty('suffix'))
				{
					throw new Error("Internal error while matching the suffixes with the file names.");
				}
			}						
    	}
    	catch(error)
    	{
    		// clean up the suffix property for each of the file item
    		for(var u = 0; u < uploader.files.length; u++)
			{
				uploader.files[u].status = 1;
				uploader.files[u].name = uploader.files[u].name.replace(/\$\$\$.*/i,'');
				delete uploader.files[u].suffix;
			}
    		result = false;
    		alert(error.message);
    	}
    	
    	return result;
	};
	
	
	
	
	
	/*******************************************/
    /****************** EVENTS *****************/
    /*******************************************/





	// File DOWNLOAD click
	var onCurrentFileClick = function(e)
	{
		var file = $(e.currentTarget);
		var fileData = {file_id:file.data("file_id"), file_name:file.data("file_name")};
		// the mandatory fields which should be available in the "fileData" object are:
		// 	- [file_id]
		//  - [file_name]
		downloadDocumentInternal(fileData);
	}
	
	// File APPROVE click
	var onCurrentFileApproveClick = function(e)
	{
		var file = $(e.currentTarget);
		var fileData = {
			docReqId:_docReqId,
			fileId:file.data("file_id"),
			userId: loggedInUserId,
			taskId: _taskID,
	        docId:_docId,
	        docName:_docName,
	        type:_docType
	        
		};
		
		DMAjax("DMApproveDocument", fileData, null, function(data){
			switch(data.Code)
			{
				case 'DM-0000':
					var approvedFileData = JSON.parse(data.Result);
					
					var itemData = getItemDataByClickedElement(e);
					itemData.status_desc = STATUS_APPROVED;
					itemData.comment = "";
					itemData.user_id = loggedInUserId;
					itemData.last_modified = approvedFileData.last_modified;
					itemData.doc_upl_id = approvedFileData.doc_upl_id;
					refreshGrid();
					break;
				
				default:
					alert("An error occurred while approving the document");
					break;
			}
		});
	}
	
	// File REJECT click
	var onCurrentFileRejectClick = function(e)
	{
		var rejectWindowContent = $("<div id='rejectWindow'> \
							  			<textarea id='rejectCommentTextArea' style='width:430px;font-family:Tahoma'></textarea><br/> \
							  			<a id='rejectConfirmButton' class='k-button k-button-icontext' href='#'>Reject</a> \
							  		</div>");
		rejectWindowContent.kendoWindow({
			width: 450,
			height: 87,
			modal: true,
			draggable: false,
			resizable: false,
			title: "Add a rejection comment",
			actions:["Close"],
			close: function(e){
				// destroy the window
				$("#rejectWindow").data("kendoWindow").destroy();
			}
		});
		rejectWindowContent.data("kendoWindow").center();
		rejectWindowContent.data("kendoWindow").open();
		
		$("#rejectConfirmButton").unbind("click");
		
		var file = $(e.currentTarget);
		var fileData = {
			docReqId:_docReqId,
			fileId:file.data("file_id")
		};
		$("#rejectConfirmButton").bind("click", function(){
			// validate the message
			if($.trim($("#rejectCommentTextArea").val()).length < 1)
			{
				alert("Please enter a comment before rejecting the Document.");
				return false;
			}
			
			$("#rejectWindow").showIndicator();
			
			fileData.comment = $.trim($("#rejectCommentTextArea").val());	
			fileData.siteNumber = _siteNumber;
			fileData.taskId 	= _taskID;
			fileData.userId 	= loggedInUserId;
			fileData.docId   	= _docId;
		    fileData.docName 	= _docName;
		    fileData.type    	= _docType;

			DMAjax("DMRejectDocument", fileData, null, function(data){
				switch(data.Code)
				{
					case 'DM-0000':
						var approvedFileData = JSON.parse(data.Result);
						
						var itemData = getItemDataByClickedElement(e);
						itemData.comment = fileData.comment;
						itemData.status_desc = STATUS_REJECTED;
						itemData.user_id = loggedInUserId;
						itemData.last_modified = approvedFileData.last_modified;
						itemData.doc_upl_id = approvedFileData.doc_upl_id;
						$("#rejectWindow").hideIndicator();
						$("#rejectWindow").data("kendoWindow").close();
						
						refreshGrid();
						break;
					default:
						alert("An error occurred while approving the document");
						break;
				}
			});
		});
	}

	// File MODIFY click
	var onCurrentFileModifyClick = function(e)
	{
	
	}
	
	// File UPLOAD TO CLIENT REPOSITORY click
	var onCurrentFileUploadToClientClick = function(e)
	{
		var file = $(e.currentTarget);
		var fileData = {fileId:file.data("file_id")};
		fileData.docReqId = _docReqId;
	
		// Make an ajax request to mark the file as Uploaded to Client Repository
		DMAjax("DMUploadedToClientRepository", fileData, null, function(data){
			switch(data.Code)
			{
				case 'DM-0000':
					var approvedFileData = JSON.parse(data.Result);
					
					var itemData = getItemDataByClickedElement(e);
					itemData.status_desc = STATUS_UPLOADED_TO_CLIENT;
					itemData.comment = "";
					itemData.user_id = loggedInUserId;
					itemData.last_modified = approvedFileData.last_modified;
					itemData.doc_upl_id = approvedFileData.doc_upl_id;

					refreshGrid();
					break;
				default:
					alert("An error occurred while approving the document");
					break;
			}
		});
	}
		
	// File DELETE click
	var onCurrentFileDeleteClick = function(e) {
		var file = $(e.currentTarget);
		var fileData = {file_id:file.data("file_id"), file_name:file.data("file_name")};
		fileData.docReqId 	= _docReqId;
		fileData.siteNumber = _siteNumber;
		fileData.taskId  	= _taskID;
		fileData.docId   	= _docId;
	    fileData.docName 	= _docName;
	    fileData.type    	= _docType;
		fileData.userId  	= loggedInUserId;
		
		// Make an ajax request to remove the file from DB and Box.net
		// Only after the request is successful we remove the file from the UI
		DMAjax("DMDeleteDocument", fileData, null, function(data){
			switch(data.Code)
    		{
    			case 'DM-0000':
    				
    				var itemData = getItemDataByClickedElement(e);
    				_dataSource.remove(itemData);
    				    				
    				break;
    			default:
    				alert(data.Description);
    				break;
    		}
    		
    		refreshGrid();
		});
	};
	
	var getItemDataByClickedElement = function(e)
	{
		var itemData = null;
		var file = $(e.currentTarget);
		if(file.data("file_id") == null)
		{
			alert("Could not extract item data");
			return;
		}
		for(var i = 0; i < _dataSource.data().length; i++)
		{
			var dataSourceItem = _dataSource.at(i);
			if(dataSourceItem.file_id == file.data("file_id"))
			{
				itemData = dataSourceItem;
				break;
			}
		}
		if(itemData == null)
			alert("Could not extract item data");
		
		return itemData;
	}
	
	var getItemDataByFileId = function(fileId)
	{
		var itemData = null;
		for(var i = 0; i < _dataSource.data().length; i++)
		{
			var dataSourceItem = _dataSource.at(i);
			if(dataSourceItem.file_id == fileId)
			{
				itemData = dataSourceItem;
				break;
			}
		}
		return itemData;
	}
	
	
	/*******************************************/
    /*************** AJAX UTILS ****************/
    /*******************************************/

	
	var DMAjax = function (methodName, params, options, successHandler, requestType) {
        $("#docWindow").showIndicator();
        
        var dataParams = (params != null) ? JSON.stringify(params) : " ";
        
        var reqType = (requestType == null) ? "POST" : requestType;
        
        $.ajax({
            url: "{0}/Box/{1}".format(API_URI,methodName),
            type: reqType,
            dataType: "json",
            contentType: "application/json",
            cache: true,
            data: dataParams
		}).done(function(e){
	    	successHandler(e);
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
		
		showDocumentManagementWindow: showDocumentManagementWindowInternal,
		downloadDocument: downloadDocumentInternal,
		onClose: function(handler){
			_onCloseHandler = handler;
		}
    };
});